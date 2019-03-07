namespace Lykke.Bil2.Ripple.Client.Api.AccountInfo
{
    /// <summary>
    /// Account info request.
    /// </summary>
    /// <typeparam name="AccountInfoRequestParams"></typeparam>
    public class AccountInfoRequest : RippleRequestBase<AccountInfoRequestParams>
    {
        /// <summary>
        /// Initializes new instance of <see cref="AccountInfoRequest"/>.
        /// </summary>
        /// <param name="account">Address of Ripple account to return account info.</param>
        /// <returns></returns>
        public AccountInfoRequest(string account) : base("account_info", new AccountInfoRequestParams(account))
        {
        }
    }
}