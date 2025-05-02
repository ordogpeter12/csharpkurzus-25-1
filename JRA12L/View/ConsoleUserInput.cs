namespace JRA12L;

public class ConsoleUserInput : IUserInput
{
    public char GetUserInput()
    {
        return Console.ReadKey(true).KeyChar;
    }
}