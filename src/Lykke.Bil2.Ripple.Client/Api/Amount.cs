using Lykke.Numerics;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api
{
    /// <summary>
    /// Currency amount.
    /// </summary>
    [JsonConverter(typeof(AmountJsonConverter))]
    public class Amount
    {
        /// <summary>
        /// Arbitrary code for currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Unique account address of the entity issuing the currency.
        /// </summary>
        [JsonProperty("issuer")]
        public string Counterparty { get; set; }

        /// <summary>
        /// Quoted decimal representation of the amount of currency.
        /// </summary>
        [JsonProperty("value")]
        public Money Value { get; set; }
    }
}