namespace JRA12L;

public interface IView
{
    public void Draw(IStep step, Coordinates playerCoordinates, List<Coordinates>? validMoves = null);
}