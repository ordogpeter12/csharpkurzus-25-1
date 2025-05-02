using JRA12L.Model;
using JRA12L.Model.Figures;
using JRA12L.View;

namespace JRA12L.Core.ChessGame;

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
    ChessPieceColor WhoseTurn { get; }
    IStep GetNextStep(Coordinates movedPiece, Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput);
    bool IsKingSafe(Coordinates inspectedTile, ChessPieceColor kingColor);
    List<Coordinates> GetChecks();
}