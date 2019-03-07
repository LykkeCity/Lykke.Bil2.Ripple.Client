using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Base class for rippled API method results.
    /// </summary>
    public abstract class RippleResponseResultBase
    {
        /// <summary>
        /// The value "success" indicates the request was successfully received and understood by the server,
        /// "error" if the request caused an error.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// A unique code for the type of error that occurred.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}