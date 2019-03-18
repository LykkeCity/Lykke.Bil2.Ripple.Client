namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Ledger information request.
    /// </summary>
    public class LedgerRequest : RippleRequestBase<LedgerRequestParams>
    {
        /// <summary>
        /// Initializes new instance of <see cref="LedgerRequest"/>.
        /// </summary>
        /// <param name="ledgerHash">The 256-bit hash of the transaction, as hex.</param>
        /// <returns></returns>
        public LedgerRequest(string ledgerHash) : base ("ledger", new LedgerRequestParams(ledgerHash))
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="LedgerRequest"/>.
        /// </summary>
        /// <param name="ledgerIndex">The sequence number of the ledger to use.</param>
        /// <returns></returns>
        public LedgerRequest(uint ledgerIndex) : base("ledger", new LedgerRequestParams(ledgerIndex))
        {
        }
    }
}