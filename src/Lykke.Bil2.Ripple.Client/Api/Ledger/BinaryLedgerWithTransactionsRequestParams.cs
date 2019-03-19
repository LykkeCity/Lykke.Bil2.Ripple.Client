using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Ledger information request parameters.
    /// Ledger info returned in binary (HEX) format with expanded transactions.
    /// </summary>
    public class BinaryLedgerWithTransactionsRequestParams
    {
        /// <summary>
        /// Initializes new instance of <see cref="BinaryLedgerWithTransactionsRequestParams"/>.
        /// </summary>
        /// <param name="ledgerIndex">The sequence number of the ledger to use.</param>
        public BinaryLedgerWithTransactionsRequestParams(uint ledgerIndex)
        {
            if (ledgerIndex == 0)
                throw new ArgumentOutOfRangeException(nameof(ledgerIndex), "Ledger index must be greater than 0");

            LedgerIndex = ledgerIndex;
        }

        /// <summary>
        /// The sequence number of the ledger to use.
        /// </summary>
        [JsonProperty("ledger_index")]
        public uint LedgerIndex { get; }

        /// <summary>
        /// If true, return information on transactions in the specified ledger version.
        /// </summary>
        [JsonProperty("transactions")]
        public bool Transactions { get; } = true;

        /// <summary>
        /// Provide full JSON-formatted information for transaction/account information instead of only hashes. 
        /// Always true for <see cref="BinaryLedgerWithTransactionsRequest"/>.
        /// </summary>
        [JsonProperty("expand")]
        public bool Expand { get; } = true;

        /// <summary>
        /// If true, and transactions and expand are both also true, return transaction information in binary format (hexadecimal string) instead of JSON format.
        /// Always true for <see cref="BinaryLedgerWithTransactionsRequest"/>.
        /// </summary>
        [JsonProperty("binary")]
        public bool Binary { get; } = true;
    }
}