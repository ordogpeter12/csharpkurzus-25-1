namespace JRA12L;

public class ChessTable : ITable
{
    private List<IStep> _steps = new List<IStep>();
    private short displayIndex = 0; //For rewinding steps
    
    public ChessTable(List<IStep> steps)
    {
        this._steps = steps;
    }
}