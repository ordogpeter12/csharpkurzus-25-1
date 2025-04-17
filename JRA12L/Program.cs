namespace JRA12L;

class Program
{
    static void Main(string[] args)
    {
        IStep step = new Step(")+*('*+),,,,,,,,--------------------------------&&&&&&&&#%$\"!$%#");
        IView view = new ConsoleView();
        view.Draw(step);
    }
}