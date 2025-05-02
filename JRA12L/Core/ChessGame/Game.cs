using JRA12L.Model;
using JRA12L.View;

namespace JRA12L.Core.ChessGame;

public class Game
{
    private bool _gameOver;
    private Coordinates _playerPosition;
    private ITable _chessTable;
    
    private IUserInput _userInput;
    private IView _view;

    public Game(IUserInput userInput, IView view, ITable chessTable)
    {
        this._userInput = userInput;
        this._view = view;
        this._chessTable = chessTable;
        _gameOver = false;
        this._playerPosition = new Coordinates(4, 4);
    }
    public void StartGame()
    {
        while(!_gameOver)
        {
            _view.Draw(_chessTable.GetCurrentStep(), _playerPosition, [.._chessTable.GetChecks()]);
            switch((IUserInput.UserInput)_userInput.GetUserInput())
            {
                case IUserInput.UserInput.Up:
                    if(_playerPosition.Y > 0)
                         _playerPosition.Y--;
                    break;
                case IUserInput.UserInput.Down:
                    if(_playerPosition.Y < _chessTable.GetCurrentStep().GetYAxisLenght()-1)
                        _playerPosition.Y++;
                    break;
                case IUserInput.UserInput.Left:
                    if(_playerPosition.X > 0)
                        _playerPosition.X--;
                    break;
                case IUserInput.UserInput.Right:
                    if(_playerPosition.X < _chessTable.GetCurrentStep().GetXAxisLenght()-1)
                        _playerPosition.X++;
                    break;
                case IUserInput.UserInput.Select:
                    SelectAction(_chessTable.GetValidMoves(_playerPosition));
                    break;
                case IUserInput.UserInput.Forward:
                    break;
                case IUserInput.UserInput.Backward:
                    break;
                case IUserInput.UserInput.Save:
                    break;
                case IUserInput.UserInput.Exit:
                    _gameOver = true;
                    break;
            }
        }
    }

    private void SelectAction(List<Coordinates> validMoves)
    {
        if (validMoves.Count > 0)
        {
            WhileFigureSelected(validMoves);
        }
    }
    private void WhileFigureSelected(List<Coordinates> validMoves)
    {
        bool figureSelected = true;
        while(figureSelected)
        {
            _view.Draw(_chessTable.GetCurrentStep(), _playerPosition, [.._chessTable.GetChecks()], [..validMoves]);
            switch((IUserInput.UserInput)_userInput.GetUserInput())
            {
                case IUserInput.UserInput.Up:
                    if(_playerPosition.Y > 0)
                        _playerPosition.Y--;
                    break;
                case IUserInput.UserInput.Down:
                    if(_playerPosition.Y < _chessTable.GetCurrentStep().GetYAxisLenght()-1)
                        _playerPosition.Y++;
                    break;
                case IUserInput.UserInput.Left:
                    if(_playerPosition.X > 0)
                        _playerPosition.X--;
                    break;
                case IUserInput.UserInput.Right:
                    if(_playerPosition.X < _chessTable.GetCurrentStep().GetXAxisLenght()-1)
                        _playerPosition.X++;
                    break;
                case IUserInput.UserInput.Select:
                    if(_chessTable.PerformMove(_playerPosition, _view.DrawPromotionMenu, _userInput))
                        figureSelected = false;
                    break;
                case IUserInput.UserInput.Exit:
                    figureSelected = false;
                    break;
            }
        }
    }
}