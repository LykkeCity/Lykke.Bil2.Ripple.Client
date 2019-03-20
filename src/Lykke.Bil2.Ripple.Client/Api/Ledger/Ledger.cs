using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Ledger data.
    /// </summary>
    public class Ledger
    {
        /// <summary>
        /// The time this ledger was closed, in seconds since the Ripple Epoch.
        /// </summary>
        [JsonProperty("close_time")]
        public long CloseTime { get; set; }

        /// <summary>
        /// The time at which the previous ledger was closed.
        /// </summary>
        [JsonProperty("parent_close_time")]
        public long ParentCloseTime { get; set; }

        /// <summary>
        /// Unique identifying hash of the ledger that came immediately before this one.
        /// </summary>
        [JsonProperty("parent_hash")]
        public string ParentHash { get; set; }

        /// <summary>
        /// Total number of XRP drops in the network.
        /// This decreases as transaction costs destroy XRP.
        /// </summary>
        [JsonProperty("total_coins")]
        public ulong TotalCoins { get; set; }

        /// <summary>
        /// Transactions applied in this ledger version.
        /// </summary>
        [JsonProperty("transactions")]
        public Transaction[] Transactions { get; set; }
    }
}