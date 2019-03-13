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

        /// <summary>
        /// A copy of the request that prompted this error, in JSON format.
        /// </summary>
        [JsonProperty("request")]
        public object Request { get; set; }

        /// <summary>
        /// Throws <see cref="RippleResponseResultErrorException"/> if <see cref="Status"/> indicates error.
        /// </summary>
        public void ThrowIfError()
        {
            if (Status == "error")
            {
                throw new RippleResponseResultErrorException(Error, Request);
            }
        }
    }
}