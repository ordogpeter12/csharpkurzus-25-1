using JRA12L.Core.ChessGame;
using JRA12L.View;

namespace JRA12L.Core.Menu;

public class Menu
{
    private readonly IUserInput _userInput;
    private readonly IMenuView _menu;
    private readonly string _title = "Console chess";
    private readonly string[] _menuItems = [ "New Game", "Load Game", "Delete Save", "Controls", "Exit" ];

    private readonly Dictionary<string, string> _controls = new()
    {
        {"W", "UP" },
        {"S", "DOWN" },
        {"↵", "SELECT" }
    };

    private bool _exit;
    private int _userPosition;

    public Menu(IUserInput userInput, IMenuView menu)
    {
        _userInput = userInput;
        _menu = menu;
        
        _userPosition = 0;
        _exit = false;
    }
    public void StartApp()
    {
        while(!_exit)
        {
            _menu.DrawMenu(_title, _menuItems, _userPosition, _controls);
            switch((IUserInput.UserInput)_userInput.GetUserInput())
            {
                case IUserInput.UserInput.Up:
                    if(_userPosition == 0)
                        _userPosition = _menuItems.Length - 1;
                    else
                        _userPosition--;
                    break;
                case IUserInput.UserInput.Down:
                    if(_userPosition == _menuItems.Length - 1)
                        _userPosition = 0;
                    else
                        _userPosition++;
                    break;
                case IUserInput.UserInput.Select:
                    SelectAction(_userPosition);
                    break;
            }
        }
    }

    private void SelectAction(int userPosition)
    {
        switch(userPosition)
        {
            case 0:
                StartGame();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                _exit = true;
                break;
        }
    }
    private void StartGame()
    {
        using IView view = new ConsoleView();
        Game game = new Game(new ConsoleUserInput(), view, new ChessTable(new Step()));
        game.StartGame();
    }
}