namespace JRA12L;

public sealed record Step : IStep
{
    private IFigure[] _tableValue { get; }
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    private bool _shortCastle = true;
    private bool _longCastle = true;
    public Step(Step step)
    {
        _tableValue = step._tableValue;
        _shortCastle = step._shortCastle;
        _longCastle = step._longCastle;
    }

    public Step()
    {
        //chess starting position
        _tableValue = new IFigure[]
        {
            new BlackRook(), new BlackKnight(), new BlackBishop(), new BlackQueen(), new BlackKing(), new BlackBishop(), new BlackKnight(), new BlackRook(),
            new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(),
            new WhiteRook(), new WhiteKnight(), new WhiteBishop(), new WhiteQueen(), new WhiteKing(), new WhiteBishop(), new WhiteKnight(), new WhiteRook(),
        };
    }
    int IStep.GetXAxisLenght() => XAxisLenght;

    int IStep.GetYAxisLenght() => YAxisLenght;

    public IFigure this[byte x, byte y] { get => _tableValue[y*XAxisLenght+x]; }
}