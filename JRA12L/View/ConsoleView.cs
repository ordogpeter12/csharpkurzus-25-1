namespace JRA12L;

public class ConsoleView : IView
{
    private readonly ConsoleColor _defaultConsoleForegroundColor;
    private readonly ConsoleColor _defaultConsoleBackgroundColor;
    
    private const ConsoleColor GameFigureColor = ConsoleColor.Black;
    private const ConsoleColor GameBackgroundColor = ConsoleColor.Black;
    
    private const ConsoleColor BlackTileColor = ConsoleColor.DarkYellow;
    private const ConsoleColor WhiteTileColor = ConsoleColor.White;
    private const ConsoleColor PlayerLocationColor = ConsoleColor.DarkMagenta;
    private const ConsoleColor PossibleMoveColor = ConsoleColor.DarkGreen;

    public ConsoleView()
    {
        _defaultConsoleForegroundColor = Console.ForegroundColor;
        _defaultConsoleBackgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = GameFigureColor;
        Console.BackgroundColor = GameBackgroundColor;
        Console.CursorVisible = false;
        Console.Clear();
    }

    public void Dispose()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Disposed!");
        Console.ForegroundColor = _defaultConsoleForegroundColor;
        Console.BackgroundColor = _defaultConsoleBackgroundColor;
        Console.CursorVisible = true;
        Console.Clear();
    }

    public void Draw(IStep step, Coordinates playerCoordinates, List<Coordinates>? reverseOrderedPossibleMoves = null)
    {
        reverseOrderedPossibleMoves ??= [];
        Console.SetCursorPosition(0, 0);
        for(byte i = 0; i < step.GetYAxisLenght(); i++)
        {
            for(byte j = 0; j < step.GetXAxisLenght(); j++)
            {
                SetConsoleBackgroundColor(j, i, playerCoordinates, reverseOrderedPossibleMoves);
                Console.Write(step[j, i]);
                Console.Out.Flush();
            }
            Console.BackgroundColor = GameBackgroundColor;
            Console.Write('\n');
        }
    }
    private static void SetConsoleBackgroundColor(int xCoordinate, int yCoordinate, Coordinates playerCoordinates, List<Coordinates> possibleMoves)
    {
        if(playerCoordinates.X == xCoordinate && playerCoordinates.Y == yCoordinate)
        {
            Console.BackgroundColor = PlayerLocationColor;
            //Needed to always clear passed Coordinates
            if(possibleMoves.Count > 0 && playerCoordinates.X == possibleMoves.LastOrDefault().X && playerCoordinates.Y == possibleMoves.LastOrDefault().Y)
                possibleMoves.RemoveAt(possibleMoves.Count-1);
        }
        else if(possibleMoves.Count > 0 && possibleMoves.LastOrDefault().X == xCoordinate && possibleMoves.LastOrDefault().Y == yCoordinate)
        {
            possibleMoves.RemoveAt(possibleMoves.Count-1);
            Console.BackgroundColor = PossibleMoveColor;
        }
        else
        {
            Console.BackgroundColor = (xCoordinate+yCoordinate)%2 == 0 ? WhiteTileColor : BlackTileColor;
        }
    }
}