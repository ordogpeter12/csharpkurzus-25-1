namespace JRA12L;

public class Game
{
    private bool _gameOver;
    private Coordinates _playerPosition;
    private ChessTable _chessTable;
    
    private IUserInput _userInput;
    private IView _view;

    public Game(IUserInput userInput, IView view)
    {
        this._userInput = userInput;
        this._view = view;
    }
    public void StartGame()
    {
        while(!_gameOver)
        {
            
        }
    }
}