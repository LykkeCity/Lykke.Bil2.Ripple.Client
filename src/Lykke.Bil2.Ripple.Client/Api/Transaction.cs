using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api
{
    /// <summary>
    /// Transaction object.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// [Common] The SHA-512 hash of the transaction.
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// [Common] The unique address of the account that initiated the transaction.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// [Common] The type of transaction.
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// [Common] Integer amount of XRP, in drops, to be destroyed as a cost for distributing this transaction to the network.
        /// </summary>
        public string Fee { get; set; }

        /// <summary>
        /// [Common] The sequence number, relative to the initiating account, of this transaction.
        /// </summary>
        public uint Sequence { get; set; }

        /// <summary>
        /// Set of bit-flags for this transaction.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// [Common] Highest ledger index this transaction can appear in.
        /// </summary>
        public uint? LastLedgerSequence { get; set; }

        /// <summary>
        /// [Common] Various metadata about the transaction.
        /// </summary>
        [JsonProperty("meta")]
        public TransactionMetadata Metadata { get; set; }

        /// <summary>
        /// See <see cref="Metadata"/>.
        /// </summary>
        [JsonProperty("metaData")]
        public TransactionMetadata MetadataMirror
        {
            get => Metadata;
            set => Metadata = value;
        }

        /// <summary>
        /// [Payment] The amount of currency to deliver. For non-XRP amounts, the nested field names MUST be lower-case.
        /// If the tfPartialPayment flag is set, deliver up to this amount instead.
        /// </summary>
        public Amount Amount { get; set; }

        /// <summary>
        /// [Payment] The unique address of the account receiving the payment.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// [Payment] Arbitrary tag that identifies the reason for the payment to the destination, or a hosted recipient to pay.
        /// </summary>
        public uint? DestinationTag { get; set; }
    }
}