using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Base class for rippled API requests with parameters.
    /// </summary>
    /// <typeparam name="TParams">Type of request parameters. Must be a reference type.</typeparam>
    public abstract class RippleRequestBase<TParams> : RippleRequestBase
        where TParams : class
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RippleRequestBase"/>.
        /// </summary>
        /// <param name="method">Requested rippled method name.</param>
        /// <param name="params">Request parameters.</param>
        protected RippleRequestBase(string method, TParams @params) : base(method)
        {
            Params = new []
            {
                @params ?? throw new ArgumentNullException(nameof(@params))
            };
        }

        /// <summary>
        /// Returns request parameters (always consists of single element containing paramaters as fields).
        /// </summary>
        [JsonProperty("params")]
        public IReadOnlyCollection<TParams> Params { get; }
    }

    /// <summary>
    /// Base class for parameterless rippled API requests.
    /// </summary>
    public abstract class RippleRequestBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RippleRequestBase"/>.
        /// </summary>
        /// <param name="method">Requested rippled method name.</param>
        protected RippleRequestBase(string method)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        /// <summary>
        /// Returns requested rippled method name.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; }
    }
}