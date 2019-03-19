using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Header data of this ledger.
    /// </summary>
    public class BinaryLedgerHeader
    {
        /// <summary>
        /// Whether or not this ledger has been closed.
        /// </summary>
        [JsonProperty("closed")]
        public bool Closed { get; set; }

        /// <summary>
        /// Ledger data in binary (HEX) format.
        /// </summary>
        [JsonProperty("ledger_data")]
        public string LedgerData { get; set; }

        /// <summary>
        /// Transactions applied in this ledger version.
        /// </summary>
        [JsonProperty("transactions")]
        public BinaryTransaction[] Transactions { get; set; }
    }
}