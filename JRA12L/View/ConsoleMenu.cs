namespace JRA12L.View;

public class ConsoleMenu : IMenuView
{
    private readonly ConsoleColor _defaultConsoleForegroundColor;
    private readonly ConsoleColor _defaultConsoleBackgroundColor;
        
    private const ConsoleColor SelectedMenuItemBackground = ConsoleColor.Black;
    private const ConsoleColor SelectedMenuItemForeground = ConsoleColor.White;
    private const ConsoleColor DefaultMenuItemBackground = ConsoleColor.Black;
    private const ConsoleColor DefaultMenuItemForeground = ConsoleColor.White;

    public ConsoleMenu()
    {
        _defaultConsoleForegroundColor = Console.ForegroundColor;
        _defaultConsoleBackgroundColor = Console.BackgroundColor;
            
        Console.ForegroundColor = DefaultMenuItemForeground;
        Console.BackgroundColor = DefaultMenuItemBackground;
        Console.CursorVisible = false;
        Console.Clear();
    }
    public void DrawMenu(string[] menuItems, int currentIndex)
    {
        Console.SetCursorPosition(0, 0);
        for(int i = 0; i < menuItems.Length; i++)
        {
            if(i != currentIndex)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(menuItems[i]);
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(menuItems[i]);
            }
        }
    }
    public void Dispose()
    {
        Console.ForegroundColor = _defaultConsoleForegroundColor;
        Console.BackgroundColor = _defaultConsoleBackgroundColor;
        Console.CursorVisible = true;
        Console.Clear();
    }
}