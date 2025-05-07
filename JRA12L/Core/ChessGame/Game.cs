using JRA12L.Infrastructure;
using JRA12L.Model;
using JRA12L.View;

namespace JRA12L.Core.ChessGame;

public class Game(IUserInput userInput, IView view, ITable chessTable)
{
    private bool _gameOver;
    private Coordinates _playerPosition = new(4, 4);

    public void StartGame()
    {
        while(!_gameOver)
        {
            view.Draw(chessTable.GetCurrentStep(), _playerPosition, [..chessTable.GetChecks()]);
            switch((IUserInput.UserInput)userInput.GetUserInput())
            {
                case IUserInput.UserInput.Up:
                    if(_playerPosition.Y > 0)
                         _playerPosition.Y--;
                    break;
                case IUserInput.UserInput.Down:
                    if(_playerPosition.Y < chessTable.GetCurrentStep().GetYAxisLenght()-1)
                        _playerPosition.Y++;
                    break;
                case IUserInput.UserInput.Left:
                    if(_playerPosition.X > 0)
                        _playerPosition.X--;
                    break;
                case IUserInput.UserInput.Right:
                    if(_playerPosition.X < chessTable.GetCurrentStep().GetXAxisLenght()-1)
                        _playerPosition.X++;
                    break;
                case IUserInput.UserInput.Select:
                    SelectAction(chessTable.GetValidMoves(_playerPosition));
                    break;
                case IUserInput.UserInput.Forward:
                    chessTable.RedoMove();
                    break;
                case IUserInput.UserInput.Backward:
                    chessTable.UndoMove();
                    break;
                case IUserInput.UserInput.Save:
                    if(SaveGame(chessTable.GetSteps()))
                        view.DisplayMessage("Couldn't save game!");
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
            view.Draw(chessTable.GetCurrentStep(), _playerPosition, [..chessTable.GetChecks()], [..validMoves]);
            switch((IUserInput.UserInput)userInput.GetUserInput())
            {
                case IUserInput.UserInput.Up:
                    if(_playerPosition.Y > 0)
                        _playerPosition.Y--;
                    break;
                case IUserInput.UserInput.Down:
                    if(_playerPosition.Y < chessTable.GetCurrentStep().GetYAxisLenght()-1)
                        _playerPosition.Y++;
                    break;
                case IUserInput.UserInput.Left:
                    if(_playerPosition.X > 0)
                        _playerPosition.X--;
                    break;
                case IUserInput.UserInput.Right:
                    if(_playerPosition.X < chessTable.GetCurrentStep().GetXAxisLenght()-1)
                        _playerPosition.X++;
                    break;
                case IUserInput.UserInput.Select:
                    if(chessTable.PerformMove(_playerPosition, view.DrawPromotionMenu, userInput))
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
        view.DisplayMessage("Give the save a name!(max. 50 characters)");
        string? filename = userInput.GetString();
        while(string.IsNullOrEmpty(filename))
        {
            view.DisplayMessage("The save's name has to be at least 1 character long");
            filename = userInput.GetString();
        }
        if (filename.Length > 50)
        {
            filename = filename.Substring(0, 50);
        }
        return StepJsonSaver.Save(stepsJson, filename);
    }
}