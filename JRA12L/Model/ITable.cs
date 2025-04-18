namespace JRA12L;

public interface ITable
{
    IStep GetCurrentStep();
    List<Coordinates> GetValidMoves(Coordinates selectedFigure);
}