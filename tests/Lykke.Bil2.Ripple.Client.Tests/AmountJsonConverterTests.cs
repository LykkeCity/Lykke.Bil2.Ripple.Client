using Lykke.Bil2.Ripple.Client.Api;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lykke.Bil2.Ripple.Client.Tests
{
    public class AmountJsonConverterTests
    {
        [Test]
        public void ShouldSerializeXrpAmount()
        {
            // Arrange
            var amount = new Amount
            {
                Currency = "XRP",
                Value = "123456"
            };

            // Act

            var json = JsonConvert.SerializeObject(amount);

            // Assert

            Assert.AreEqual
            (
                "\"123456000000\"",
                json
            );
        }

        [Test]
        public void ShouldSerializeIssuedCurrency()
        {
            // Arrange

            var amount = new Amount
            {
                Currency = "USD",
                Counterparty = "rHb9CJAWyB4rj91VRWn96DkukG4bwdtyTh",
                Value = "123.456"
            };

            // Act

            var json = JsonConvert.SerializeObject(amount);

            // Assert

            Assert.AreEqual
            (
                "{\"currency\":\"USD\",\"issuer\":\"rHb9CJAWyB4rj91VRWn96DkukG4bwdtyTh\",\"value\":\"123.456\"}",
                json
            );
        }

        [Test]
        public void ShouldSerializeNull()
        {
            // Arrange
            var obj = new
            {
                Amount = (Amount)null
            };

            // Act

            var json = JsonConvert.SerializeObject(obj);

            // Assert

            Assert.AreEqual("{\"Amount\":null}", json);
        }

        [Test]
        public void ShouldDeserializeXrpAmount()
        {
            // Arrange

            // Act

            var amount = JsonConvert.DeserializeObject<Amount>("\"123456000\"");

            // Assert

            Assert.AreEqual("XRP", amount.Currency);
            Assert.AreEqual("123.456000", amount.Value);
        }

        [Test]
        public void ShouldDeserializeIssuedCurrency()
        {
            // Arrange

            // Act

            var amount = JsonConvert.DeserializeObject<Amount>("{\"currency\":\"USD\",\"issuer\":\"rHb9CJAWyB4rj91VRWn96DkukG4bwdtyTh\",\"value\":\"123.456\"}");

            // Assert

            Assert.AreEqual("USD", amount.Currency);
            Assert.AreEqual("rHb9CJAWyB4rj91VRWn96DkukG4bwdtyTh", amount.Counterparty);
            Assert.AreEqual("123.456", amount.Value);
        }

        [Test]
        public void ShouldDeserializeNull()
        {
            // Arrange

            // Act

            var amount = JsonConvert.DeserializeObject<Amount>("null");

            // Assert

            Assert.IsNull(amount);
        }

        [Test]
        public void ShouldDeserializeUndefined()
        {
            // Arrange
            var obj = new
            {
                Amount = default(Amount)
            };

            // Act

            var res = JsonConvert.DeserializeAnonymousType("{}", obj);

            // Assert

            Assert.IsNull(res.Amount);
        }
    }
}