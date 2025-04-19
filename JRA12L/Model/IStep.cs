namespace JRA12L;

public interface IStep
{
    int GetXAxisLenght();
    int GetYAxisLenght();
    Figure this[sbyte x, sbyte y] { get; }
    public Figure this[Coordinates coordinates] { get; }
    IStep GetNextStep(Coordinates movedPiece, Coordinates destination);
}