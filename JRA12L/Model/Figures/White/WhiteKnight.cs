namespace JRA12L;

public class WhiteKnight : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.White;
    private const ChessPieceType Type = ChessPieceType.Bishop;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = new();
        if(figureCoordinates.Y < table.GetCurrentStep().GetYAxisLenght()-1)
        {
            if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 2
                && table.GetCurrentStep()[(sbyte)(figureCoordinates.X + 2), (sbyte)(figureCoordinates.Y + 1)].GetChessPieceColor() != ChessPieceColor.White)
            {
                validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X+2), (sbyte)(figureCoordinates.Y+1)));
            }
            if (figureCoordinates.X > 1
                && table.GetCurrentStep()[(sbyte)(figureCoordinates.X - 2), (sbyte)(figureCoordinates.Y + 1)].GetChessPieceColor() != ChessPieceColor.White)
            {
                validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X-2), (sbyte)(figureCoordinates.Y+1)));
            }
            if(figureCoordinates.Y < table.GetCurrentStep().GetYAxisLenght()-2)
            {
                if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 1
                    && table.GetCurrentStep()[(sbyte)(figureCoordinates.X + 1), (sbyte)(figureCoordinates.Y + 2)].GetChessPieceColor() != ChessPieceColor.White)
                {
                    validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X+1), (sbyte)(figureCoordinates.Y+2)));
                }
                if (figureCoordinates.X > 0
                    && table.GetCurrentStep()[(sbyte)(figureCoordinates.X - 1), (sbyte)(figureCoordinates.Y + 2)].GetChessPieceColor() != ChessPieceColor.White)
                {
                    validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y+2)));
                }
            }
        }
        if(figureCoordinates.Y > 0)
        {
            if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 2
                && table.GetCurrentStep()[(sbyte)(figureCoordinates.X + 2), (sbyte)(figureCoordinates.Y - 1)].GetChessPieceColor() != ChessPieceColor.White)
            {
                validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X+2), (sbyte)(figureCoordinates.Y-1)));
            }
            if (figureCoordinates.X > 1
                && table.GetCurrentStep()[(sbyte)(figureCoordinates.X - 2), (sbyte)(figureCoordinates.Y - 1)].GetChessPieceColor() != ChessPieceColor.White)
            {
                validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X-2), (sbyte)(figureCoordinates.Y-1)));
            }
            if(figureCoordinates.Y > 1)
            {
                if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 1
                    && table.GetCurrentStep()[(sbyte)(figureCoordinates.X + 1), (sbyte)(figureCoordinates.Y - 2)].GetChessPieceColor() != ChessPieceColor.White)
                {
                    validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X+1), (sbyte)(figureCoordinates.Y-2)));
                }

                if (figureCoordinates.X > 0
                    && table.GetCurrentStep()[(sbyte)(figureCoordinates.X - 1), (sbyte)(figureCoordinates.Y - 2)].GetChessPieceColor() != ChessPieceColor.White)
                {
                    validMoves.Add(new Coordinates((sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y-2)));
                }
            }
        }
        return validMoves;
    }
    public override string ToString()
    {
        return " \u2658 ";
    }
}