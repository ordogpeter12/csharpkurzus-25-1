namespace JRA12L;

public interface IUserInput
{
    enum UserInput
    {
        Up = 'w',
        Down = 's',
        Left = 'a',
        Right = 'd',
        Select = '\r',
        Forward = 'k',
        Backward = 'l',
        Save = 'm',
        Exit = 'z',
    }
    char GetUserInput();
}