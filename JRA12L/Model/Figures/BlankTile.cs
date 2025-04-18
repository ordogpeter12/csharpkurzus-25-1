namespace JRA12L;

public class BlankTile : IFigure
{
    public ChessPieceColor Color { get; } = ChessPieceColor.Blank;
    public List<Coordinates> GetValidMoves(in IStep step, in Coordinates figureCoordinates)
    {
        return new List<Coordinates>();
    }
    public override string ToString()
    {
        return "   ";
    }
}