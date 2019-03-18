using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Tx
{
    public class Meta
    {
        /// <summary>
        /// The transaction's position within the ledger that included it. This is zero-indexed.
        /// </summary>
        public uint? TransactionIndex { get; set; }

        /// <summary>
        /// A result code indicating whether the transaction succeeded or how it failed.
        /// </summary>
        public string TransactionResult { get; set; }

        /// <summary>
        /// The Currency Amount actually received by the Destination account.
        /// </summary>
        [JsonProperty("delivered_amount")]
        public Amount DeliveredAmount { get; set; }
    }
}