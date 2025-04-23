namespace JRA12L;

public class WhiteQueen : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.White;
    private const ChessPieceType Type = ChessPieceType.Queen;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        validMoves.AddRange(new WhiteBishop().GetValidMoves(table, figureCoordinates));
        validMoves.AddRange(new WhiteRook().GetValidMoves(table, figureCoordinates));
        return validMoves;
    }
    public override string ToString()
    {
        return " \u2655 ";
    }
}