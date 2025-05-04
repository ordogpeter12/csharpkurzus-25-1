using System.Text.Json;

using JRA12L.Core.ChessGame;

namespace JRA12L.Infrastructure;

public static class JsonStepLoader
{
    public static List<JsonStepDto> LoadJsonSteps(string filename)
    {
        List<JsonStepDto> stepsDto;
        string path = Path.Combine(AppContext.BaseDirectory, SaveDirectoryReader.GameSaveDir, filename);
        if(!File.Exists(path))
        {
            return [];
        }
        try
        {
            using FileStream stream = File.OpenRead(path);
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
            stepsDto = JsonSerializer.Deserialize<List<JsonStepDto>>(stream, options) ?? [];
        }
        catch (Exception e)
        {
            return [];
        }
        return stepsDto;
    }
}