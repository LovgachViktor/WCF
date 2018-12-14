using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Stripe;

namespace KalturaTestWcfService
{
    public class StripeService : IStripeService
    {
        #region Common Methods
        public string TestConnection()
        {
            return "OK";
        }

        #endregion

        #region Stripe methods

        public bool Transact(string customerId, double amount, string currency, string cardId, Dictionary<string, string> extraData)
        {
            var pendingLogQueue = new BlockingCollection<LogMessage>();
            var threadAdapter = new ThreadAdapter();
            var loggerFactory = new LoggerFactory(pendingLogQueue, threadAdapter);

            var simpleTextFileLogger = new SimpleTextFileLogger();
            simpleTextFileLogger.Start();
            ILogListener[] listeners = new[] { simpleTextFileLogger };

            var loggingQueueDispatcher = new LoggingQueueDispatcher(pendingLogQueue, listeners);
            loggingQueueDispatcher.Start();

            var _logger = loggerFactory.For(typeof(Console));

            try
            {
                var apiKey = ConfigurationManager.AppSettings["StripeApiKey"];
                StripeConfiguration.SetApiKey(apiKey);
                var options = new ChargeCreateOptions
                {
                    CustomerId = customerId,
                    Amount = Convert.ToInt64(amount),
                    Currency = currency,
                    Description = "",
                    SourceId = cardId
                };
                _logger.Debug("Sended data: CustomerId: " + options.CustomerId + " Amount: " + options.Amount + " Currency: " + options.Currency + " CardId" + options.SourceId);
                var service = new ChargeService();
                try
                {
                    Charge charge = service.Create(options);
                    if (charge.Status == "succeeded")
                    {
                        _logger.Info("Payment successfully processed");
                        pendingLogQueue.CompleteAdding();
                        simpleTextFileLogger.Stop();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Invalid payment" + e.Message + e.StackTrace);
                    pendingLogQueue.CompleteAdding();
                    simpleTextFileLogger.Stop();
                    return false;
                }
              
            }
            catch (Exception e)
            {
                _logger.Error("Invalid transact" + e.Message + e.StackTrace);
                pendingLogQueue.CompleteAdding();
                simpleTextFileLogger.Stop();
                return false;
            }
            pendingLogQueue.CompleteAdding();
            simpleTextFileLogger.Stop();
            return false;
        }

        #endregion
    }
}
