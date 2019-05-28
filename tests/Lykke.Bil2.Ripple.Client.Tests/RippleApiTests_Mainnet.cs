using System;
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
    public class RippleApiTests_Mainnet
    {
        IRippleApi _api;

        [OneTimeSetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddRippleClient("http://s2.ripple.com:51234", logRequestErrors: false)
                .BuildServiceProvider();

            _api = serviceProvider.GetRequiredService<IRippleApi>();
        }

        [Test]
        //[Ignore("txnNotFound")]
        public async Task ShouldParseBalanceChanges()
        {
            // Arrange

            // Act

            var response = await _api.Post(new TxRequest("3CD1D8DBB148AC9CD2A69673F0CAA2FC754414E42565C6194434F52AD00E3795"));

            var balanceChanges = response.Result.Metadata.GetBalanceChanges();

            // Assert

            Assert.IsNotEmpty(balanceChanges);
            Assert.True(balanceChanges.All(x => x.Value.Any()));
            Assert.True(balanceChanges.All(x => x.Value.Length == x.Value.GroupBy(y => new { y.Counterparty, y.Currency }).Count()));
        }

        [Test]
        public async Task ShouldReturnLedgerByIndex()
        {
            // Arrange

            // Act

            var response = await _api.Post(new BinaryLedgerWithTransactionsRequest(1323927));
            var ledger = response.Result.Ledger.Parse();
            var binaryTx = response.Result.Ledger.Transactions.First();
            var tx = binaryTx.Parse();
            var balanceChanges = tx.Metadata.GetBalanceChanges();

            // Assert

            Assert.IsNotNull(response.Result);
            Assert.AreEqual("success", response.Result.Status);
            Assert.AreEqual(1323927, response.Result.LedgerIndex);
            Assert.AreEqual("2D4B4EAB02C681191A42747A45BFC1929A63791018609994509269DCD3D69A98", response.Result.LedgerHash);
            Assert.AreEqual("7B4E4F5AD4E89F78CD1BCECA31641C703F20FFC97B5DB89B885A4C2D59A91082", ledger.ParentHash);
            Assert.IsNotEmpty(response.Result.Ledger.Transactions);
            Assert.IsNotEmpty(ledger.Transactions);
            Assert.AreEqual("3CD1D8DBB148AC9CD2A69673F0CAA2FC754414E42565C6194434F52AD00E3795", ledger.Transactions[0].Hash);
            Assert.AreEqual("tesSUCCESS", ledger.Transactions[0].Metadata.TransactionResult);
        }
    }
}