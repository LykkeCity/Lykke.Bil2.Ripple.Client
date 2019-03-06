using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Base class for rippled API request body.
    /// </summary>
    /// <typeparam name="TParams">Type of request parameters. Must be a reference type.</typeparam>
    public abstract class RippleRequestBase<TParams, TResult>
        where TParams : class
        where TResult : RippleRequestResultBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RippleRequestBase"/>.
        /// </summary>
        /// <param name="method">Requested rippled method name.</param>
        /// <param name="@params">Request paramaters.</param>
        public RippleRequestBase(string method, TParams @params = null)
        {
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Params = new []
            {
                @params ?? default(TParams)
            };
        }

        /// <summary>
        /// Returns requested rippled method name.
        /// </summary>
        /// <value></value>
        public string Method { get; }

        /// <summary>
        /// Returns request parameters (always consists of single element containing paramaters as fields).
        /// </summary>
        /// <value></value>
        public IReadOnlyCollection<TParams> Params { get; }
    }
}