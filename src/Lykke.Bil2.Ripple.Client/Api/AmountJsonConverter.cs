using System.Globalization;
using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client.Api
{
    public class AmountJsonConverter : JsonConverter<Amount>
    {
        public override Amount ReadJson(JsonReader reader, Type objectType, Amount existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // XRP formatted as integer string (in drops).
            // All other currencies formatted as Amount object.

            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;

                case JsonToken.StartObject:
                    return serializer.Deserialize<Amount>(reader);

                case JsonToken.String:
                    var drops = decimal.Parse((string)serializer.Deserialize(reader), CultureInfo.InvariantCulture);
                    var coins = drops / 1_000_000;
                    return new Amount
                    {
                        Currency = "XRP",
                        Value = coins.ToString("F6", CultureInfo.InvariantCulture)
                    };

                default:
                    throw new InvalidOperationException("Invalid amount format, must be either XRP drops string or Amount object");
            }
        }

        public override void WriteJson(JsonWriter writer, Amount value, JsonSerializer serializer)
        {
            // XRP formatted as integer string (in drops).
            // All other currencies formatted as Amount object.

            if (value.Currency == "XRP")
                serializer.Serialize(writer, decimal.ToInt64(decimal.Parse(value.Value, CultureInfo.InvariantCulture) * 1_000_000).ToString("D"));
            else
                serializer.Serialize(writer, value);
        }
    }
}