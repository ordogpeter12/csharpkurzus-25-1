namespace JRA12L;

public interface IFigure
{
    ChessPieceColor Color { get; }
    List<Coordinates> GetValidMoves(in IStep step, in Coordinates figureCoordinates);
}