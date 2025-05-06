using JRA12L.Infrastructure;
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
                    _chessTable.RedoMove();
                    break;
                case IUserInput.UserInput.Backward:
                    _chessTable.UndoMove();
                    break;
                case IUserInput.UserInput.Save:
                    if(SaveGame(_chessTable.GetSteps()))
                        _view.DisplayMessage("Couldn't save game!");
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
    private bool SaveGame(List<IStep> steps)
    {
        List<JsonStepDto> stepsJson = [];
        foreach(var step in steps)
        {
            stepsJson.Add(JsonMapper.ToDto((step.GetSavableInformation())));
        }
        _view.DisplayMessage("Give the save a name!(max. 50 characters)");
        string? filename = _userInput.GetString();
        while(string.IsNullOrEmpty(filename))
        {
            _view.DisplayMessage("The save's name has to be at least 1 character long");
            filename = _userInput.GetString();
        }
        if (filename.Length > 50)
        {
            filename = filename.Substring(0, 50);
        }
        return StepJsonSaver.Save(stepsJson, filename);
    }
}