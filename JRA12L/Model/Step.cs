using System.Drawing;

namespace JRA12L;

public sealed record Step : IStep
{
    private readonly Figure[] _tableValue;
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    public bool WhiteShortCastle { get; } = true;
    public bool WhiteLongCastle { get; } = true;
    public bool BlackShortCastle { get; } = true;
    public bool BlackLongCastle { get; } = true;

    public Step(Step step)
    {
        _tableValue = step._tableValue.Clone() as Figure[];
        WhiteShortCastle = step.WhiteShortCastle;
        WhiteLongCastle = step.WhiteLongCastle;
        BlackShortCastle = step.BlackShortCastle;
        BlackLongCastle = step.BlackLongCastle;
    }
    public Step()
    {
        //chess starting position
        _tableValue =
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

    public IStep GetNextStep(Coordinates movedPiece, Coordinates destination)
    {
        Step nextStep = new(this);
        nextStep._tableValue[destination.Y*XAxisLenght+destination.X] = nextStep[movedPiece];
        nextStep._tableValue[movedPiece.Y*XAxisLenght+movedPiece.X] = new BlankTile();
        return nextStep;
    }

    public bool IsKingSafe(Coordinates inspectedTile, ChessPieceColor kingColor)
    {
        //pawns
        if(kingColor == ChessPieceColor.White && CoveredByBlackPawn(inspectedTile) != null)
            return false;
        if(kingColor == ChessPieceColor.Black && CoveredByWhitePawn(inspectedTile) != null)
            return false;
        //opposition
        if(IsKingOnNeighboringTile(inspectedTile, kingColor))
            return false;
        //queen, rook
        if(CoveredByStraightMovingPiece(inspectedTile, kingColor) != null)
            return false;
        //queen, bishop
        if(CoveredByDiagonallyMovingPiece(inspectedTile, kingColor) != null)
            return false;
        if(CoveredByKnight(inspectedTile, kingColor) != null)
            return false;
        return true;
    }
    public List<Coordinates> GetCheckingFigures(Coordinates inspectedTile, ChessPieceColor kingColor)
    {
        List<Coordinates> checkingFigures = [];
        //pawn
        Coordinates? functionReturn = CoveredByBlackPawn(inspectedTile);
        if(kingColor == ChessPieceColor.White && functionReturn != null)
            checkingFigures.Add(functionReturn.Value);
        functionReturn = CoveredByWhitePawn(inspectedTile);
        if(kingColor == ChessPieceColor.Black && functionReturn != null)
            checkingFigures.Add(functionReturn.Value);
        //queen, rook
        functionReturn = CoveredByStraightMovingPiece(inspectedTile, kingColor);
        if(CoveredByStraightMovingPiece(inspectedTile, kingColor) != null)
            checkingFigures.Add(functionReturn.Value);
        //queen, bishop
        functionReturn = CoveredByDiagonallyMovingPiece(inspectedTile, kingColor);
        if(CoveredByDiagonallyMovingPiece(inspectedTile, kingColor) != null)
            checkingFigures.Add(functionReturn.Value);
        //knight
        functionReturn = CoveredByKnight(inspectedTile, kingColor);
        if(CoveredByKnight(inspectedTile, kingColor) != null)
            checkingFigures.Add(functionReturn.Value);
        return checkingFigures;
    }
    public Figure this[sbyte x, sbyte y] { get => _tableValue[y*XAxisLenght+x]; }
    
    public Figure this[Coordinates coordinates] { get => _tableValue[coordinates.Y*XAxisLenght+coordinates.X]; }
    private Coordinates? CoveredByWhitePawn(Coordinates inspectedTile)
    {
        if(inspectedTile.Y+1 < YAxisLenght)
        {
            Coordinates suspectedPawnLeft = new((sbyte)(inspectedTile.X-1), (sbyte)(inspectedTile.Y+1));
            Coordinates suspectedPawnRight = new((sbyte)(inspectedTile.X+1), (sbyte)(inspectedTile.Y+1));
            if(suspectedPawnLeft.X >= 0 && this[suspectedPawnLeft].GetChessPieceColor() == ChessPieceColor.White
                && this[suspectedPawnLeft].GetChessPieceType() == ChessPieceType.Pawn)
            {
                return suspectedPawnLeft;
            }
            if(suspectedPawnRight.X < XAxisLenght
               && this[suspectedPawnRight].GetChessPieceColor() == ChessPieceColor.White 
                && this[suspectedPawnRight].GetChessPieceType() == ChessPieceType.Pawn)
            {
                Console.WriteLine(inspectedTile);
                return suspectedPawnRight;
            }
        }
        return null;
    }
    private Coordinates? CoveredByBlackPawn(Coordinates inspectedTile)
    {
        if(inspectedTile.Y-1 > 0)
        {
            Coordinates suspectedPawnLeft = new((sbyte)(inspectedTile.X+1), (sbyte)(inspectedTile.Y-1));
            Coordinates suspectedPawnRight = new((sbyte)(inspectedTile.X-1), (sbyte)(inspectedTile.Y+1));
            if(suspectedPawnLeft.X >= 0 && this[suspectedPawnLeft].GetChessPieceColor() == ChessPieceColor.Black
                && this[suspectedPawnLeft].GetChessPieceType() == ChessPieceType.Pawn)
            {
                return suspectedPawnLeft;
            }
            if(suspectedPawnRight.X < XAxisLenght
                && this[suspectedPawnRight].GetChessPieceColor() == ChessPieceColor.Black 
                       && this[suspectedPawnRight].GetChessPieceType() == ChessPieceType.Pawn)
            {
                return suspectedPawnRight;
            }
        }
        return null;
    }
    private bool IsKingOnNeighboringTile(Coordinates inspectedTile, ChessPieceColor kingColor)
    {
        Coordinates loopStartingCoordinates = new(inspectedTile.X == 0 ? inspectedTile.X: (sbyte)(inspectedTile.X-1), 
            inspectedTile.Y == 0 ? inspectedTile.Y: (sbyte)(inspectedTile.Y-1));
        Coordinates loopEndingCoordinates = new(
            inspectedTile.X < XAxisLenght-1 ? (sbyte)(inspectedTile.X+1) : inspectedTile.X,
            inspectedTile.Y < YAxisLenght-1 ? (sbyte)(inspectedTile.Y+1) : inspectedTile.Y);
        for(sbyte x = loopStartingCoordinates.X; x <= loopEndingCoordinates.X; x++)
        {
            for(sbyte y = loopStartingCoordinates.Y; y <= loopEndingCoordinates.Y; y++)
            {
                if(this[x, y].GetChessPieceColor() != kingColor
                    && this[x, y].GetChessPieceType() == ChessPieceType.King)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private Coordinates? CoveredByDiagonallyMovingPiece(Coordinates inspectedTile, ChessPieceColor kingColor)
    {
        //up left
        sbyte x = (sbyte)(inspectedTile.X-1);
        sbyte y = (sbyte)(inspectedTile.Y-1);
        while(x >= 0 && y >= 0)
        {
            if(this[x, y].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                if (this[x, y].GetChessPieceColor() != kingColor
                    && (this[x, y].GetChessPieceType() == ChessPieceType.Bishop 
                        || this[x, y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(x, y);
                }
                break;
            }
            x--;
            y--;
        }
        //up right
        x = (sbyte)(inspectedTile.X+1); 
        y = (sbyte)(inspectedTile.Y-1);
        while(x < XAxisLenght && y >= 0)
        {
            if(this[x, y].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                if (this[x, y].GetChessPieceColor() != kingColor
                    && (this[x, y].GetChessPieceType() == ChessPieceType.Bishop 
                        || this[x, y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(x, y);
                }
                break;
            }
            x++;
            y--;
        }
        //down left
        x = (sbyte)(inspectedTile.X-1); 
        y = (sbyte)(inspectedTile.Y+1);
        while(x >= 0 && y < YAxisLenght)
        {
            if(this[x, y].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                if (this[x, y].GetChessPieceColor() != kingColor
                    && (this[x, y].GetChessPieceType() == ChessPieceType.Bishop 
                        || this[x, y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(x, y);
                }
                break;
            }
            x--;
            y++;
        }
        //down right
        x = (sbyte)(inspectedTile.X+1); 
        y = (sbyte)(inspectedTile.Y+1);
        while(x < XAxisLenght && y < YAxisLenght)
        {
            if(this[x, y].GetChessPieceColor() == ChessPieceColor.Blank)
            {
                if (this[x, y].GetChessPieceColor() != kingColor
                    && (this[x, y].GetChessPieceType() == ChessPieceType.Bishop 
                        || this[x, y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(x, y);
                }
                break;
            }
            x++;
            y++;
        }
        return null;
    }
    private Coordinates? CoveredByStraightMovingPiece(Coordinates inspectedTile, ChessPieceColor kingColor)
    {
        for(sbyte x = (sbyte)(inspectedTile.X+1); x < XAxisLenght; x++)
        {
            if(this[x, inspectedTile.Y].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if(this[x, inspectedTile.Y].GetChessPieceColor() != kingColor
                   && (this[x, inspectedTile.Y].GetChessPieceType() == ChessPieceType.Rook
                       || this[x, inspectedTile.Y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(x, inspectedTile.Y);
                }
                break;
            }
        }
        for(sbyte x = (sbyte)(inspectedTile.X-1); x >= 0; x--)
        {
            if(this[x, inspectedTile.Y].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if(this[x, inspectedTile.Y].GetChessPieceColor() != kingColor
                   && (this[x, inspectedTile.Y].GetChessPieceType() == ChessPieceType.Rook
                       || this[x, inspectedTile.Y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(x, inspectedTile.Y);
                }
                break;
            }
        }
        for(sbyte y = (sbyte)(inspectedTile.Y+1); y < YAxisLenght; y++)
        {
            if(this[inspectedTile.X, y].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if(this[inspectedTile.X, y].GetChessPieceColor() != kingColor
                   && (this[inspectedTile.X, y].GetChessPieceType() == ChessPieceType.Rook
                       || this[inspectedTile.X, y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(inspectedTile.X, y);
                }
                break;
            }
        }
        for(sbyte y = (sbyte)(inspectedTile.Y-1); y >= 0; y--)
        {
            if(this[inspectedTile.X, y].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if(this[inspectedTile.X, y].GetChessPieceColor() != kingColor
                   && (this[inspectedTile.X, y].GetChessPieceType() == ChessPieceType.Rook
                       || this[inspectedTile.X, y].GetChessPieceType() == ChessPieceType.Queen))
                {
                    return new Coordinates(inspectedTile.X, y);
                }
                break;
            }
        }
        return null;
    }
    private Coordinates? CoveredByKnight(Coordinates inspectedTile, ChessPieceColor kingColor)
    {
        List<Coordinates> possibleKnights = new WhiteKnight().GetValidMoves(new ChessTable(this), inspectedTile);
        foreach(var possibleKnight in possibleKnights)
        {
            if (this[possibleKnight].GetChessPieceColor() != kingColor
                && this[possibleKnight].GetChessPieceType() == ChessPieceType.Knight)
            {
                return possibleKnight;
            }
        }
        return null;
    }
}