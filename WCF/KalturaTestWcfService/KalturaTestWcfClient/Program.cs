using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalturaTestWcfClient.RemoteService;

namespace KalturaTestWcfClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // Client service creating
            var client = new StripeServiceClient();

            try
            {
                // Checking connection
                Console.WriteLine("Checking connection... ");
                if (!string.Equals(client.TestConnection(), "OK", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception("Connection error");
                }
                else
                {
                    Console.WriteLine("OK!");
                }
                Console.WriteLine();

                //Set client data
                string customerId = "cus_E9A3t8GleymVcN";
                double amount = 2000;
                string currency = "usd";
                string cardId = "card_1DgrpEJTZKBWLpi8FWmM4gof";
                var extraData = new Dictionary<string, string>();

                var result = client.Transact(customerId, amount, currency, cardId, extraData);
                if (result)
                {
                    Console.Write("Payment successfully processed");
                }
                else
                {
                    Console.Write("Invalid payment");
                }

                // Closing client
                client.Close();
            }
            catch (Exception ex)
            {
                client.Abort();
                Console.WriteLine();
                Console.WriteLine("Error: {0}", ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
