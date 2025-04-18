namespace JRA12L;

public sealed record Step : IStep
{
    private IFigure[] _tableValue { get; }
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    private bool _shortCastle = true;
    private bool _longCastle = true;
    public Step(Step step)
    {
        _tableValue = step._tableValue;
        _shortCastle = step._shortCastle;
        _longCastle = step._longCastle;
    }

    public Step()
    {
        //chess starting position
        _tableValue = new[]
        {
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
            new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(), new BlackKnight(),
        };
    }
    int IStep.GetXAxisLenght() => XAxisLenght;

    int IStep.GetYAxisLenght() => YAxisLenght;

    public IFigure this[byte x, byte y] { get => _tableValue[y*XAxisLenght+x]; }
}