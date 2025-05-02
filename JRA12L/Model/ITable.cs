namespace JRA12L;

public interface ITable
{
    IStep GetCurrentStep();
    IStep? GetPreviousStep();
    List<Coordinates> GetValidMoves(Coordinates selectedFigure);
    bool PerformMove(Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput);
}