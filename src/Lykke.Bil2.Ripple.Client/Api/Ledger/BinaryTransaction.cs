using Newtonsoft.Json;
using Ripple.Core.Hashing;
using Ripple.Core.Types;
using Ripple.Core.Util;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Transaction data in binary (HEX) format.
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

        /// <summary>
        /// Parses binary data to a <see cref="Transaction"/> instance.
        /// </summary>
        /// <returns></returns>
        public Transaction Parse()
        {
            var txObject = StObject.FromHex(TxBlob);

            var tx = txObject
                .ToJson()
                .ToObject<Transaction>();

            tx.Hash = B16.Encode(Sha512.Half(txObject.ToBytes(), (uint)HashPrefix.TransactionId));

            tx.Metadata = StObject.FromHex(Meta)
                .ToJson()
                .ToObject<TransactionMetadata>();

            return tx;
        }
    }
}