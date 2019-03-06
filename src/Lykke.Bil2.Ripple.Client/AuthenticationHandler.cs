using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lykke.Bil2.Ripple.Client
{
    internal class AuthenticationHandler : DelegatingHandler
    {
        readonly string _scheme;
        readonly string _accessToken;

        internal AuthenticationHandler(string scheme, string accessToken)
        {
            _scheme = scheme ??
                throw new ArgumentNullException(nameof(scheme));

            _accessToken = accessToken ??
                throw new ArgumentNullException(nameof(accessToken));
        }

        /// <summary>
        /// Sends request.
        /// </summary>
        /// <param name="request">Request message.</param>
        /// <param name="cancellationToken">Task cancellation token.</param>
        /// <returns>Http response.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(_scheme, _accessToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}