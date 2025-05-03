using JRA12L.Core.ChessGame;
using JRA12L.Infrastructure;
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
        {"w", "UP" },
        {"s", "DOWN" },
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
                GetSaves("Saves", LoadGame);
                break;
            case 2:
                GetSaves("Delete saves", DeleteSave);
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

    private void GetSaves(string title, Action<string> action)
    {
        Dictionary<string, string> localControls = new()
        {
            { "w", "UP" }, { "s", "DOWN" }, { "a", "BACK" }, { "↵", "SELECT" }
        };
        string[] localMenuItems = SaveDirectoryReader.GetSaves();
        int localPosition = 0;
        bool selecting = true;
        while(selecting)
        {
            _menu.DrawMenu(title, localMenuItems, localPosition, localControls);
            switch((IUserInput.UserInput)_userInput.GetUserInput())
            {
                case IUserInput.UserInput.Up:
                    if(localPosition == 0)
                        localPosition = localMenuItems.Length - 1;
                    else
                        localPosition--;
                    break;
                case IUserInput.UserInput.Down:
                    if(localPosition == localMenuItems.Length - 1)
                        localPosition = 0;
                    else
                        localPosition++;
                    break;
                case IUserInput.UserInput.Left:
                    selecting = false;
                    break;
                case IUserInput.UserInput.Select:
                    action(localMenuItems[localPosition]);
                    selecting = false;
                    break;
            }
        }
    }
    private static void DeleteSave(string filename)
    {
        SaveDeleter.DeleteSave(filename, ".json");
    }
    private static void LoadGame(string filename)
    {
        //TODO
    }
}