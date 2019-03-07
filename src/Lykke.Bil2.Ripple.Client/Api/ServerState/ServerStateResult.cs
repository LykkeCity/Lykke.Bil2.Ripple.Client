using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.ServerState
{
    /// <summary>
    /// Result of "server_state" method.
    /// </summary>
    public class ServerStateResult : RippleResponseResultBase
    {
        /// <summary>
        /// Server state.
        /// </summary>
        [JsonProperty("state")]
        public State State { get; set; }
    }
}