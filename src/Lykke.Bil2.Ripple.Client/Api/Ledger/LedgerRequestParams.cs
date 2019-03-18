using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Ledger information request parameters.
    /// </summary>
    public class LedgerRequestParams
    {
        /// <summary>
        /// Initializes new instance of <see cref="LedgerRequestParams"/>.
        /// </summary>
        /// <param name="ledgerHash">A 20-byte hex string for the ledger version to use.</param>
        public LedgerRequestParams(string ledgerHash)
        {
            LedgerHash = ledgerHash ?? throw new ArgumentNullException(nameof(ledgerHash));
        }

        /// <summary>
        /// Initializes new instance of <see cref="LedgerRequestParams"/>.
        /// </summary>
        /// <param name="ledgerIndex">The sequence number of the ledger to use.</param>
        public LedgerRequestParams(uint ledgerIndex)
        {
            if (ledgerIndex == 0)
                throw new ArgumentOutOfRangeException(nameof(ledgerIndex), "Ledger index must be greater than 0");

            LedgerIndex = ledgerIndex;
        }

        /// <summary>
        /// A 20-byte hex string for the ledger version to use.
        /// </summary>
        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; }

        /// <summary>
        /// The sequence number of the ledger to use.
        /// </summary>
        [JsonProperty("ledger_index")]
        public uint? LedgerIndex { get; }

        /// <summary>
        /// If true, return information on transactions in the specified ledger version. Currently always true.
        /// </summary>
        [JsonProperty("transactions")]
        public bool Transactions { get; } = true;
    }
}