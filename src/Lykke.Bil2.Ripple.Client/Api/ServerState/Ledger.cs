using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.ServerState
{
    /// <summary>
    /// Ripple ledger (block) info.
    /// </summary>
    public class Ledger
    {
        /// <summary>
        /// Unique hash of this ledger version, as hex.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Unique sequence number of this ledger.
        /// </summary>
        [JsonProperty("seq")]
        public long Seq { get; set; }

        /// <summary>
        /// Time this ledger was closed, in seconds since the Ripple Epoch.
        /// </summary>
        [JsonProperty("close_time")]
        public long CloseTime { get; set; }

        /// <summary>
        /// Base fee, in drops of XRP, for propagating a transaction to the network.
        /// </summary>
        [JsonProperty("base_fee")]
        public long BaseFee { get; set; }

        /// <summary>
        /// Minimum amount, in drops of XRP, necessary for every account to keep in reserve.
        /// </summary>
        [JsonProperty("reserve_base")]
        public long ReserveBase { get; set; }
    }
}