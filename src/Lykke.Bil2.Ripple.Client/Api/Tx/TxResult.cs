using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Tx
{
    public class TxResult : RippleResponseResultBase
    {
        /// <summary>
        /// The unique address of the account that initiated the transaction.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// The type of transaction.
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Integer amount of XRP, in drops, to be destroyed as a cost for distributing this transaction to the network.
        /// </summary>
        public string Fee { get; set; }

        /// <summary>
        /// Highest ledger index this transaction can appear in.
        /// </summary>
        public uint? LastLedgerSequence { get; set; }

        /// <summary>
        /// The sequence number, relative to the initiating account, of this transaction.
        /// </summary>
        public uint Sequence { get; set; }

        /// <summary>
        /// The SHA-512 hash of the transaction.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// Transaction timestamp in seconds since the Ripple Epoch.
        /// </summary>
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
        /// Various metadata about the transaction.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set;}

        /// <summary>
        /// The unique address of the account receiving the payment.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Arbitrary tag that identifies the reason for the payment to the destination, or a hosted recipient to pay.
        /// </summary>
        public uint? DestinationTag { get; set; }
    }
}