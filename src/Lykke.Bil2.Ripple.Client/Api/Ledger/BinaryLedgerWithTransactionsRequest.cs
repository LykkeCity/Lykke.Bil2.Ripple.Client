namespace Lykke.Bil2.Ripple.Client.Api.Ledger
{
    /// <summary>
    /// Ledger information request.
    /// Ledger info returned in binary (HEX) format with expanded transactions.
    /// </summary>
    public class BinaryLedgerWithTransactionsRequest :
        RippleRequestBase<BinaryLedgerWithTransactionsRequestParams>
    {
        /// <summary>
        /// Initializes new instance of <see cref="BinaryLedgerWithTransactionsRequest"/>.
        /// </summary>
        /// <param name="ledgerIndex">The sequence number of the ledger to use.</param>
        /// <returns></returns>
        public BinaryLedgerWithTransactionsRequest(uint ledgerIndex) :
            base("ledger", new BinaryLedgerWithTransactionsRequestParams(ledgerIndex))
        {
        }
    }
}