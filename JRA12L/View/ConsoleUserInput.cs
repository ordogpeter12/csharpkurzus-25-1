namespace JRA12L;

public class ConsoleUserInput : IUserInput
{
    public char GetUserInput()
    {
        while(true)
        {
            return Console.ReadKey(true).KeyChar;
        }
    }
}