namespace JRA12L;

public class WhiteQueen : IFigure
{
    public ChessPieceColor Color { get; } = ChessPieceColor.White;
    public List<Coordinates> GetValidMoves(in IStep step, in Coordinates figureCoordinates)
    {
        return new List<Coordinates>();
    }
    public override string ToString()
    {
        return " \u2655 ";
    }
}