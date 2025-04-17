namespace JRA12L;

public class ConsoleView : IView
{
    private const char DifferenceBetweenFigureAndCharRepresantation = '\u2633';
    private readonly ConsoleColor _defaultConsoleForegroundColor;
    private readonly ConsoleColor _defaultConsoleBackgroundColor;
    
    private const ConsoleColor FigureColor = ConsoleColor.Black;
    private const ConsoleColor BackgroundColor = ConsoleColor.Black;
    
    private const ConsoleColor BlackTileColor = ConsoleColor.DarkYellow;
    private const ConsoleColor WhiteTileColor = ConsoleColor.White;
    private const ConsoleColor PlayerLocationColor = ConsoleColor.DarkMagenta;

    public ConsoleView()
    {
        _defaultConsoleForegroundColor = Console.ForegroundColor;
        _defaultConsoleBackgroundColor = Console.BackgroundColor;
        
        Console.ForegroundColor = FigureColor;
        Console.BackgroundColor = BackgroundColor;
        Console.Clear();
    }
    public void Draw(IStep step)
    {
        for(int i = 0; i < step.GetXAxisLenght(); i++)
        {
            for(int j = 0; j < step.GetYAxisLenght(); j++)
            {
                SetConsoleBackgroundColor(i, j);
                Console.Write(ConvertToTile(step[i*step.GetYAxisLenght() + j]));
                Console.Out.Flush();
            }
            Console.BackgroundColor = BackgroundColor;
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

    private static void SetConsoleBackgroundColor(int xCoordinate, int yCoordinate)
    {
        Console.BackgroundColor = (xCoordinate+yCoordinate)%2 == 0 ? WhiteTileColor : BlackTileColor;
    }
}