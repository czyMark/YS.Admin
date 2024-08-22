using System;
using System.Globalization;
using Newtonsoft.Json;

namespace YS.Admin.Entity
{
    public class StringJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long?) || objectType == typeof(long);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            // 读取字符串并尝试转换为 long
            string value = (string)reader.Value;
            if (long.TryParse(value, out long result))
            {
                return result;
            }

            throw new JsonSerializationException($"Unable to convert \"{value}\" to long.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }
    }
}