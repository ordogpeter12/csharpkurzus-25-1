namespace JRA12L;

public interface IStep
{
    int GetXAxisLenght();
    int GetYAxisLenght();
    IFigure this[byte x, byte y] { get; }
}