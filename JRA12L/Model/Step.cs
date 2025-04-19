namespace JRA12L;

public sealed record Step : IStep
{
    private Figure[] TableValue { get; }
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    public Step(Step step)
    {
        TableValue = step.TableValue;
    }
    public Step()
    {
        //chess starting position
        TableValue =
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
    public Figure this[byte x, byte y] { get => TableValue[y*XAxisLenght+x]; }
    
    public Figure this[Coordinates coordinates] { get => TableValue[coordinates.Y*XAxisLenght+coordinates.X]; }
}