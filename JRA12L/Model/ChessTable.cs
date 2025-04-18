namespace JRA12L;

public class ChessTable : ITable
{
    private List<IStep> _steps;
    private int _displayIndex; //For rewinding steps
    
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
}