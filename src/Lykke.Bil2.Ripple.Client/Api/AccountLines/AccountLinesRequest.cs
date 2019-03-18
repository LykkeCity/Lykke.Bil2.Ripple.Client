namespace Lykke.Bil2.Ripple.Client.Api.AccountLines
{
    /// <summary>
    /// Account trustlines request.
    /// </summary>
    public class AccountLinesRequest : RippleRequestBase<AccountLinesRequestParams>
    {
        /// <summary>
        /// Initializes new instance of <see cref="AccountLinesRequest"/>.
        /// </summary>
        /// <param name="account">Address of Ripple account to return account info.</param>
        /// <returns></returns>
        public AccountLinesRequest(string account) : base("account_lines", new AccountLinesRequestParams(account))
        {
        }

        /// <summary>
        /// Limit the number of transactions to retrieve.
        /// </summary>
        public uint Limit { get; } = 400;
    }
}