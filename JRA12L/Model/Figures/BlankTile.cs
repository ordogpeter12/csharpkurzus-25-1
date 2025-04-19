namespace JRA12L;

public class BlankTile : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.Blank;
    private const ChessPieceType Type = ChessPieceType.None;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        return new List<Coordinates>();
    }
    public override string ToString()
    {
        return "   ";
    }
}