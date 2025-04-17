namespace JRA12L;

public interface IStep
{
    const int XAxisLenght = 8;
    const int YAxisLenght = 8;
    enum Figure : byte
    {
        WhitePawn = 33,
        WhiteKnight = 34,
        WhiteBishop = 35,
        WhiteRook = 36,
        WhiteQueen = 37,
        WhiteKing = 38,
        BlankTile = 39,
        BlackPawn = 40,
        BlackKnight = 41,
        BlackBishop = 42,
        BlackRook = 43,
        BlackQueen = 44,
        BlackKing = 45,
    }
    public Figure this[int index] { get; }
}