using JRA12L.Core;
using JRA12L.Core.ChessGame;
using JRA12L.Model;

namespace JRA12L.View;

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
    private const ConsoleColor CheckColor = ConsoleColor.Red;

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
        Console.ForegroundColor = _defaultConsoleForegroundColor;
        Console.BackgroundColor = _defaultConsoleBackgroundColor;
        Console.CursorVisible = true;
        Console.Clear();
    }

    public void Draw(IStep step, Coordinates playerCoordinates, 
        List<Coordinates> checks, List<Coordinates>? reverseOrderedPossibleMoves = null)
    {
        Console.ForegroundColor = GameFigureColor;
        if(Console.CursorTop != _chessTableBottom)
        {
            Console.Clear();
        }
        reverseOrderedPossibleMoves ??= [];
        Console.SetCursorPosition(0, 0);
        for(sbyte i = 0; i < step.GetYAxisLenght(); i++)
        {
            for(sbyte j = 0; j < step.GetXAxisLenght(); j++)
            {
                SetConsoleBackgroundColor(j, i, playerCoordinates, checks, reverseOrderedPossibleMoves);
                Console.Write(step[j, i]);
                Console.Out.Flush();
            }
            Console.BackgroundColor = GameBackgroundColor;
            Console.Write('\n');
        }
        _chessTableBottom = Console.CursorTop;
    }
    private static void SetConsoleBackgroundColor(int xCoordinate, int yCoordinate, Coordinates playerCoordinates, 
        List<Coordinates> checks, List<Coordinates> possibleMoves)
    {
        if(playerCoordinates.X == xCoordinate && playerCoordinates.Y == yCoordinate)
        {
            Console.BackgroundColor = PlayerLocationColor;
            //Needed to always clear passed Coordinates
            if(possibleMoves.Count > 0 && playerCoordinates.X == possibleMoves.LastOrDefault().X && playerCoordinates.Y == possibleMoves.LastOrDefault().Y)
                possibleMoves.RemoveAt(possibleMoves.Count-1);
            if(checks.Count > 0 && playerCoordinates.X == checks.LastOrDefault().X && playerCoordinates.Y == checks.LastOrDefault().Y)
                checks.RemoveAt(checks.Count-1);
        }
        else if(possibleMoves.Count > 0 && possibleMoves.LastOrDefault().X == xCoordinate && possibleMoves.LastOrDefault().Y == yCoordinate)
        {
            possibleMoves.RemoveAt(possibleMoves.Count-1);
            Console.BackgroundColor = PossibleMoveColor;
        }
        else if(checks.Count > 0 && checks.LastOrDefault().X == xCoordinate && checks.LastOrDefault().Y == yCoordinate)
        {
            checks.RemoveAt(checks.Count-1);
            Console.BackgroundColor = CheckColor;
        }
        else
        {
            Console.BackgroundColor = (xCoordinate+yCoordinate)%2 == 0 ? WhiteTileColor : BlackTileColor;
        }
    }

    private int _chessTableBottom;
    public void DrawPromotionMenu(string[] figureStrings, int currentIndex)
    {
        Console.SetCursorPosition(0, _chessTableBottom);
        for(int i = 0; i < figureStrings.Length; i++)
        {
            if(i != currentIndex)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" "+figureStrings[i]+" ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" "+figureStrings[i]+" ");
            }
        }
        Console.WriteLine();
        Console.BackgroundColor = GameBackgroundColor;
        Console.ForegroundColor = GameFigureColor;
    }
    public void DisplayMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Out.WriteLine(message);
    }
}