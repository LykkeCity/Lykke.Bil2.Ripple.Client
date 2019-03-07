using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Rippled API method response.
    /// </summary>
    /// <typeparam name="TResult">Type of method result.</typeparam>
    public sealed class RippleResponse<TResult> where TResult : RippleResponseResultBase
    {
        /// <summary>
        /// Gets or sets rippled API method result.
        /// </summary>
        [JsonProperty("result")]
        public TResult Result { get; set; }
    }
}