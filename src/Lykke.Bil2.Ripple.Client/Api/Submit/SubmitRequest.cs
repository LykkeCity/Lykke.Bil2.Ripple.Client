namespace Lykke.Bil2.Ripple.Client.Api.Submit
{
    /// <summary>
    /// Sumbit transaction request.
    /// </summary>
    public class SubmitRequest : RippleRequestBase<SubmitRequestParams>
    {
        /// <summary>
        /// Initializes new instance of <see cref="SubmitRequest"/>.
        /// </summary>
        public SubmitRequest(string txBlob) : base("submit", new SubmitRequestParams(txBlob))
        {
        }
    }
}