using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Transaction data.
    /// </summary>
    public class BinaryTransaction
    {
        /// <summary>
        /// Transaction metadata in binary (HEX) format.
        /// </summary>
        [JsonProperty("meta")]
        public string Meta { get; set; }

        /// <summary>
        /// Transaction content in binary (HEX) format.
        /// </summary>
        [JsonProperty("tx_blob")]
        public string TxBlob { get; set; }
    }
}