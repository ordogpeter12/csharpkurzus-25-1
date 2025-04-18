namespace JRA12L;

public class BlackKnight : IFigure
{
    public ChessPieceColor Color { get; } = ChessPieceColor.Black;
    public List<Coordinates> GetValidMoves(in IStep step, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = new();
        if(figureCoordinates.Y < step.GetYAxisLenght()-1)
        {
            if (figureCoordinates.X < step.GetXAxisLenght() - 2
                && step[(byte)(figureCoordinates.X + 2), (byte)(figureCoordinates.Y + 1)].Color != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X+2), (byte)(figureCoordinates.Y+1)));
            }
            if (figureCoordinates.X > 1
                && step[(byte)(figureCoordinates.X - 2), (byte)(figureCoordinates.Y + 1)].Color != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X-2), (byte)(figureCoordinates.Y+1)));
            }
            if(figureCoordinates.Y < step.GetYAxisLenght()-2)
            {
                if (figureCoordinates.X < step.GetXAxisLenght() - 1
                    && step[(byte)(figureCoordinates.X + 1), (byte)(figureCoordinates.Y + 2)].Color != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X+1), (byte)(figureCoordinates.Y+2)));
                }
                if (figureCoordinates.X > 0
                    && step[(byte)(figureCoordinates.X - 1), (byte)(figureCoordinates.Y + 2)].Color != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X-1), (byte)(figureCoordinates.Y+2)));
                }
            }
        }
        if(figureCoordinates.Y > 0)
        {
            if (figureCoordinates.X < step.GetXAxisLenght() - 2
                && step[(byte)(figureCoordinates.X + 2), (byte)(figureCoordinates.Y - 1)].Color != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X+2), (byte)(figureCoordinates.Y-1)));
            }
            if (figureCoordinates.X > 1
                && step[(byte)(figureCoordinates.X - 2), (byte)(figureCoordinates.Y - 1)].Color != ChessPieceColor.Black)
            {
                validMoves.Add(new Coordinates((byte)(figureCoordinates.X-2), (byte)(figureCoordinates.Y-1)));
            }
            if(figureCoordinates.Y > 1)
            {
                if (figureCoordinates.X < step.GetXAxisLenght() - 1
                    && step[(byte)(figureCoordinates.X + 1), (byte)(figureCoordinates.Y - 2)].Color != ChessPieceColor.Black)
                {
                    validMoves.Add(new Coordinates((byte)(figureCoordinates.X+1), (byte)(figureCoordinates.Y-2)));
                }

                if (figureCoordinates.X > 0
                    && step[(byte)(figureCoordinates.X - 1), (byte)(figureCoordinates.Y - 2)].Color != ChessPieceColor.Black)
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