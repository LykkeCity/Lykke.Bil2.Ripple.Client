using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Base class for rippled API method results.
    /// </summary>
    public abstract class RippleResponseResultBase : IRippleResponseResult
    {
        /// <summary>
        /// See <see cref="IRippleResponseResult.Status"/>.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// See <see cref="IRippleResponseResult.Error"/>.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>
        /// See <see cref="IRippleResponseResult.Request"/>.
        /// </summary>
        [JsonProperty("request")]
        public object Request { get; set; }
    }
}