using JRA12L.Core.Menu;
using JRA12L.View;

namespace JRA12L;

class Program
{
    static void Main(string[] args)
    {
        using IMenuView menuView = new ConsoleMenu();
        Menu menu = new Menu(new ConsoleUserInput(), menuView);
        menu.StartApp();
    }
}