namespace Lykke.Bil2.Ripple.Client.Api.ServerState
{
    /// <summary>
    /// Server state request.
    /// </summary>
    public class ServerStateRequest : RippleRequestBase
    {
        /// <summary>
        /// Initializes new instance of <see cref="ServerStateRequest"/>.
        /// </summary>
        public ServerStateRequest() : base("server_state")
        {
        }
    }
}