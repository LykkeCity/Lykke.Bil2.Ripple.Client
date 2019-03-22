using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.AccountInfo
{
    /// <summary>
    /// Result of "account_info" method.
    /// </summary>
    public class AccountInfoResult : RippleResponseResultBase
    {
        /// <summary>
        /// Account's information, as stored in the ledger.
        /// </summary>
        /// <value></value>
        [JsonProperty("account_data")]
        public AccountRoot AccountData { get; set; }

        /// <summary>
        /// The sequence number of the most-current ledger, which was used when retrieving this information.
        /// The information does not contain any changes from ledgers newer than this one.
        /// Omitted if ledger_index is provided instead.
        /// </summary>
        /// <value></value>
        [JsonProperty("ledger_current_index")]
        public uint? LedgerCurrentIndex { get; set; }

        /// <summary>
        /// The sequence number of the ledger used when retrieving this information.
        /// The information does not contain any changes from ledgers newer than this one.
        /// Omitted if ledger_current_index is provided instead.
        /// </summary>
        [JsonProperty("ledger_index")]
        public uint? LedgerIndex { get; set; }

        /// <summary>
        /// True if this data is from a validated ledger version; if omitted or set to false, this data is not final.
        /// </summary>
        /// <value></value>
        [JsonProperty("validated")]
        public bool? Validated { get; set; }
    }
}