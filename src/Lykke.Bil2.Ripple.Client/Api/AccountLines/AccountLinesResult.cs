using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.AccountLines
{
    /// <summary>
    /// Result of "account_lines" method.
    /// </summary>
    public class AccountLinesResult : RippleResponseResultBase
    {
        /// <summary>
        /// The identifying address of this account.
        /// </summary>
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// Account's information, as stored in the ledger.
        /// </summary>
        [JsonProperty("lines")]
        public Line[] Lines { get; set; }
    }
}