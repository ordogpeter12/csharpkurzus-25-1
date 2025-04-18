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
    public void Draw(IStep step, Coordinates playerCoordinates, List<Coordinates>? orderedPossibleMoves = null)
    {
        orderedPossibleMoves ??= [];
        Console.SetCursorPosition(0, 0);
        for(byte i = 0; i < step.GetYAxisLenght(); i++)
        {
            for(byte j = 0; j < step.GetXAxisLenght(); j++)
            {
                SetConsoleBackgroundColor(j, i, playerCoordinates, orderedPossibleMoves);
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
            if(possibleMoves.Count > 0 && playerCoordinates.X == possibleMoves[0].X && playerCoordinates.Y == possibleMoves[0].Y)
                possibleMoves.RemoveAt(0);
        }
        else if(possibleMoves.Count > 0 && possibleMoves[0].X == xCoordinate && possibleMoves[0].Y == yCoordinate)
        {
            possibleMoves.RemoveAt(0);
            Console.BackgroundColor = PossibleMoveColor;
        }
        else
        {
            Console.BackgroundColor = (xCoordinate+yCoordinate)%2 == 0 ? WhiteTileColor : BlackTileColor;
        }
    }
}