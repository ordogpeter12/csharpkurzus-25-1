using JRA12L.Factories;
using JRA12L.Model;
using JRA12L.Model.Figures;
using JRA12L.Model.Figures.Black;
using JRA12L.Model.Figures.White;
using JRA12L.View;

namespace JRA12L.Core.ChessGame;

public sealed record Step : IStep
{
    private readonly Figure[] _tableValue;
    private const int XAxisLenght = 8;
    private const int YAxisLenght = 8;
    public bool WhiteShortCastle { get; private set; } = true;
    public bool WhiteLongCastle { get; private set; } = true;
    public bool BlackShortCastle { get; private set; } = true;
    public bool BlackLongCastle { get; private set; } = true;
    public ChessPieceColor WhoseTurn { get; private set; } = ChessPieceColor.White; //true if white's

    public Step(Step step)
    {
        _tableValue = step._tableValue.Clone() as Figure[];
        WhiteShortCastle = step.WhiteShortCastle;
        WhiteLongCastle = step.WhiteLongCastle;
        BlackShortCastle = step.BlackShortCastle;
        BlackLongCastle = step.BlackLongCastle;
    }

    public Step(Figure[] tableValue, bool whiteShortCastle, bool whiteLongCastle, bool blackShortCastle,
        bool blackLongCastle, ChessPieceColor whoseTurn)
    {
        if(tableValue.Length != XAxisLenght*YAxisLenght)
            throw new ArgumentException("The figure array lenght is invalid.");
        _tableValue = tableValue;
        WhiteShortCastle = whiteShortCastle;
        WhiteLongCastle = whiteLongCastle;
        BlackShortCastle = blackShortCastle;
        BlackLongCastle = blackLongCastle;
        WhoseTurn = whoseTurn;
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

    public StepSavableInformation GetSavableInformation()
    {
        return new StepSavableInformation
        {
            TableValue = string.Join("", _tableValue.Select(
                t => new string(new[] {(char)t.GetChessPieceColor(), (char)t.GetChessPieceType()}))),
            WhiteShortCastle = this.WhiteShortCastle,
            WhiteLongCastle = this.WhiteLongCastle,
            BlackShortCastle = this.BlackShortCastle,
            BlackLongCastle = this.BlackLongCastle,
            WhoseTurn = this.WhoseTurn,
        };
    }
    private Coordinates GetKingPosition(ChessPieceColor color)
    {
        for(int i = 0; i < _tableValue.Length; i++)
        {
            if(_tableValue[i].GetChessPieceType() == ChessPieceType.King
                && _tableValue[i].GetChessPieceColor() == color)
            {
                return new Coordinates((sbyte)(i%XAxisLenght), (sbyte)(i/XAxisLenght));
            }
        }
        throw new InvalidOperationException("There is no king");
    }

    //last list element is the king
    public List<Coordinates> GetChecks()
    {
        ChessPieceColor currentKingColor = WhoseTurn;
        Coordinates kingCoordinates = GetKingPosition(currentKingColor);
        List<Coordinates> checkingFigures = GetCheckingFigures(kingCoordinates, currentKingColor);
        if(checkingFigures.Count != 0)
        {
            checkingFigures.Add(kingCoordinates);
        }
        return checkingFigures;
    }
    public IStep GetNextStep(Coordinates movedPiece, Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput)
    {
        Step nextStep = new(this);
        nextStep._tableValue[destination.Y*XAxisLenght+destination.X] = nextStep[movedPiece];
        nextStep._tableValue[movedPiece.Y*XAxisLenght+movedPiece.X] = new BlankTile();
        CleanUpAfterSpecialMoves(nextStep, movedPiece, destination, promotionMenu, userInput);
        nextStep.WhoseTurn = this.WhoseTurn == ChessPieceColor.White ? ChessPieceColor.Black : ChessPieceColor.White;
        return nextStep;
    }
    private void CleanUpAfterSpecialMoves(Step nextStep, Coordinates movedPiece, Coordinates destination, Action<string[], int> promotionMenu, IUserInput userInput)
    {
        //en passant clean up (not color dependent)
        if (this[movedPiece].GetChessPieceType() == ChessPieceType.Pawn
            && movedPiece.Y != destination.Y && this[destination].GetChessPieceColor() == ChessPieceColor.Blank)
        {
            nextStep._tableValue[movedPiece.Y*XAxisLenght+destination.X] = new BlankTile();
        }
        //promotion
        if (this[movedPiece].GetChessPieceType() == ChessPieceType.Pawn
            && destination.Y is 0 or YAxisLenght - 1)
        {
            nextStep._tableValue[destination.Y*XAxisLenght+destination.X] = 
                PromotionChoice(promotionMenu, userInput, this[movedPiece].GetChessPieceColor());
        }
        //king move
        if(this[movedPiece].GetChessPieceType() == ChessPieceType.King)
        {
            if(this[movedPiece].GetChessPieceColor() == ChessPieceColor.White)
            {
                nextStep.WhiteShortCastle = false;
                nextStep.WhiteLongCastle = false;
            }
            else
            {
                nextStep.BlackShortCastle = false;
                nextStep.BlackLongCastle = false;
            }
            //if castle
            if(movedPiece.X - destination.X == -2)
            {
                nextStep._tableValue[destination.Y * XAxisLenght + (sbyte)(destination.X-1)] =
                    nextStep._tableValue[destination.Y * XAxisLenght + (sbyte)(destination.X+1)];
                nextStep._tableValue[destination.Y * XAxisLenght + (sbyte)(destination.X+1)] = new BlankTile();
            }
            else if (movedPiece.X - destination.X == 2)
            {
                nextStep._tableValue[destination.Y * XAxisLenght + (sbyte)(destination.X+1)] =
                    nextStep._tableValue[destination.Y * XAxisLenght + (sbyte)(destination.X-2)];
                nextStep._tableValue[destination.Y * XAxisLenght + (sbyte)(destination.X-2)] = new BlankTile();
            }
        }
        //rook move
        if(this[movedPiece].GetChessPieceType() == ChessPieceType.Rook)
        {
            if(this[movedPiece].GetChessPieceColor() == ChessPieceColor.White)
            {
                if(movedPiece.X == 0)
                {
                    nextStep.WhiteLongCastle = false;
                }
                else if(movedPiece.X == XAxisLenght-1)
                {
                    nextStep.WhiteShortCastle = false;
                }
            }
            else
            {
                if(movedPiece.X == 0)
                {
                    nextStep.BlackLongCastle = false;
                }
                else if(movedPiece.X == XAxisLenght-1)
                {
                    nextStep.BlackShortCastle = false;
                }
            }
        }
    }

    private Figure PromotionChoice(Action<string[], int> promotionMenu, IUserInput userInput, ChessPieceColor color)
    {
        bool selected = false;
        int index = 0;
        string[] figures = ["Queen", "Rook", "Bishop", "Knight"];
        ChessPieceType[] figureTypesInternal = [ChessPieceType.Queen, ChessPieceType.Rook, ChessPieceType.Bishop, ChessPieceType.Knight];
        while(!selected)
        {
            promotionMenu(figures, index);
            switch((IUserInput.UserInput)userInput.GetUserInput())
            {
                case IUserInput.UserInput.Left:
                    if(index != 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = figures.Length-1;
                    }
                    break;
                case IUserInput.UserInput.Right:
                    if(index != figures.Length-1)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                    break;
                case IUserInput.UserInput.Select:
                    selected = true;
                    break;
            }
        }
        return new BaseChessSimpleFigureFactory().GetFigure(color, figureTypesInternal[index]);
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
            Coordinates suspectedPawnLeft = new((sbyte)(inspectedTile.X-1), (sbyte)(inspectedTile.Y-1));
            Coordinates suspectedPawnRight = new((sbyte)(inspectedTile.X+1), (sbyte)(inspectedTile.Y-1));
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
            if(this[x, y].GetChessPieceColor() != ChessPieceColor.Blank)
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
            if(this[x, y].GetChessPieceColor() != ChessPieceColor.Blank)
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
            if(this[x, y].GetChessPieceColor() != ChessPieceColor.Blank)
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
            if(this[x, y].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if(this[x, y].GetChessPieceColor() != kingColor
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
        List<Coordinates> possibleKnights = kingColor == ChessPieceColor.White ?
            new WhiteKnight().GetValidMoves(new ChessTable(this), inspectedTile): new BlackKnight().GetValidMoves(new ChessTable(this), inspectedTile);
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