using JRA12L.Core;
using JRA12L.Core.ChessGame;
using JRA12L.Model;

namespace JRA12L.View;

public interface IView : IDisposable
{
    void Draw(IStep step, Coordinates playerCoordinates, List<Coordinates> checks,
        List<Coordinates>? reverseOrderedPossibleMoves = null);
    void DrawPromotionMenu(string[] figureStrings, int currentIndex);
}