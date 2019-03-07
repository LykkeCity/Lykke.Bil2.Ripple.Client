using System;
using System.IO;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Bil2.Ripple.Client.Api.AccountInfo;
using Lykke.Bil2.Ripple.Client.Api.ServerState;
using Lykke.Common.Log;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

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
        public async Task ShouldGetAccountInfo()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountInfoRequest((string)_settings.Account));

            // Assert

            Assert.IsNotNull(response.Result);
            Assert.AreEqual("success", response.Result.Status);
            Assert.IsNotEmpty(response.Result.AccountData.Balance);
            Assert.IsTrue(long.TryParse(response.Result.AccountData.Balance, out var _));
        }

        [Test]
        public async Task ShouldReturnDestinationTagRequired()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountInfoRequest((string)_settings.AccountWithDestinationTagRequired));

            // Assert

            Assert.IsTrue(response.Result.AccountData.IsDestinationTagRequired);
        }

        [Test]
        public async Task ShouldReturnAccountNotFound()
        {
            // Arrange

            // Act

            var response = await _api.Post(new AccountInfoRequest((string)_settings.AccountNotFound));

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
    }
}