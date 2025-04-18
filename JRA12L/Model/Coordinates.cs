using System.Security.Cryptography;

namespace JRA12L;

public class Coordinates
{
    public byte X;
    public byte Y;

    public Coordinates(byte x, byte y)
    {
        X = x;
        Y = y;
    }
}