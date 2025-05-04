namespace JRA12L.Infrastructure;

public static class SaveDeleter
{
    public static bool DeleteSave(string filename)
    {
        string path = Path.Combine(AppContext.BaseDirectory, SaveDirectoryReader.GameSaveDir, filename);
        if(!File.Exists(path))
        {
            return false;
        }
        try
        {
            File.Delete(path);
        }
        catch(Exception ex)
        {
            return false;
        }
        return true;
    }
}