namespace JRA12L;

public class Coordinates(sbyte x, sbyte y) : IComparable<Coordinates>
{
    public sbyte X = x;
    public sbyte Y = y;

    public Coordinates(Coordinates other) : this(other.X, other.Y) {}
    public int CompareTo(Coordinates other)
    {
        if(Y == other.Y)
        {
            return X-other.X;
        }
        return Y-other.Y;
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }
}