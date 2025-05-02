namespace JRA12L.View;

public interface IMenuView : IDisposable
{
    void DrawMenu(string[] menuItems, int currentIndex);
    
}