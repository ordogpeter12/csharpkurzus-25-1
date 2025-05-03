namespace JRA12L.View;

public interface IMenuView : IDisposable
{
    void DrawMenu(string title, string[] menuItems, int currentIndex, Dictionary<string, string> controls);

}