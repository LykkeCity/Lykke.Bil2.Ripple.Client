namespace Lykke.Bil2.Ripple.Client.Api.AccountLines
{
    public class Line
    {
        /// <summary>
        /// The unique address of the counterparty to this trust line.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Representation of the numeric balance currently held against this line.
        /// </summary>
        public string Balance { get; set; }

        /// <summary>
        /// A currency code identifying what currency this trust line can hold.
        /// </summary>
        public string Currency { get; set; }
    }
}