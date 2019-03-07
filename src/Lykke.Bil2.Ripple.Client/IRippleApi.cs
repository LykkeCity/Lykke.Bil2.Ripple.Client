using System.Threading.Tasks;
using Lykke.Bil2.Ripple.Client.Api.AccountInfo;
using Lykke.Bil2.Ripple.Client.Api.ServerState;
using Refit;

namespace Lykke.Bil2.Ripple.Client
{
    public interface IRippleApi
    {
        /// <summary>
        /// https://developers.ripple.com/account_info.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Account info.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<AccountInfoResult>> Post([Body] AccountInfoRequest body);

        /// <summary>
        /// https://developers.ripple.com/server_state.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Server state.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<ServerStateResult>> Post([Body] ServerStateRequest body);
    }
}