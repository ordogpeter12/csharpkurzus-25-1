namespace JRA12L;

public interface IStep
{
    int GetXAxisLenght();
    int GetYAxisLenght();
    Figure this[byte x, byte y] { get; }
    public Figure this[Coordinates coordinates] { get; }
}