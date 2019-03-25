using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Thrown by <see cref="RippleResponseResultExtensions.ThrowIfError"/> method if rippled API request result indicates error.
    /// </summary>
    public class RippleResponseResultErrorException : Exception
    {
        /// <summary>
        /// Initializes new instance of <see cref="RippleResponseResultErrorException"/>.
        /// </summary>
        /// <param name="error">A unique code for the type of error that occurred.</param>
        /// <param name="request">A copy of the request that prompted this error.</param>
        public RippleResponseResultErrorException(string error, object request) :
            base($"Ripple request failed. Error: {error}. Request: {JsonConvert.SerializeObject(request)}")
        {
        }
    }
}