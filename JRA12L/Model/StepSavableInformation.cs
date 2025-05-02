using JRA12L.Model.Figures;

namespace JRA12L.Model;

public record StepSavableInformation
{
    public Figure[] TableValue;
    public bool WhiteShortCastle { get; set; }
    public bool WhiteLongCastle { get; set; }
    public bool BlackShortCastle { get; set; }
    public bool BlackLongCastle { get; set; }
    public ChessPieceColor WhoseTurn { get; set; }
}