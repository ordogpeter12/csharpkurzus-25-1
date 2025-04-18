namespace JRA12L;

public class BlackQueen : IFigure
{
    public ChessPieceColor Color { get; } = ChessPieceColor.Black;
    public List<Coordinates> GetValidMoves(in IStep step, in Coordinates figureCoordinates)
    {
        return new List<Coordinates>();
    }
    public override string ToString()
    {
        return " \u265b ";
    }
}