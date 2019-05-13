using System.Linq;
using Newtonsoft.Json;
using Ripple.Core.Binary;
using Ripple.Core.Ledger;
using Ripple.Core.Types;

namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Ledger data in binary (HEX) format.
    /// </summary>
    public class BinaryLedger
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
        /// Parses binary data to a <see cref="Ledger"/> instance.
        /// </summary>
        /// <returns></returns>
        public Ledger Parse(bool headerOnly = false)
        {
            var header = LedgerHeader.FromReader(new StReader(new BufferParser(LedgerData)));

            return new Ledger
            {
                CloseTime = header.CloseTime?.Value,
                ParentCloseTime = header.ParentCloseTime?.Value,
                ParentHash = header.ParentHash?.ToString(),
                Transactions = !headerOnly
                    ? Transactions?.Select(x => x.Parse()).ToArray()
                    : null
            };
        }
    }
}