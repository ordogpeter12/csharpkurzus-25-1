using JRA12L.Model;
using JRA12L.Model.Figures;
using JRA12L.View;

namespace JRA12L.Core.ChessGame;

public class ChessTable : ITable
{
    private List<IStep> _steps;
    private int _displayIndex; //For rewinding steps

    private Coordinates _selectedFigure;
    private List<Coordinates> _validMoves;
    private List<Coordinates> _checks = [];
    
    public ChessTable(List<IStep> steps)
    {
        if (steps.Count == 0)
        {
            throw new ArgumentException("At least one step is required.");
        }
        this._steps = steps;
        this._displayIndex = steps.Count-1;
    }

    public ChessTable(IStep step)
    {
        this._steps = new() {step};
        this._displayIndex = 0;
    }
    public IStep GetCurrentStep() => _steps[_displayIndex];
    public IStep? GetPreviousStep() => _displayIndex != 0 ? _steps[_displayIndex - 1] : null;

    public List<Coordinates> GetChecks()
    {
        List<Coordinates> returnableChecks = [.._checks];
        returnableChecks.Sort((a, b) => b.CompareTo(a));
        return returnableChecks;
    }

    private List<Coordinates> GetCheckBlockableTiles()
    {
        List<Coordinates> blockChecks = [];
        //if double check or no check
        if(_checks.Count >= 3 || _checks.Count == 0)
        {
            return [];
        }
        //0 index is attacker, 1 index is the king
        if(_checks[0].X == _checks[1].X)
        {
            if(_checks[0].Y > _checks[1].Y)
            {
                for(int i = _checks[0].Y; i > _checks[1].Y; i--)
                {
                    blockChecks.Add(new Coordinates(_checks[0].X, (sbyte)i));
                }
            }
            else
            {
                for(int i = _checks[0].Y; i < _checks[1].Y; i++)
                {
                    blockChecks.Add(new Coordinates(_checks[0].X, (sbyte)i));
                }
            }
        }
        else if(_checks[0].Y == _checks[1].Y)
        {
            if(_checks[0].X > _checks[1].X)
            {
                for(int i = _checks[0].X; i > _checks[1].X; i--)
                {
                    blockChecks.Add(new Coordinates((sbyte)i, _checks[0].Y));
                }
            }
            else
            {
                for(int i = _checks[0].X; i < _checks[1].X; i++)
                {
                    blockChecks.Add(new Coordinates((sbyte)i, _checks[0].Y));
                }
            }
        }
        else if(Math.Abs(_checks[1].X-_checks[0].X) == Math.Abs(_checks[1].Y-_checks[0].Y))
        {
            if (_checks[0].Y > _checks[1].Y)
            {
                if (_checks[0].X > _checks[1].X)
                {
                    sbyte x = _checks[0].X;
                    sbyte y = _checks[0].Y;
                    while(y > _checks[1].Y)
                    {
                        blockChecks.Add(new Coordinates(x, y));
                        x--;
                        y--;
                    }
                }
                else
                {
                    sbyte x = _checks[0].X;
                    sbyte y = _checks[0].Y;
                    while (y > _checks[1].Y)
                    {
                        blockChecks.Add(new Coordinates(x, y));
                        x++;
                        y--;
                    }
                }
            }
            else
            {
                if (_checks[0].X > _checks[1].X)
                {
                    sbyte x = _checks[0].X;
                    sbyte y = _checks[0].Y;
                    while (y < _checks[1].Y)
                    {
                        blockChecks.Add(new Coordinates(x, y));
                        x--;
                        y++;
                    }
                }
                else
                {
                    sbyte x = _checks[0].X;
                    sbyte y = _checks[0].Y;
                    while(y < _checks[1].Y)
                    {
                        blockChecks.Add(new Coordinates(x, y));
                        x++;
                        y++;
                    }
                }
            }
        }
        return blockChecks;
    }
    public List<Coordinates> GetValidMoves(Coordinates selectedFigure)
    {
        _selectedFigure = selectedFigure;
        _validMoves = [];
        if(GetCurrentStep()[selectedFigure].GetChessPieceColor() == GetCurrentStep().WhoseTurn)
        {
            //if double check only the king can move
            if(_checks.Count == 3 && GetCurrentStep()[selectedFigure].GetChessPieceType() == ChessPieceType.King)
            {
                _validMoves = GetCurrentStep()[selectedFigure].GetValidMoves(this, selectedFigure);
            }
            else if(_checks.Count == 0)
            {
                _validMoves = GetCurrentStep()[selectedFigure].GetValidMoves(this, selectedFigure);
            }
            else if(_checks.Count < 3)
            {
                //step out from check with king
                if (GetCurrentStep()[selectedFigure].GetChessPieceType() == ChessPieceType.King)
                {
                    _validMoves = GetCurrentStep()[selectedFigure].GetValidMoves(this, selectedFigure);
                }
                else
                {
                    _validMoves = GetCurrentStep()[selectedFigure].GetValidMoves(this, selectedFigure);
                    List<Coordinates> blockingMoves = GetCheckBlockableTiles();
                    _validMoves.RemoveAll(item => !blockingMoves.Contains(item));
                }
            }
        }
        _validMoves.Sort((a, b) => b.CompareTo(a));
        return _validMoves;
    }
    public bool PerformMove(Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput)
    {
        if(_validMoves.Contains(destination))
        {
            _steps.Add(GetCurrentStep().GetNextStep(_selectedFigure, destination, promotionMenu, userInput));
            _displayIndex++;
            _checks = GetCurrentStep().GetChecks();
            return true;
        }
        return false;
    }
}