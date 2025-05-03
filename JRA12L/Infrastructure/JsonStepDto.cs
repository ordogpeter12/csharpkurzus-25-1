using JRA12L.Model.Figures;

namespace JRA12L.Infrastructure;

public record JsonStepDto
{ 
    public string TableValue { get; set; }
    public bool WhiteShortCastle { get; set; }
    public bool WhiteLongCastle { get; set; }
    public bool BlackShortCastle { get; set; }
    public bool BlackLongCastle { get; set; }
    public ChessPieceColor WhoseTurn { get; set; }
}