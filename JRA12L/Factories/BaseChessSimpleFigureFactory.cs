using JRA12L.Model.Figures;
using JRA12L.Model.Figures.Black;
using JRA12L.Model.Figures.White;

namespace JRA12L.Factories;

public class BaseChessSimpleFigureFactory
{
    public Figure GetFigure(ChessPieceColor pieceColor, ChessPieceType chessPieceType)
    {
        if(ChessPieceColor.White == pieceColor)
        {
            return GetWhiteFigure(chessPieceType);
        }
        if(ChessPieceColor.Black == pieceColor)
        {
            return GetBlackFigure(chessPieceType);
        }
        if (ChessPieceColor.Blank == pieceColor)
        {
            return new BlankTile();
        }
        throw new ArgumentException("No such chess piece color!");
    }
    private Figure GetWhiteFigure(ChessPieceType pieceType)
    {
        switch(pieceType)
        {
            case ChessPieceType.Pawn:
                return new WhitePawn();
            case ChessPieceType.Knight:
                return new WhiteKnight();
            case ChessPieceType.Bishop:
                return new WhiteBishop();
            case ChessPieceType.Rook:
                return new WhiteRook();
            case ChessPieceType.Queen:
                return new WhiteQueen();
            case ChessPieceType.King:
                return new WhiteKing();
            default:
                throw new ArgumentException("No such chess piece type!");
        }
    }
    private Figure GetBlackFigure(ChessPieceType pieceType)
    {
        switch(pieceType)
        {
            case ChessPieceType.Pawn:
                return new BlackPawn();
            case ChessPieceType.Knight:
                return new BlackKnight();
            case ChessPieceType.Bishop:
                return new BlackBishop();
            case ChessPieceType.Rook:
                return new BlackRook();
            case ChessPieceType.Queen:
                return new BlackQueen();
            case ChessPieceType.King:
                return new BlackKing();
            default:
                throw new ArgumentException("No such chess piece type!");
        }
    }
}