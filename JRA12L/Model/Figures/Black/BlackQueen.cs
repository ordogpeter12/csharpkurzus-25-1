using JRA12L.Core;
using JRA12L.Core.ChessGame;

namespace JRA12L.Model.Figures.Black;

public class BlackQueen : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.Black;
    private const ChessPieceType Type = ChessPieceType.Queen;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        validMoves.AddRange(new BlackBishop().GetValidMoves(table, figureCoordinates));
        validMoves.AddRange(new BlackRook().GetValidMoves(table, figureCoordinates));
        return validMoves;
    }
    public override string ToString()
    {
        return " \u265b ";
    }
}