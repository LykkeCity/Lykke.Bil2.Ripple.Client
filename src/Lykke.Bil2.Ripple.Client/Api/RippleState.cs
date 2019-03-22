namespace Lykke.Bil2.Ripple.Client.Api
{
    /// <summary>
    /// Links two accounts, tracking the balance of one currency between them.
    /// The concept of a trust line is an abstraction of this object type.
    /// </summary>
    public class RippleState
    {
        /// <summary>
        /// A bit-map of boolean flags enabled for this object.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// The balance of the trust line, from the perspective of the low account.
        /// </summary>
        public Amount Balance { get; set; }

        /// <summary>
        /// The limit that the low account has set on the trust line.
        /// The issuer is the address of the low account that set this limit.
        /// </summary>
        public Amount LowLimit { get; set; }

        /// <summary>
        /// The limit that the high account has set on the trust line.
        /// The issuer is the address of the high account that set this limit.
        /// </summary>
        public Amount HighLimit { get; set; }
    }
}