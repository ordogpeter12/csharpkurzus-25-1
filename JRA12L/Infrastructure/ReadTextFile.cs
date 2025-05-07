namespace JRA12L.Infrastructure;

public static class ReadTextFile
{
    private const string TextSources = "text_sources";
    private static string[] FileContent = 
    [
        "w: up",
        "s: down",
        "a: left",
        "d: right",
        "Enter: select figure",
        "Enter(while figure is selected): step with figure",
        "z: end game(without save)",
        "z(while figure is selected): cancel selection",
        "m: save",
        "j: undo one move",
        "l: redo one move"
    ];
    /*public static string[] ReadRows(string filename)
    {
        string path = Path.Combine(AppContext.BaseDirectory, TextSources, filename);
        if(!File.Exists(path))
        {
            throw new FileNotFoundException();
        }
        return File.ReadAllLines(path);
    }*/
    
    
    //text file is not committed to the repo
    public static string[] ReadRows(string filename)
    {
        return FileContent;
    }
}