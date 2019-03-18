using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api.Submit
{
    public class SubmitResult : RippleResponseResultBase
    {
        /// <summary>
        /// Code indicating the preliminary result of the transaction, for example tesSUCCESS.
        /// </summary>
        [JsonProperty("engine_result")]
        public string EngineResult { get; set; }

        /// <summary>
        /// Numeric code indicating the preliminary result of the transaction, directly correlated to engine_result.
        /// </summary>
        [JsonProperty("engine_result_code")]
        public long EngineResultCode { get; set; }

        /// <summary>
        /// Human-readable explanation of the transaction's preliminary result.
        /// </summary>
        [JsonProperty("engine_result_message")]
        public string EngineResultMessage { get; set; }
    }
}