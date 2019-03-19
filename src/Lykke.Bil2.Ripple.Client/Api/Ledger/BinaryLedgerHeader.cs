using Newtonsoft.Json;
using Ripple.Core.Binary;
using Ripple.Core.Ledger;
using Ripple.Core.Types;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Header data of this ledger.
    /// </summary>
    public class BinaryLedgerHeader
    {
        /// <summary>
        /// Whether or not this ledger has been closed.
        /// </summary>
        [JsonProperty("closed")]
        public bool Closed { get; set; }

        /// <summary>
        /// Ledger data in binary (HEX) format.
        /// </summary>
        [JsonProperty("ledger_data")]
        public string LedgerData { get; set; }

        /// <summary>
        /// Transactions applied in this ledger version.
        /// </summary>
        [JsonProperty("transactions")]
        public BinaryTransaction[] Transactions { get; set; }

        /// <summary>
        /// Parses binary data to a <see cref="Header"/> instance.
        /// </summary>
        /// <returns></returns>
        public Header Parse()
        {
            var header = LedgerHeader.FromReader(new StReader(new BufferParser(LedgerData)));

            return new Header
            {
                CloseTime = header.CloseTime.Value,
                ParentCloseTime = header.ParentCloseTime.Value,
                ParentHash = header.ParentHash.ToString(),
                TotalCoins = header.TotalDrops.Value
            };
        }
    }
}