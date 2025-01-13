using System.Text.Json;

namespace AutoServiceApp
{
    public class StateManager
    {
        public void SaveState(string filePath, object state)
        {
            var json = JsonSerializer.Serialize(state);
            File.WriteAllText(filePath, json);
        }

        public T LoadState<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}