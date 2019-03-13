using System;
using System.IO;
using System.Threading.Tasks;
using Lykke.Bil2.Ripple.Client.Api.AccountInfo;
using Lykke.Bil2.Ripple.Client.Api.ServerState;
using Lykke.Bil2.Ripple.Client.Api.Tx;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Lykke.Bil2.Ripple.Client.Tests
{
    public class RippleDateTimeTests
    {
        [TestCase(0, "2000-01-01")]
        [TestCase(60, "2000-01-01T00:01:00")]
        [TestCase(-60, "1999-12-31T23:59:00")]
        public void ShouldConvertNumberOfSecondsToDateTime(long value, string expected)
        {
            // Arrange

            // Act

            var actual = value.FromRippleEpoch();

            // Assert

            Assert.AreEqual(DateTime.Parse(expected), actual);
        }

        [TestCase("2000-01-01", 0)]
        [TestCase("2000-01-01T00:01:00", 60)]
        [TestCase("1999-12-31T23:59:00", -60)]
        public void ShouldConvertDateTimeToNumberOfSeconds(string value, long expected)
        {
            // Arrange

            // Act

            var actual = DateTime.Parse(value).ToRippleEpoch();

            // Assert

            Assert.AreEqual(expected, actual);
        }
    }
}