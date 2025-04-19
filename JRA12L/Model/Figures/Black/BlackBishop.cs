namespace JRA12L;

public class BlackBishop : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.Black;
    private const ChessPieceType Type = ChessPieceType.Bishop;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;

    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        return new List<Coordinates>();
    }
    public override string ToString()
    {
        return " \u265d ";
    }
    
}