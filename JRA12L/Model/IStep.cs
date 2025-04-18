namespace JRA12L;

public interface IStep
{
    enum Figure : byte
    {
        WhiteKing = 33,
        WhiteQueen = 34,
        WhiteRook = 35,
        WhiteBishop = 36,
        WhiteKnight = 37,
        WhitePawn = 38,
        BlackKing = 39,
        BlackQueen = 40,
        BlackRook = 41,
        BlackBishop = 42,
        BlackKnight = 43,
        BlackPawn = 44,
        BlankTile = 45,
    }

    int GetXAxisLenght();
    int GetYAxisLenght();
    Figure this[int index] { get; }
}