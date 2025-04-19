namespace JRA12L;

public sealed record Step : IStep
{
    private Figure[] _tableValue;
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    public Step(Step step)
    {
        _tableValue = step._tableValue;
    }
    public Step()
    {
        //chess starting position
        _tableValue =
        [
            new BlackRook(), new BlackKnight(), new BlackBishop(), new BlackQueen(), new BlackKing(), new BlackBishop(), new BlackKnight(), new BlackRook(),
            new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(), new BlackPawn(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(), new BlankTile(),
            new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(), new WhitePawn(),
            new WhiteRook(), new WhiteKnight(), new WhiteBishop(), new WhiteQueen(), new WhiteKing(), new WhiteBishop(), new WhiteKnight(), new WhiteRook()
        ];
    }
    int IStep.GetXAxisLenght() => XAxisLenght;

    int IStep.GetYAxisLenght() => YAxisLenght;

    public IStep GetNextStep(Coordinates movedPiece, Coordinates destination)
    {
        Step nextStep = new(this);
        nextStep._tableValue[destination.Y*XAxisLenght+destination.X] = nextStep[movedPiece];
        nextStep._tableValue[movedPiece.Y*XAxisLenght+movedPiece.X] = new BlankTile();
        return nextStep;
    }
    public Figure this[sbyte x, sbyte y] { get => _tableValue[y*XAxisLenght+x]; }
    
    public Figure this[Coordinates coordinates] { get => _tableValue[coordinates.Y*XAxisLenght+coordinates.X]; }
}