namespace JRA12L.View;

public class ConsoleUserInput : IUserInput
{
    public char GetUserInput()
    {
        return Console.ReadKey(true).KeyChar;
    }
    public string? GetString()
    {
        return Console.ReadLine();
        
    }
}