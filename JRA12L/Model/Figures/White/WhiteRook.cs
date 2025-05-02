using JRA12L.Core;

namespace JRA12L.Model.Figures.White;

public class WhiteRook : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.White;
    private const ChessPieceType Type = ChessPieceType.Rook;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        if(NotLinearIncreasingPinned(table.GetCurrentStep(), figureCoordinates, Color)
            && NotLinearDecreasingPinned(table.GetCurrentStep(), figureCoordinates, Color))
        {
            if(NotRowPinned(table.GetCurrentStep(), figureCoordinates, Color))
            {
                validMoves.AddRange(UpValids(table.GetCurrentStep(), figureCoordinates));
                validMoves.AddRange(DownValids(table.GetCurrentStep(), figureCoordinates));
            }

            if (NotColumnPinned(table.GetCurrentStep(), figureCoordinates, Color))
            {
                validMoves.AddRange(LeftValids(table.GetCurrentStep(), figureCoordinates));
                validMoves.AddRange(RightValids(table.GetCurrentStep(), figureCoordinates));
            }
        }
        return validMoves;
    }
    private List<Coordinates> UpValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.Y--;
        while(figureCoordinates.Y >= 0)
        {
            if(currentStep[figureCoordinates].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                validMoves.Add(figureCoordinates);
            }
            else if(currentStep[figureCoordinates].GetChessPieceColor() != Color)
            {
                validMoves.Add(figureCoordinates);
                break;
            }
            else
            {
                break;
            }
            figureCoordinates.Y--;
        }
        return validMoves;
    }
    private List<Coordinates> DownValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.Y++;
        while(figureCoordinates.Y < currentStep.GetYAxisLenght())
        {
            if(currentStep[figureCoordinates].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                validMoves.Add(figureCoordinates);
            }
            else if(currentStep[figureCoordinates].GetChessPieceColor() != Color)
            {
                validMoves.Add(figureCoordinates);
                break;
            }
            else
            {
                break;
            }
            figureCoordinates.Y++;
        }
        return validMoves;
    }
    private List<Coordinates> LeftValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.X--;
        while(figureCoordinates.X >= 0)
        {
            if(currentStep[figureCoordinates].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                validMoves.Add(figureCoordinates);
            }
            else if(currentStep[figureCoordinates].GetChessPieceColor() != Color)
            {
                validMoves.Add(figureCoordinates);
                break;
            }
            else
            {
                break;
            }
            figureCoordinates.X--;
        }
        return validMoves;
    }
    private List<Coordinates> RightValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.X++;
        while(figureCoordinates.X < currentStep.GetXAxisLenght())
        {
            if(currentStep[figureCoordinates].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                validMoves.Add(figureCoordinates);
            }
            else if(currentStep[figureCoordinates].GetChessPieceColor() != Color)
            {
                validMoves.Add(figureCoordinates);
                break;
            }
            else
            {
                break;
            }
            figureCoordinates.X++;
        }
        return validMoves;
    }
    public override string ToString()
    {
        return " \u2656 ";
    }
}