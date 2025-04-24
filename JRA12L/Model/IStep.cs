namespace JRA12L;

public interface IStep
{
    int GetXAxisLenght();
    int GetYAxisLenght();
    Figure this[sbyte x, sbyte y] { get; }
    Figure this[Coordinates coordinates] { get; }
    IStep GetNextStep(Coordinates movedPiece, Coordinates destination);
    bool IsKingSafe(Coordinates inspectedTile, ChessPieceColor kingColor);
    List<Coordinates> GetCheckingFigures(Coordinates inspectedTile, ChessPieceColor kingColor);
}