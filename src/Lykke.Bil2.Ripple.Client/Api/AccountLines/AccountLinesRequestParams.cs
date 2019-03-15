using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.AccountLines
{
    /// <summary>
    /// Account trustlines request parameters.
    /// </summary>
    public class AccountLinesRequestParams
    {
        /// <summary>
        /// Initializes new instance of <see cref="AccountLinesRequestParams"/>.
        /// </summary>
        /// <param name="account">Address of Ripple account to return account trustlines.</param>
        public AccountLinesRequestParams(string account)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        /// <summary>
        /// Address of Ripple account to return account info.
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; }
    }
}