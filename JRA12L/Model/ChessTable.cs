namespace JRA12L;

public class ChessTable : ITable
{
    private List<IStep> _steps;
    private int _displayIndex; //For rewinding steps
    
    private List<Coordinates> _validMoves;
    
    public ChessTable(List<IStep> steps)
    {
        this._steps = steps;
        this._displayIndex = steps.Count-1;
    }

    public ChessTable(IStep step)
    {
        this._steps = new() {step};
        this._displayIndex = 0;
    }
    public IStep GetCurrentStep() => _steps[_displayIndex];

    public List<Coordinates> GetValidMoves(Coordinates selectedFigure)
    {
        _validMoves = GetCurrentStep()[selectedFigure.X, selectedFigure.Y].GetValidMoves(GetCurrentStep(), selectedFigure);
        _validMoves.Sort((a, b) => b.CompareTo(a));
        return _validMoves;
    }
}