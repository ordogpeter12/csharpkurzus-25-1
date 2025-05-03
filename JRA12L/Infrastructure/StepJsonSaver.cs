using System.Text.Json;

namespace JRA12L.Infrastructure;

public static class StepJsonSaver
{
    public static bool Save(List<JsonStepDto> steps, string fileName)
    {
        fileName += ".json";
        string path = Path.Combine(AppContext.BaseDirectory, "saves");
        try
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, fileName);
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(steps, options);
            File.WriteAllText(path, jsonString);
        }
        catch(Exception e)
        {
            return false;
        }
        return true;
    }
}