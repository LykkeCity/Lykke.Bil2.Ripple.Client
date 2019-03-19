using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Base class for rippled API method results.
    /// </summary>
    public interface IRippleResponseResult
    {
        /// <summary>
        /// The value "success" indicates the request was successfully received and understood by the server,
        /// "error" if the request caused an error.
        /// </summary>
        [JsonProperty("status")]
        string Status { get; set; }

        /// <summary>
        /// A unique code for the type of error that occurred.
        /// </summary>
        [JsonProperty("error")]
        string Error { get; set; }

        /// <summary>
        /// A copy of the request that prompted this error, in JSON format.
        /// </summary>
        [JsonProperty("request")]
        object Request { get; set; }
    }
}