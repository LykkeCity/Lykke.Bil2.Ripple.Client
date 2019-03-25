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
                .AddRippleClient("http://s1.ripple.com:51234", logRequestErrors: false)
                .BuildServiceProvider();

            _api = serviceProvider.GetRequiredService<IRippleApi>();
        }

        [Test]
        public async Task ShouldParseBalanceChanges()
        {
            // Arrange

            // Act

            var response = await _api.Post(new TxRequest("949C39E621F2C7F9ECF77C3BBB9100CBDF1323EB11088EB9E4371FC890423839"));

            var balanceChanges = response.Result.Metadata.GetBalanceChanges();

            // Assert

            Assert.IsNotEmpty(balanceChanges);
            Assert.True(balanceChanges.All(x => x.Value.Any()));
            Assert.True(balanceChanges.All(x => x.Value.Length == x.Value.GroupBy(y => new { y.Counterparty, y.Currency }).Count()));
        }
    }
}