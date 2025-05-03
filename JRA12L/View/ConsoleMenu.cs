namespace JRA12L.View;

public class ConsoleMenu : IMenuView
{
    private readonly ConsoleColor _defaultConsoleForegroundColor;
    private readonly ConsoleColor _defaultConsoleBackgroundColor;
        
    private const ConsoleColor SelectedMenuItemBackground = ConsoleColor.White;
    private const ConsoleColor SelectedMenuItemForeground = ConsoleColor.Black;
    private const ConsoleColor DefaultMenuItemBackground = ConsoleColor.Black;
    private const ConsoleColor DefaultMenuItemForeground = ConsoleColor.White;

    private string _lastTitle = "";
    public ConsoleMenu()
    {
        _defaultConsoleForegroundColor = Console.ForegroundColor;
        _defaultConsoleBackgroundColor = Console.BackgroundColor;
            
        Console.ForegroundColor = DefaultMenuItemForeground;
        Console.BackgroundColor = DefaultMenuItemBackground;
        Console.CursorVisible = false;
        Console.Clear();
    }
    public void DrawMenu(string title, string[] menuItems, int currentIndex, Dictionary<string, string> controls)
    {
        if(_lastTitle != title)
        {
            Console.Clear();
        }
        _lastTitle = title;
        Console.SetCursorPosition(0, 0);
        Console.CursorVisible = false;
        Console.BackgroundColor = DefaultMenuItemBackground;
        Console.ForegroundColor = DefaultMenuItemForeground;
        Console.WriteLine(title);
        Console.WriteLine();
        for(int i = 0; i < menuItems.Length; i++)
        {
            if(i != currentIndex)
            {
                Console.BackgroundColor = DefaultMenuItemBackground;
                Console.ForegroundColor = DefaultMenuItemForeground;
                Console.WriteLine(menuItems[i]);
            }
            else
            {
                Console.BackgroundColor = SelectedMenuItemBackground;
                Console.ForegroundColor = SelectedMenuItemForeground;
                Console.WriteLine(menuItems[i]);
            }
        }
        Console.WriteLine();
        Console.WriteLine();
        foreach(var pair in controls)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" "+pair.Key+" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" "+pair.Value+"    ");
        }
        Console.Out.Flush();
    }
    public void Dispose()
    {
        Console.ForegroundColor = _defaultConsoleForegroundColor;
        Console.BackgroundColor = _defaultConsoleBackgroundColor;
        Console.CursorVisible = true;
        Console.Clear();
    }
}