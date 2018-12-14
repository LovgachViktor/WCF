using System;
using System.Collections.Generic;
using KalturaTestWcfClient.RemoteService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WcfServiceUnitTest
{
    [TestClass]
    public class WcfUnitTest
    {
        private StripeServiceClient client = new StripeServiceClient();
        [TestMethod]
        public void CallTransact_ValidAllData_ReturnsTrue()
        {
            string customerId = "cus_E9A3t8GleymVcN";
            double amount = 2000;
            string currency = "usd";
            string cardId = "card_1DgrpEJTZKBWLpi8FWmM4gof";
            var extraData = new Dictionary<string, string>();
            var result = client.Transact(customerId, amount, currency, cardId, extraData);

            Assert.IsTrue(result,"Resul is false when invalid data");
        }

        [TestMethod]
        public void CallTransact_InvalidCustomerId_ReturnsFalse()
        {
            string customerId = "cus_E9A3t8GleymVcN123";
            double amount = 2000;
            string currency = "usd";
            string cardId = "card_1DgrpEJTZKBWLpi8FWmM4gof";
            var extraData = new Dictionary<string, string>();
            var result = client.Transact(customerId, amount, currency, cardId, extraData);

            Assert.IsFalse(result,"Resul is true when passed invalid customerId");
        }

        [TestMethod]
        public void CallTransact_InvalidAmount_ReturnsFalse()
        {
            string customerId = "cus_E9A3t8GleymVcN";
            double amount = -1;
            string currency = "usd";
            string cardId = "card_1DgrpEJTZKBWLpi8FWmM4gof";
            var extraData = new Dictionary<string, string>();
            var result = client.Transact(customerId, amount, currency, cardId, extraData);

            Assert.IsFalse(result, "Resul is true when passed invalid amount");
        }

        [TestMethod]
        public void CallTransact_InvalidCurrency_ReturnsFalse()
        {
            string customerId = "cus_E9A3t8GleymVcN";
            double amount = 2000;
            string currency = "usd123";
            string cardId = "card_1DgrpEJTZKBWLpi8FWmM4gof";
            var extraData = new Dictionary<string, string>();
            var result = client.Transact(customerId, amount, currency, cardId, extraData);

            Assert.IsFalse(result, "Resul is true when passed invalid currency");
        }

        [TestMethod]
        public void CallTransact_InvalidCardId_ReturnsFalse()
        {
            string customerId = "cus_E9A3t8GleymVcN";
            double amount = 2000;
            string currency = "usd";
            string cardId = "card_1DgrpEJTZKBWLpi8FWmM4gof1";
            var extraData = new Dictionary<string, string>();
            var result = client.Transact(customerId, amount, currency, cardId, extraData);

            Assert.IsFalse(result, "Resul is true when passed invalid cardId");
        }
    }
}
