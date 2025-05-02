namespace JRA12L;

public interface IView : IDisposable
{
    void Draw(IStep step, Coordinates playerCoordinates, List<Coordinates> checks,
        List<Coordinates>? reverseOrderedPossibleMoves = null);
    void DrawPromotionMenu(string[] figureStrings, int currentIndex);
}