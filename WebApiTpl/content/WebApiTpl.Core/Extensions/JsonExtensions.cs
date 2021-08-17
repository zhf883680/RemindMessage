using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApiTpl.Core.Extensions
{
    public class JsonExtensions
    {
        /// <summary>
        ///     json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToModel<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        ///     对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ModelToJson(object obj)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DatetimeJsonConverter());
            return JsonSerializer.Serialize(obj, options);
        }
    }

    /// <summary>
    /// 时间处理 防止前台出现2019-01-01T00:00:00
    /// </summary>
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                if (DateTime.TryParse(reader.GetString(), out var date))
                    return date;
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}