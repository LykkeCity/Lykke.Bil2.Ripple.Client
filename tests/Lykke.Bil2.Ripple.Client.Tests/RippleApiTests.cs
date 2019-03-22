using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Common;
using Lykke.Bil2.Ripple.Client.Api.AccountInfo;
using Lykke.Bil2.Ripple.Client.Api.AccountLines;
using Lykke.Bil2.Ripple.Client.Api.Ledger;
using Lykke.Bil2.Ripple.Client.Api.ServerState;
using Lykke.Bil2.Ripple.Client.Api.Tx;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Ripple.Core.Util;

namespace Lykke.Bil2.Ripple.Client.Tests
{
    public class RippleApiTests
    {
        IRippleApi _api;
        dynamic _settings;

        [OneTimeSetUp]
        public void Setup()
        {
            var settingsPath = "appsettings.test.json";

            if (!File.Exists(settingsPath))
            {
                Assert.Ignore();
            }

            _settings = JsonConvert.DeserializeObject(File.ReadAllText(settingsPath));

            var serviceProvider = new ServiceCollection()
                .AddRippleClient
                (
                    (string)_settings.Url,
                    (string)_settings.Username,
                    (string)_settings.Password,
                    logRequestErrors: false
                )
                .BuildServiceProvider();

            _api = serviceProvider.GetRequiredService<IRippleApi>();
        }

        [Test]
        public async Task ShouldReturnAccountInfo()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountInfoRequest("rE6jo1LZNZeD3iexQ6DnfCREEWZ9aUweVy"));

            // Assert

            Assert.IsNotNull(response.Result);
            Assert.AreEqual("success", response.Result.Status);
            Assert.IsNotEmpty(response.Result.AccountData.Balance);
            Assert.IsTrue(long.TryParse(response.Result.AccountData.Balance, out var _));
        }

        [Test]
        public async Task ShouldReturnAccountLines()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountLinesRequest("rHb9CJAWyB4rj91VRWn96DkukG4bwdtyTh"));

            // Assert

            Assert.IsNotNull(response.Result);
            Assert.AreEqual("success", response.Result.Status);
            Assert.IsNotEmpty(response.Result.Lines);
        }

        [Test]
        public async Task ShouldReturnDestinationTagRequired()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountInfoRequest("rG1Zu2dm2Ty9pQrnGJux1RuKZA6qhjWwMc"));

            // Assert

            Assert.IsTrue(response.Result.AccountData.IsDestinationTagRequired);
        }

        [Test]
        public async Task ShouldReturnAccountNotFound()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountInfoRequest("r9otZt3oCDL2UPioEiGduu5g5zXkqaPZt9"));

            // Assert

            Assert.AreEqual("error", response.Result.Status);
            Assert.AreEqual("actNotFound", response.Result.Error);
        }

        [Test]
        public async Task ShouldReturnServerState()
        {
            // Arrange

            // Act

            var response = await _api.Post(new ServerStateRequest());

            // Assert

            Assert.IsNotNull(response.Result);
            Assert.IsNotNull(response.Result.State.ClosedLedger ?? response.Result.State.ValidatedLedger);
            Assert.AreEqual("success", response.Result.Status);
            Assert.AreEqual("full", response.Result.State.ServerState);
        }

        [Test]
        public async Task ShouldReturnTxPayment()
        {
            // Arrange
            var hash = "673D36E10DBD969D3F639858F2B0D3151CB3B2F2FE2300188742A685ABD8C3EC";

            // Act

            var response = await _api.Post(new TxRequest(hash));

            // Assert

            Assert.AreEqual(hash, response.Result.Hash);
            Assert.AreEqual("Payment", response.Result.TransactionType);
            Assert.AreEqual("0.290000", response.Result.Metadata.DeliveredAmount.Value);
            Assert.AreEqual("12", response.Result.Fee);
            Assert.AreEqual(1798, response.Result.Sequence);
            Assert.AreEqual("tesSUCCESS", response.Result.Metadata.TransactionResult);
        }

        [Test]
        public async Task ShouldReturnTxNotPayment()
        {
            // Arrange
            var hash = "442DE01B4165E0EE85A48EAE6CB895B426CFDD9F243A6EEFA5AC36292C9DCCED";

            // Act

            var response = await _api.Post(new TxRequest(hash));

            // Assert

            Assert.AreEqual(hash, response.Result.Hash);
            Assert.AreEqual("AccountSet", response.Result.TransactionType);
            Assert.IsNull(response.Result.Metadata.DeliveredAmount);
            Assert.AreEqual("12", response.Result.Fee);
            Assert.AreEqual(178, response.Result.Sequence);
            Assert.AreEqual("tesSUCCESS", response.Result.Metadata.TransactionResult);
        }

        [Test]
        public async Task ShouldReturnLedgerByIndex()
        {
            // Arrange

            // Act

            var response = await _api.Post(new BinaryLedgerWithTransactionsRequest(17860911));
            var ledger = response.Result.Ledger.Parse();

            // Assert

            Assert.IsNotNull(response.Result);
            Assert.AreEqual("success", response.Result.Status);
            Assert.AreEqual(17860911, response.Result.LedgerIndex);
            Assert.AreEqual("7B49D99C332D7D8D10AB1BE79D04076E4284D521FD5E6CA9034A296E870A40F4", response.Result.LedgerHash);
            Assert.AreEqual("9ECB6BF942D0B93D16DF96FAEB22E3FC852DDEEDB5C25AC76BF8989ABF47FA45", ledger.ParentHash);
            Assert.IsNotEmpty(response.Result.Ledger.Transactions);
            Assert.IsNotEmpty(ledger.Transactions);
            Assert.AreEqual("6E1FA8671649B117D13AC3C14BA653AC29FAA3533F0F5884E868BB9B9553FE81", ledger.Transactions[0].Hash);
            Assert.AreEqual(772, ledger.Transactions[0].DestinationTag);
            Assert.AreEqual(2, ledger.Transactions[0].Metadata.TransactionIndex);
            Assert.AreEqual("tesSUCCESS", ledger.Transactions[0].Metadata.TransactionResult);
        }
    }
}