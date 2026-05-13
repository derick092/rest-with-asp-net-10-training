using System.Text.Json;
using System.Text.Json.Serialization;

namespace RWAN10T.Api.JsonSerializer
{
    public class DateSerializer : JsonConverter<DateTime>
    {
        private readonly string _format = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, 
            Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out DateTime date)) return date;

            return new DateTime();
        }

        public override void Write(Utf8JsonWriter writer, 
            DateTime value, JsonSerializerOptions options)
        {
#pragma warning disable CS8073 // O resultado da expressão é sempre o mesmo, pois um valor deste tipo nunca é 'null' 
            if (value != null)
            {
                writer.WriteStringValue(value.ToString(_format));
            }
            else 
            {
                writer.WriteNullValue();
            }
#pragma warning restore CS8073 // O resultado da expressão é sempre o mesmo, pois um valor deste tipo nunca é 'null' 
        }
    }
}
