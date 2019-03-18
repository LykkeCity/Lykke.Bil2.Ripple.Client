using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    public class Header
    {
        /// <summary>
        /// The time this ledger was closed, in seconds since the Ripple Epoch.
        /// </summary>
        [JsonProperty("close_time")]
        public long CloseTime { get; set; }

        /// <summary>
        /// Whether or not this ledger has been closed.
        /// </summary>
        [JsonProperty("closed")]
        public bool Closed { get; set; }

        /// <summary>
        /// Unique identifying hash of the ledger that came immediately before this one.
        /// </summary>
        [JsonProperty("parent_hash")]
        public string ParentHash { get; set; }

        /// <summary>
        /// Transactions applied in this ledger version (hashes).
        /// </summary>
        [JsonProperty("transactions")]
        public string[] Transactions { get; set; }
    }
}