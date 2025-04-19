namespace JRA12L;

class Program
{
    static void Main(string[] args)
    {
        using IView view = new ConsoleView();
        Game game = new Game(new ConsoleUserInput(), view, new ChessTable(new Step()));
        game.StartGame();
    }
}