namespace JRA12L;

public class BlackKnight : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.Black;
    private const ChessPieceType Type = ChessPieceType.Knight;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        if(figureCoordinates.Y < table.GetCurrentStep().GetYAxisLenght()-1)
        {
            if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 2
                && table.GetCurrentStep()[(byte)(figureCoordinates.X + 2), (byte)(figureCoordinates.Y + 1)].GetChessPieceColor() != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X+2), (byte)(figureCoordinates.Y+1)));
            }
            if (figureCoordinates.X > 1
                && table.GetCurrentStep()[(byte)(figureCoordinates.X - 2), (byte)(figureCoordinates.Y + 1)].GetChessPieceColor() != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X-2), (byte)(figureCoordinates.Y+1)));
            }
            if(figureCoordinates.Y < table.GetCurrentStep().GetYAxisLenght()-2)
            {
                if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 1
                    && table.GetCurrentStep()[(byte)(figureCoordinates.X + 1), (byte)(figureCoordinates.Y + 2)].GetChessPieceColor() != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X+1), (byte)(figureCoordinates.Y+2)));
                }
                if (figureCoordinates.X > 0
                    && table.GetCurrentStep()[(byte)(figureCoordinates.X - 1), (byte)(figureCoordinates.Y + 2)].GetChessPieceColor() != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X-1), (byte)(figureCoordinates.Y+2)));
                }
            }
        }
        if(figureCoordinates.Y > 0)
        {
            if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 2
                && table.GetCurrentStep()[(byte)(figureCoordinates.X + 2), (byte)(figureCoordinates.Y - 1)].GetChessPieceColor() != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X+2), (byte)(figureCoordinates.Y-1)));
            }
            if (figureCoordinates.X > 1
                && table.GetCurrentStep()[(byte)(figureCoordinates.X - 2), (byte)(figureCoordinates.Y - 1)].GetChessPieceColor() != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X-2), (byte)(figureCoordinates.Y-1)));
            }
            if(figureCoordinates.Y > 1)
            {
                if (figureCoordinates.X < table.GetCurrentStep().GetXAxisLenght() - 1
                    && table.GetCurrentStep()[(byte)(figureCoordinates.X + 1), (byte)(figureCoordinates.Y - 2)].GetChessPieceColor() != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X+1), (byte)(figureCoordinates.Y-2)));
                }

                if (figureCoordinates.X > 0
                    && table.GetCurrentStep()[(byte)(figureCoordinates.X - 1), (byte)(figureCoordinates.Y - 2)].GetChessPieceColor() != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X-1), (byte)(figureCoordinates.Y-2)));
                }
            }
        }
        return validMoves;
    }
    public override string ToString()
    {
        return " \u265E ";
    }
}