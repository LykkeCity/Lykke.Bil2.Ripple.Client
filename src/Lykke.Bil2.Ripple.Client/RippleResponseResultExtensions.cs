namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Extensions for <see cref="IRippleResponseResult"/>
    /// </summary>
    public static class RippleResponseResultExtensions
    {
        /// <summary>
        /// Throws <see cref="RippleResponseResultErrorException"/> if <see cref="IRippleResponseResult.Status"/> indicates error.
        /// </summary>
        public static void ThrowIfError(this IRippleResponseResult self)
        {
            if (self.Error == "error")
            {
                throw new RippleResponseResultErrorException(self.Error, self.Request);
            }
        }
    }
}