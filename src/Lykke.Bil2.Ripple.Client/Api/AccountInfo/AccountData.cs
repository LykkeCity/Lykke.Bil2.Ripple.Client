namespace Lykke.Bil2.Ripple.Client.Api.AccountInfo
{
    public class AccountData
    {
        /// <summary>
        /// The identifying address of this account.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// The account's current XRP balance in drops, represented as a string.
        /// </summary>
        public string Balance { get; set; }

        /// <summary>
        /// A bit-map of boolean flags enabled for this account.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// The sequence number of the next valid transaction for this account.
        /// </summary>
        public uint Sequence { get; set; }

        /// <summary>
        /// Returns true if account requires incoming payments to specify a Destination Tag.
        /// </summary>
        public bool IsDestinationTagRequired
        {
            get => (Flags & 0x00020000) == 0x00020000;
        }
    }
}