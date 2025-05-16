namespace JRA12L.Infrastructure;

public static class ReadTextFile
{
    private const string TextSources = "text_sources";
    public static string[] ReadRows(string filename)
    {
        string path = Path.Combine(AppContext.BaseDirectory, TextSources, filename);
        if(!File.Exists(path))
        {
            throw new FileNotFoundException();
        }
        return File.ReadAllLines(path);
    }
}