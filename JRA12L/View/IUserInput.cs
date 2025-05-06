namespace JRA12L.View;

public interface IUserInput
{
    enum UserInput
    {
        Up = 'w',
        Down = 's',
        Left = 'a',
        Right = 'd',
        Select = '\r',
        Forward = 'l',
        Backward = 'j',
        Save = 'm',
        Exit = 'z',
    }
    char GetUserInput();
    string? GetString();
}