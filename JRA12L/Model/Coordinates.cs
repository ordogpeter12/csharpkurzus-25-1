namespace JRA12L.Model;

public struct Coordinates(sbyte x, sbyte y) : IComparable<Coordinates>
{
    public sbyte X = x;
    public sbyte Y = y;
    public int CompareTo(Coordinates other)
    {
        if(Y == other.Y)
        {
            return X-other.X;
        }
        return Y-other.Y;
    }

    public override bool Equals(object? obj)
    {
        if(obj is Coordinates coordinates)
        {
            return this.X == coordinates.X && coordinates.Y == this.Y;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }
}