using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.ServerState
{
    /// <summary>
    /// Server state.
    /// </summary>
    public class State
    {
        /// <summary>
        /// The version number of the running rippled version.
        /// </summary>
        [JsonProperty("build_version")]
        public string BuildVersion { get; set; }

        /// <summary>
        /// Range expression indicating the sequence numbers of the ledger versions the local rippled has in its database.
        /// If the server does not have any complete ledgers this is the string empty.
        /// </summary>
        [JsonProperty("complete_ledgers")]
        public string CompleteLedgers { get; set; }

        /// <summary>
        /// A string indicating to what extent the server is participating in the network.
        /// </summary>
        [JsonProperty("server_state")]
        public string ServerState { get; set; }

        /// <summary>
        /// (May be omitted) Information on the most recently closed ledger that has not been validated by consensus.
        /// If the most recently validated ledger is available, the response omits this field and includes validated_ledger instead.
        /// </summary>
        [JsonProperty("closed_ledger")]
        public LedgerState ClosedLedger { get; set; }

        /// <summary>
        /// (May be omitted) Information about the most recent fully-validated ledger.
        /// If the most recent validated ledger is not available, the response omits this field and includes closed_ledger instead.
        /// </summary>
        [JsonProperty("validated_ledger")]
        public LedgerState ValidatedLedger { get; set; }

        /// <summary>
        ///	This is the baseline amount of server load used in transaction cost calculations.
        /// If the load_factor is equal to the load_base then only the base transaction cost is enforced.
        /// If the load_factor is higher than the load_base, then transaction costs are multiplied by the ratio between them.
        /// For example, if the load_factor is double the load_base, then transaction costs are doubled.
        /// </summary>
        [JsonProperty("load_base")]
        public uint LoadBase { get; set; }

        /// <summary>
        /// The load factor the server is currently enforcing.
        /// The ratio between this value and the load_base determines the multiplier for transaction costs.
        /// </summary>
        [JsonProperty("load_factor")]
        public uint LoadFactor { get; set; }

        /// <summary>
        /// Returns current transaction cost, in drops.
        /// </summary>
        /// <returns></returns>
        public uint GetFee()
        {
            return ((ValidatedLedger?.BaseFee ?? ClosedLedger.BaseFee) * LoadFactor) / (LoadBase != 0 ? LoadBase : 1);
        }
    }
}