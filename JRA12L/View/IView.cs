namespace JRA12L;

public interface IView : IDisposable
{
    void Draw(IStep step, Coordinates playerCoordinates, List<Coordinates>? validMoves = null);
    void DrawPromotionMenu(string[] figureStrings, int currentIndex);
}