using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoServiceApp
{
    public class StateManager
    {
        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter(), new UserConverter() }
            };
            return options;
        }

        public void SaveState(string filePath, object state)
        {
            var options = GetJsonSerializerOptions();
            var json = JsonSerializer.Serialize(state, options);
            File.WriteAllText(filePath, json);
        }

        public T LoadState<T>(string filePath)
        {
            var options = GetJsonSerializerOptions();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, options);
        }

        private class UserConverter : JsonConverter<User>
        {
            public override User Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
                {
                    JsonElement root = doc.RootElement;

                    string role = root.GetProperty("Role").GetString();
                    switch (role)
                    {
                        case "Admin":
                            return JsonSerializer.Deserialize<Administrator>(root.GetRawText(), options);
                        case "Mechanic":
                            return JsonSerializer.Deserialize<Mechanic>(root.GetRawText(), options);
                        default:
                            throw new NotSupportedException($"Role '{role}' is not supported");
                    }
                }
            }

            public override void Write(Utf8JsonWriter writer, User value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
            }
        }
    }
}