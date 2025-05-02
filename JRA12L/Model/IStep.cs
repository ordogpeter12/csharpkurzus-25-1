namespace JRA12L;

public interface IStep
{
    int GetXAxisLenght();
    int GetYAxisLenght();
    bool WhiteShortCastle { get; }
    bool WhiteLongCastle { get; }
    bool BlackShortCastle { get; }
    bool BlackLongCastle { get; }
    Figure this[sbyte x, sbyte y] { get; }
    Figure this[Coordinates coordinates] { get; }
    IStep GetNextStep(Coordinates movedPiece, Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput);
    bool IsKingSafe(Coordinates inspectedTile, ChessPieceColor kingColor);
    List<Coordinates> GetCheckingFigures(Coordinates inspectedTile, ChessPieceColor kingColor);
}