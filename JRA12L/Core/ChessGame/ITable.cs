using JRA12L.Model;
using JRA12L.View;

namespace JRA12L.Core.ChessGame;

public interface ITable
{
    IStep GetCurrentStep();
    IStep? GetPreviousStep();
    List<Coordinates> GetValidMoves(Coordinates selectedFigure);
    List<Coordinates> GetChecks();
    bool PerformMove(Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput);
}