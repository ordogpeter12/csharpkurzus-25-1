namespace JRA12L;

public class KingData(Coordinates coordinates, bool isShortCastleAvailable, bool isLongCastleAvailable)
{
    public Coordinates KingPosition { get; set; }
    public bool ShortCastle { get; set; }
    public bool LongCastle { get; set;  }
}