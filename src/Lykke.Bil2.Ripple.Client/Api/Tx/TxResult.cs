using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Tx
{
    public class TxResult : Transaction, IRippleResponseResult
    {
        /// <summary>
        /// Transaction timestamp in seconds since the Ripple Epoch.
        /// </summary>
        [JsonProperty("date")]
        public long? Date { get; set; }

        /// <summary>
        /// The sequence number of the ledger that includes this transaction.
        /// </summary>
        [JsonProperty("ledger_index")]
        public uint? LedgerIndex { get; set; }

        /// <summary>
        /// True if this data is from a validated ledger version; if omitted or set to false, this data is not final.
        /// </summary>
        [JsonProperty("validated")]
        public bool? Validated { get; set; }

        /// <summary>
        /// See <see cref="IRippleResponseResult.Request"/>.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// See <see cref="IRippleResponseResult.Error"/>
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// See <see cref="IRippleResponseResult.Request"/>
        /// </summary>
        public object Request { get; set; }
    }
}