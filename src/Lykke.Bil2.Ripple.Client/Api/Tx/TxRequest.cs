namespace Lykke.Bil2.Ripple.Client.Api.Tx
{
    /// <summary>
    /// Single transaction information request.
    /// </summary>
    public class TxRequest : RippleRequestBase<TxRequestParams>
    {
        /// <summary>
        /// Initializes new instance of <see cref="TxRequest"/>.
        /// </summary>
        /// <param name="transaction">The 256-bit hash of the transaction, as hex.</param>
        /// <returns></returns>
        public TxRequest(string transaction) : base ("tx", new TxRequestParams(transaction))
        {
        }
    }
}