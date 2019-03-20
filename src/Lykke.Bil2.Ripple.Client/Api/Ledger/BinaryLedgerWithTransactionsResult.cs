using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    public class BinaryLedgerWithTransactionsResult : RippleResponseResultBase
    {
        /// <summary>
        /// Unique identifying hash of the entire ledger.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }

        /// <summary>
        /// The Ledger Index of this ledger, as a quoted integer.
        /// </summary>
        [JsonProperty("ledger_index")]
        public uint LedgerIndex { get; set; }

        /// <summary>
        /// Ledger data.
        /// </summary>
        [JsonProperty("ledger")]
        public BinaryLedger Ledger { get; set; }

        /// <summary>
        /// True if this data is from a validated ledger version; if omitted or set to false, this data is not final.
        /// </summary>
        /// <value></value>
        [JsonProperty("validated")]
        public bool? Validated { get; set; }
    }
}