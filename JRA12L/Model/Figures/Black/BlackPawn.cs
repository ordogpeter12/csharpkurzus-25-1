namespace JRA12L;

public class BlackPawn : Figure
{
    private const ChessPieceColor Color = ChessPieceColor.Black;
    private const ChessPieceType Type = ChessPieceType.Pawn;
    public override ChessPieceColor GetChessPieceColor() => Color;
    public override ChessPieceType GetChessPieceType() => Type;
    public override List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        IStep currentStep = table.GetCurrentStep();
        if(NotRowPinned(table.GetCurrentStep(), figureCoordinates, Color))
        {
            if(NotColumnPinned(table.GetCurrentStep(), figureCoordinates, Color))
            {
                if(LeftTakeValid(currentStep, figureCoordinates) is {} left)
                    validMoves.Add(left);
                if(RightTakeValid(currentStep, figureCoordinates) is {} right)
                    validMoves.Add(right);
                if(EnPassantValids(currentStep, table.GetPreviousStep(), figureCoordinates) is {} enPassant)
                    validMoves.Add(enPassant);
            }
            validMoves.AddRange(ForwardValids(currentStep, figureCoordinates));
        }
        return validMoves;
    }

    private List<Coordinates> ForwardValids(IStep currentStep, Coordinates figureCoordinates)
    {
        List<Coordinates> validMoves = [];
        if(currentStep[figureCoordinates.X, (sbyte)(figureCoordinates.Y+1)]
               .GetChessPieceColor() == ChessPieceColor.Blank
           && NotLinearIncreasingPinned(currentStep, figureCoordinates, Color)
           && NotLinearDecreasingPinned(currentStep, figureCoordinates, Color))
        {
            validMoves.Add(new Coordinates(figureCoordinates.X, (sbyte)(figureCoordinates.Y+1)));
            //if it is on the starting position
            if (figureCoordinates.Y == 1 && currentStep[figureCoordinates.X, (sbyte)(figureCoordinates.Y+2)]
                    .GetChessPieceColor() == ChessPieceColor.Blank)
            {
                validMoves.Add(new Coordinates(figureCoordinates.X, (sbyte)(figureCoordinates.Y+2)));
            }
        }
        return validMoves;
    }
    private Coordinates? LeftTakeValid(IStep currentStep, Coordinates figureCoordinates)
    {
        if(currentStep[(sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y+1)].GetChessPieceColor() != ChessPieceColor.Blank
           && currentStep[(sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y+1)].GetChessPieceColor() != Color
           && NotLinearIncreasingPinned(currentStep, figureCoordinates, Color))
        {
            return new Coordinates((sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y+1));
        }
        return null;
    }
    private Coordinates? RightTakeValid(IStep currentStep, Coordinates figureCoordinates)
    {
        if(currentStep[(sbyte)(figureCoordinates.X+1), (sbyte)(figureCoordinates.Y+1)].GetChessPieceColor() != ChessPieceColor.Blank
           && currentStep[(sbyte)(figureCoordinates.X+1), (sbyte)(figureCoordinates.Y+1)].GetChessPieceColor() != Color
           && NotLinearDecreasingPinned(currentStep, figureCoordinates, Color))
        {
            return new Coordinates((sbyte)(figureCoordinates.X + 1), (sbyte)(figureCoordinates.Y + 1));
        }
        return null;
    }
    private Coordinates? EnPassantValids(IStep currentStep, IStep? previousStep, Coordinates figureCoordinates)
    {
        //en passant
        //checking if the figure was pinned before enemy pawn made the move,
        //because en passant will remove 2 figures from the row
        if(previousStep != null && NotRowPinned(previousStep, figureCoordinates, Color)
                                && figureCoordinates.Y == currentStep.GetYAxisLenght()-4)
        {
            if(currentStep[(sbyte)(figureCoordinates.X-1), figureCoordinates.Y]
                   .GetChessPieceType() == ChessPieceType.Pawn
               && currentStep[(sbyte)(figureCoordinates.X-1), figureCoordinates.Y]
                   .GetChessPieceColor() != Color
               && previousStep[(sbyte)(figureCoordinates.X-1), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
               && previousStep[(sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y+1)].GetChessPieceColor() == ChessPieceColor.Blank
               && NotLinearIncreasingPinned(currentStep, figureCoordinates, Color))
            {
                return new Coordinates((sbyte)(figureCoordinates.X-1), (sbyte)(figureCoordinates.Y+1));
            }
            else if(currentStep[(sbyte)(figureCoordinates.X+1), figureCoordinates.Y]
                     .GetChessPieceType() == ChessPieceType.Pawn
                 && currentStep[(sbyte)(figureCoordinates.X+1), figureCoordinates.Y]
                     .GetChessPieceColor() != Color
                 && previousStep[(sbyte)(figureCoordinates.X+1), figureCoordinates.Y].GetChessPieceColor() == ChessPieceColor.Blank
                 && previousStep[(sbyte)(figureCoordinates.X+1), (sbyte)(figureCoordinates.Y+1)].GetChessPieceColor() == ChessPieceColor.Blank
                 && NotLinearDecreasingPinned(currentStep, figureCoordinates, Color))
            {
                return new Coordinates((sbyte)(figureCoordinates.X+1), (sbyte)(figureCoordinates.Y+1));
            }
        }
        return null;
    }
    public override string ToString()
    {
        return " \u265f ";
    }
}