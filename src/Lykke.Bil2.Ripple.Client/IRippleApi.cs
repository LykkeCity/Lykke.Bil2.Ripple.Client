using System.Threading.Tasks;
using Refit;

namespace Lykke.Bil2.Ripple.Client
{
    public interface IRippleApi
    {
        /// <summary>
        /// https://developers.ripple.com/account_info.html
        /// </summary>
        /// <param name="body">Request body </param>
        /// <returns></returns>
        /// <exception cref="ApiException">Any HTTP-related error</exception>
        [Post("/")]
        Task<RippleResponse<TResult>> Request<TParams, TResult>([Body] RippleRequestBase<TParams, TResult> body)
            where TParams : class
            where TResult : RippleRequestResultBase;
    }
}