namespace JRA12L;

public class Coordinates(byte x, byte y) : IComparable<Coordinates>
{
    public byte X = x;
    public byte Y = y;

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