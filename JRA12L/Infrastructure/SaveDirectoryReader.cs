namespace JRA12L.Infrastructure;

public static class SaveDirectoryReader
{
    public const string GameSaveDir = "saves";
    
    public static string[] GetSaves()
    {
        string[] saves = [];
        string dir = Path.Combine(AppContext.BaseDirectory, GameSaveDir);
        if(!Directory.Exists(dir))
        {
            return [];
        }
        try
        {
            saves = Directory.GetFiles(dir);
        }
        catch (Exception ex)
        {
            return [];
        }
        for(int i = 0; i < saves.Length; i++)
        {
            saves[i] = Path.GetFileName(saves[i]);
        }
        return saves;
    }
}