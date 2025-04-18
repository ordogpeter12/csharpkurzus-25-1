namespace JRA12L;

public sealed record Step : IStep
{
    private IStep.Figure[] _tableValue { get; }
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    private bool _shortCastle = true;
    private bool _longCastle = true;

    public Step(string tableValue)
    {
        _tableValue = Array.ConvertAll(tableValue.ToCharArray(), c => (IStep.Figure)c);
    }
    public Step(Step step)
    {
        _tableValue = step._tableValue;
        _shortCastle = step._shortCastle;
        _longCastle = step._longCastle;
    }
    int IStep.GetXAxisLenght() => XAxisLenght;

    int IStep.GetYAxisLenght() => YAxisLenght;

    public IStep.Figure this[int index] { get => _tableValue[index]; }
}