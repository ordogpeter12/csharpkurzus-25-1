namespace JRA12L;

public class ChessTable : ITable
{
    private List<IStep> _steps;
    private int _displayIndex; //For rewinding steps

    private Coordinates _selectedFigure;
    private List<Coordinates> _validMoves;
    
    public ChessTable(List<IStep> steps)
    {
        if (steps.Count == 0)
        {
            throw new ArgumentException("At least one step is required.");
        }
        this._steps = steps;
        this._displayIndex = steps.Count-1;
        //TODO KingData
    }

    public ChessTable(IStep step)
    {
        this._steps = new() {step};
        this._displayIndex = 0;
    }
    public IStep GetCurrentStep() => _steps[_displayIndex];
    public IStep? GetPreviousStep() => _displayIndex != 0 ? _steps[_displayIndex - 1] : null;

    public List<Coordinates> GetValidMoves(Coordinates selectedFigure)
    {
        _selectedFigure = selectedFigure;
        _validMoves = GetCurrentStep()[selectedFigure].GetValidMoves(this, selectedFigure);
        _validMoves.Sort((a, b) => b.CompareTo(a));
        return _validMoves;
    }
    public bool PerformMove(Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput)
    {
        if(_validMoves.Contains(destination))
        {
            _steps.Add(GetCurrentStep().GetNextStep(_selectedFigure, destination, promotionMenu, userInput));
            _displayIndex++;
            return true;
        }
        return false;
    }
}