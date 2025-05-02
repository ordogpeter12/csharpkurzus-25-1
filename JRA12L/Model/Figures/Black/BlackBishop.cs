using JRA12L.Core;

namespace JRA12L.Model.Figures.Black;

public class BlackBishop : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.Black;
    private const ChessPieceType Type = ChessPieceType.Bishop;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;

    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        if (NotRowPinned(table.GetCurrentStep(), figureCoordinates, Color)
            && NotColumnPinned(table.GetCurrentStep(), figureCoordinates, Color))
        {
            if(NotLinearIncreasingPinned(table.GetCurrentStep(), figureCoordinates, Color))
            {
                validMoves.AddRange(DiagonalUpLeftValids(table.GetCurrentStep(), figureCoordinates));
                validMoves.AddRange(DiagonalDownRightValids(table.GetCurrentStep(), figureCoordinates));
            }
            if(NotLinearDecreasingPinned(table.GetCurrentStep(), figureCoordinates, Color))
            {
                validMoves.AddRange(DiagonalUpRightValids(table.GetCurrentStep(), figureCoordinates));
                validMoves.AddRange(DiagonalDownLeftValids(table.GetCurrentStep(), figureCoordinates));
            }
        }
        return validMoves;
    }

    private List<Coordinates> DiagonalUpLeftValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.X--;
        figureCoordinates.Y--;
        while(figureCoordinates is { X: >= 0, Y: >= 0 })
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
            figureCoordinates.Y--;
        }
        return validMoves;
    }
    private List<Coordinates> DiagonalDownRightValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.X++;
        figureCoordinates.Y++;
        while(figureCoordinates.X < currentStep.GetXAxisLenght() && figureCoordinates.Y < currentStep.GetYAxisLenght())
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
            figureCoordinates.Y++;
        }
        return validMoves;
    }
    private List<Coordinates> DiagonalUpRightValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.X++;
        figureCoordinates.Y--;
        while(figureCoordinates.X < currentStep.GetXAxisLenght() && figureCoordinates.Y >= 0)
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
            figureCoordinates.Y--;
        }
        return validMoves;
    }
    private List<Coordinates> DiagonalDownLeftValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        figureCoordinates.X--;
        figureCoordinates.Y++;
        while(figureCoordinates.X >= 0 && figureCoordinates.Y < currentStep.GetYAxisLenght())
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
            figureCoordinates.Y++;
        }
        return validMoves;
    }
    public override string ToString()
    {
        return " \u265d ";
    }
    
}