namespace JRA12L;

public class ConsoleView : IView
{
    private const char DifferenceBetweenFigureAndCharRepresantation = '\u2633';
    private readonly ConsoleColor _defaultConsoleForegroundColor;
    private readonly ConsoleColor _defaultConsoleBackgroundColor;
    
    private const ConsoleColor GameFigureColor = ConsoleColor.Black;
    private const ConsoleColor GameBackgroundColor = ConsoleColor.Black;
    
    private const ConsoleColor BlackTileColor = ConsoleColor.DarkYellow;
    private const ConsoleColor WhiteTileColor = ConsoleColor.White;
    private const ConsoleColor PlayerLocationColor = ConsoleColor.DarkMagenta;

    public ConsoleView()
    {
        _defaultConsoleForegroundColor = Console.ForegroundColor;
        _defaultConsoleBackgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = GameFigureColor;
        Console.BackgroundColor = GameBackgroundColor;
        Console.CursorVisible = false;
        Console.Clear();
    }
    public void Draw(IStep step, Coordinates playerCoordinates)
    {
        Console.SetCursorPosition(0, 0);
        for(int i = 0; i < step.GetYAxisLenght(); i++)
        {
            for(int j = 0; j < step.GetXAxisLenght(); j++)
            {
                SetConsoleBackgroundColor(j, i, playerCoordinates);
                Console.Write(ConvertToTile(step[i*step.GetXAxisLenght() + j]));
                Console.Out.Flush();
            }
            Console.BackgroundColor = GameBackgroundColor;
            Console.Write('\n');
        }
    }

    private static string ConvertToTile(IStep.Figure figure)
    {
        if(figure == IStep.Figure.BlankTile)
        {
            return "   ";
        }
        return $" {(char)(DifferenceBetweenFigureAndCharRepresantation + (char)figure)} ";
    }

    private static void SetConsoleBackgroundColor(int xCoordinate, int yCoordinate, Coordinates playerCoordinates)
    {
        if(playerCoordinates.X == xCoordinate && playerCoordinates.Y == yCoordinate)
        {
            Console.BackgroundColor = PlayerLocationColor;
        }
        else
        {
            Console.BackgroundColor = (xCoordinate+yCoordinate)%2 == 0 ? WhiteTileColor : BlackTileColor;
        }
    }
}