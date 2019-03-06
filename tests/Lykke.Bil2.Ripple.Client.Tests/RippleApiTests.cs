using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lykke.Bil2.Ripple.Client.Tests
{
    public class RippleApiTests
    {
        IRippleApi _api;

        [OneTimeSetUp]
        public void Setup()
        {
            var env = Environment.CurrentDirectory;

            var path = "appsettings.test.json";
            if (File.Exists(path))
            {
                dynamic settings = JsonConvert.DeserializeObject(File.ReadAllText(path));

                _api = new ServiceCollection()
                    .AddRippleClient((string)settings.Url, (string)settings.Username, (string)settings.Password)
                    .BuildServiceProvider()
                    .GetRequiredService<IRippleApi>();
            }
        }

        [Test]
        public async Task ShouldGetAccountInfo()
        {
            if (_api == null) Assert.Ignore();

            // Arrange

            // Act

            var response = await _api.Request(new AccountInfoRequest("rE6jo1LZNZeD3iexQ6DnfCREEWZ9aUweVy"));

            // Assert

            Assert.IsNotNull(response);
            Assert.AreEqual("success", response.Result.Status);
            Assert.IsNotEmpty(response.Result.AccountData.Balance);
            Assert.IsTrue(long.TryParse(response.Result.AccountData.Balance, out var _));
        }
    }
}