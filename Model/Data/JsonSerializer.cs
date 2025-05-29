using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Model.Core;

namespace Model.Data
{
    public class JsonSerializer : SerializerBase
    {
        private readonly JsonSerializerOptions _options;

        public JsonSerializer()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //Converters = { new ITProductJsonConverter() }
                Converters = { new PolymorphicConverter<ITProduct>() }
            };
        }

        public override string Serialize<T>(T obj)
        {

            return System.Text.Json.JsonSerializer.Serialize(obj, _options);
        }

        public override T Deserialize<T>(string data)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(data, _options);
        }
    }

    public class ITProductJsonConverter : JsonConverter<ITProduct>
    {
        public override ITProduct Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                if (!root.TryGetProperty("Type", out var typeElement))
                    throw new System.Text.Json.JsonException("Missing Type discriminator");

                string type = typeElement.GetString();
                return type switch
                {
                    "Laptop" => System.Text.Json.JsonSerializer.Deserialize<Laptop>(root.GetRawText(), options),
                    "Smartphone" => System.Text.Json.JsonSerializer.Deserialize<Smartphone>(root.GetRawText(), options),
                    "Tablet" => System.Text.Json.JsonSerializer.Deserialize<Tablet>(root.GetRawText(), options),
                    _ => throw new System.Text.Json.JsonException($"Unknown type discriminator: {type}")
                };
            }
        }


        public override void Write(Utf8JsonWriter writer, ITProduct value, JsonSerializerOptions options)
        {
            // Добавляем тип в JSON для правильной десериализации
            var json = System.Text.Json.JsonSerializer.Serialize(value, value.GetType(), options);
            writer.WriteRawValue(json);
        }

    }

    public class PolymorphicConverter<T> : JsonConverter<T> where T : class
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var root = jsonDoc.RootElement;

            if (typeof(T) == typeof(ITProduct))
            {
                var typeDiscriminator = root.GetProperty("$type").GetString();
                return typeDiscriminator switch
                {
                    "laptop" => System.Text.Json.JsonSerializer.Deserialize<Laptop>(root.GetRawText(), options) as T,
                    "smartphone" => System.Text.Json.JsonSerializer.Deserialize<Smartphone>(root.GetRawText(), options) as T,
                    "tablet" => System.Text.Json.JsonSerializer.Deserialize<Tablet>(root.GetRawText(), options) as T,
                    _ => throw new JsonException($"Unknown type discriminator: {typeDiscriminator}")
                };
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(root.GetRawText(), options);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value is ITProduct product)
            {
                var type = product switch
                {
                    Laptop _ => "laptop",
                    Smartphone _ => "smartphone",
                    Tablet _ => "tablet",
                    _ => throw new JsonException($"Unknown type: {product.GetType().Name}")
                };

                writer.WriteStartObject();
                writer.WriteString("$type", type);

                foreach (var prop in value.GetType().GetProperties())
                {
                    if (prop.Name == "$type") continue;
                    writer.WritePropertyName(JsonNamingPolicy.CamelCase.ConvertName(prop.Name));
                    System.Text.Json.JsonSerializer.Serialize(writer, prop.GetValue(value), options);
                }

                writer.WriteEndObject();
            }
            else
            {
                System.Text.Json.JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}
