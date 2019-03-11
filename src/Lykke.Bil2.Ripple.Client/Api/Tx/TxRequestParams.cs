using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Tx
{
    /// <summary>
    /// Single transaction information request parameters.
    /// </summary>
    public class TxRequestParams
    {
        /// <summary>
        /// Initializes new instance of <see cref="TxRequestParams"/>.
        /// </summary>
        /// <param name="transaction">The 256-bit hash of the transaction, as hex.</param>
        public TxRequestParams(string transaction)
        {
            Transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        /// <summary>
        /// The 256-bit hash of the transaction, as hex.
        /// </summary>
        /// <value></value>
        [JsonProperty("transaction")]
        public string Transaction { get; }
    }
}