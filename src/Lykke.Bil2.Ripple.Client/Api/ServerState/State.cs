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
        /// Range expression indicating the sequence numbers of the ledger versions the local rippled has in its database.abstract
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
        public Ledger ClosedLedger { get; set; }

        /// <summary>
        /// (May be omitted) Information about the most recent fully-validated ledger.
        /// If the most recent validated ledger is not available, the response omits this field and includes closed_ledger instead.
        /// </summary>
        [JsonProperty("validated_ledger")]
        public Ledger ValidatedLedger { get; set; }
    }
}