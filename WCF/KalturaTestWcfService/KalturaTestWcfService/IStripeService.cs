using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace KalturaTestWcfService
{
    [ServiceContract]
    public interface IStripeService
    {
        #region Common Methods

        /// <summary>
        /// Test connection
        /// </summary>
        /// <returns> OK </returns>
        [OperationContract]
        string TestConnection();

        #endregion

        #region Stripe methods

        /// <summary>
        /// Transact
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <param name="amount"> Transaction amount </param>
        /// <param name="currency"> Currency code </param>
        /// <param name="cardId"> Credit card Id</param>
        /// <param name="extraData"> ExtraData </param>
        /// <returns> success </returns>
        [OperationContract]
        bool Transact(string customerId, double amount, string currency, string cardId, Dictionary<string, string> extraData);
        #endregion
    }
}
