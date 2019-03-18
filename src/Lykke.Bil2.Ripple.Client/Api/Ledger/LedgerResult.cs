using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    public class LedgerResult : RippleResponseResultBase
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
        /// Header data of this ledger.
        /// </summary>
        [JsonProperty("ledger")]
        public Header Ledger { get; set; }
    }
}