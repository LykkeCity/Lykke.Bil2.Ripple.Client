using System;

namespace Lykke.Bil2.Ripple.Client
{
    /// <summary>
    /// Ripple Epoch helpers.
    /// </summary>
    public static class RippleDateTime
    {
        /// <summary>
        /// Ripple Epoch started on January 1, 2000 at 00:00 UTC.
        /// </summary>
        public static readonly DateTime RippleEpoch = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts number of seconds since Ripple Epoch (2000-01-01T00:00:00Z) to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="seconds">Number of seconds since Ripple Epoch.</param>
        /// <returns></returns>
        public static DateTime FromRippleEpoch(this long seconds)
        {
            return RippleEpoch.AddSeconds(seconds);
        }

        /// <summary>
        /// Converts <see cref="DateTime"/> to number of seconds since Ripple Epoch (2000-01-01T00:00:00Z).
        /// </summary>
        /// <param name="time">Date and time.</param>
        /// <returns></returns>
        public static long ToRippleEpoch(this DateTime time)
        {
            return Convert.ToInt64((time - RippleEpoch).TotalSeconds);
        }
    }
}