using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Submit
{
    /// <summary>
    /// Submit transaction request params.
    /// </summary>
    public class SubmitRequestParams
    {
        /// <summary>
        /// Initializes new instance of <see cref="SubmitRequestParams"/>.
        /// </summary>
        /// <param name="txBlob">Hex representation of the signed transaction to submit. </param>
        public SubmitRequestParams(string txBlob)
        {
            TxBlob = txBlob ?? throw new ArgumentNullException(nameof(txBlob));
        }

        /// <summary>
        /// Hex representation of the signed transaction to submit.
        /// </summary>
        [JsonProperty("tx_blob")]
        public string TxBlob { get; }
    }
}