using System.Threading.Tasks;
using Lykke.Bil2.Ripple.Client.Api.AccountInfo;
using Lykke.Bil2.Ripple.Client.Api.AccountLines;
using Lykke.Bil2.Ripple.Client.Api.Ledger;
using Lykke.Bil2.Ripple.Client.Api.ServerState;
using Lykke.Bil2.Ripple.Client.Api.Submit;
using Lykke.Bil2.Ripple.Client.Api.Tx;
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
        /// https://developers.ripple.com/account_lines.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Account trustlines.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<AccountLinesResult>> Post([Body] AccountLinesRequest body);

        /// <summary>
        /// https://developers.ripple.com/ledger.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Ledger data.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<LedgerResult>> Post([Body] LedgerRequest body);

        /// <summary>
        /// https://developers.ripple.com/server_state.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Server state.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<ServerStateResult>> Post([Body] ServerStateRequest body);

        /// <summary>
        /// https://developers.ripple.com/submit.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Submit state.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<SubmitResult>> Post([Body] SubmitRequest body);

        /// <summary>
        /// https://developers.ripple.com/tx.html
        /// </summary>
        /// <param name="body">Request body.</param>
        /// <returns>Transaction data.</returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<TxResult>> Post([Body] TxRequest body);
    }
}