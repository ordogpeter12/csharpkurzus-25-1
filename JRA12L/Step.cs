namespace JRA12L;

public sealed record Step : IStep
{
    private IStep.Figure[] _tableValue { get; }
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
    public IStep.Figure this[int index] { get => _tableValue[index]; }
}