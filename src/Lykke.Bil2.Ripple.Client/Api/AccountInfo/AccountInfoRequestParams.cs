using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.AccountInfo
{
    /// <summary>
    /// Account info request parameters.
    /// </summary>
    public class AccountInfoRequestParams
    {
        /// <summary>
        /// Initializes new instance of <see cref="AccountInfoRequestParams"/>.
        /// </summary>
        /// <param name="account">Address of Ripple account to return account info.</param>
        public AccountInfoRequestParams(string account)
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