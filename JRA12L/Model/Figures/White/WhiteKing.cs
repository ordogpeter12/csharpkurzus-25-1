using JRA12L.Core;
using JRA12L.Core.ChessGame;

namespace JRA12L.Model.Figures.White;

public class WhiteKing : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.White;
    private const ChessPieceType Type = ChessPieceType.King;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        IStep currentStep = table.GetCurrentStep();
        Coordinates loopStartingCoordinates = new(
            figureCoordinates.X == 0 ? figureCoordinates.X: (sbyte)(figureCoordinates.X-1), 
                figureCoordinates.Y == 0 ? figureCoordinates.Y: (sbyte)(figureCoordinates.Y-1));
        Coordinates loopEndingCoordinates = new(
            figureCoordinates.X < currentStep.GetXAxisLenght()-1 ? (sbyte)(figureCoordinates.X+1) : figureCoordinates.X,
            figureCoordinates.Y < currentStep.GetYAxisLenght()-1 ? (sbyte)(figureCoordinates.Y+1) : figureCoordinates.Y);
        for(sbyte x = loopStartingCoordinates.X; x <= loopEndingCoordinates.X; x++)
        {
            for(sbyte y = loopStartingCoordinates.Y; y <= loopEndingCoordinates.Y; y++)
            {
                Coordinates currentCoordinates = new(x, y);
                if (currentStep[x, y].GetChessPieceColor() != Color
                    && currentStep.IsKingSafe(currentCoordinates, Color))
                {
                    validMoves.Add(currentCoordinates);
                }
            }
        }
        validMoves.AddRange(GetCastles(currentStep, figureCoordinates));
        return validMoves;
    }
    private List<Coordinates> GetCastles(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        if(currentStep.WhiteLongCastle
           && currentStep[(sbyte)(figureCoordinates.X+1), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
           && currentStep[(sbyte)(figureCoordinates.X+2), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
           && currentStep.IsKingSafe(new Coordinates((sbyte)(figureCoordinates.X+1), figureCoordinates.Y), Color)
           && currentStep.IsKingSafe(new Coordinates((sbyte)(figureCoordinates.X+2), figureCoordinates.Y), Color))
        {
            validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X + 2), figureCoordinates.Y));
        }
        if(currentStep.WhiteLongCastle
           && currentStep[(sbyte)(figureCoordinates.X-1), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
           && currentStep[(sbyte)(figureCoordinates.X-2), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
           && currentStep[(sbyte)(figureCoordinates.X-3), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
           && currentStep.IsKingSafe(new Coordinates((sbyte)(figureCoordinates.X-1), figureCoordinates.Y), Color)
           && currentStep.IsKingSafe(new Coordinates((sbyte)(figureCoordinates.X-2), figureCoordinates.Y), Color))
        {
            validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X-2), figureCoordinates.Y));
        }
        return validMoves;
    }
    public override string ToString()
    {
        return " \u2654 ";
    }
}