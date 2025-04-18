namespace JRA12L;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game(new ConsoleUserInput(), new ConsoleView(), new ChessTable(new Step()));
        game.StartGame();
    }
}