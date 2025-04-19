namespace JRA12L;

public abstract class Figure
{
    public abstract ChessPieceColor GetChessPieceColor();
    public abstract ChessPieceType GetChessPieceType();
    public abstract List<Coordinates> GetValidMoves(in ITable table, in Coordinates figureCoordinates);
    
    //diagonal like the function
    //It starts in one direction and checks if there is this Figure.
    //If it finds a king with a same color it starts to iterate in the other direction as well
    //to find a queen or bishop with different color.(Or vice versa)
    //If it finds any other figure at some point the loop breaks and returns with false.
    //The other "pinned" functions work similarly
    protected bool NotLinearIncreasingPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        Coordinates localInspectedFigure = new Coordinates((sbyte)(inspectedFigure.X+1), (sbyte)(inspectedFigure.Y-1));
        while(localInspectedFigure.X < step.GetXAxisLenght() && localInspectedFigure.Y >= 0)
        {
            if(step[localInspectedFigure] is not BlankTile)
            {
                if (step[localInspectedFigure].GetChessPieceType() == ChessPieceType.King
                    && step[localInspectedFigure].GetChessPieceColor() == pieceColor)
                {
                    localInspectedFigure.X = (sbyte)(inspectedFigure.X - 1);
                    localInspectedFigure.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(localInspectedFigure.X >= 0 && localInspectedFigure.Y < step.GetYAxisLenght())
                    {
                        if (step[localInspectedFigure] is not BlankTile)
                        {
                            if ((step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Queen
                                 || step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Bishop)
                                && step[localInspectedFigure].GetChessPieceColor() != pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                        localInspectedFigure.X--;
                        localInspectedFigure.Y++;
                    }
                }
                else if ((step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Queen
                          || step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Bishop)
                         && step[localInspectedFigure].GetChessPieceColor() != pieceColor)
                {
                    localInspectedFigure.X = (sbyte)(inspectedFigure.X - 1);
                    localInspectedFigure.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(localInspectedFigure.X >= 0 && localInspectedFigure.Y < step.GetYAxisLenght())
                    {
                        if(step[localInspectedFigure] is not BlankTile)
                        {
                            if (step[localInspectedFigure].GetChessPieceType() == ChessPieceType.King
                                && step[localInspectedFigure].GetChessPieceColor() == pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                        localInspectedFigure.X--;
                        localInspectedFigure.Y++;
                    }
                }
                break;
            }
            localInspectedFigure.X++;
            localInspectedFigure.Y--;
        }
        return false;
    }
    protected bool NotLinearDecreasingPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        Coordinates localInspectedFigure = new Coordinates((sbyte)(inspectedFigure.X-1), (sbyte)(inspectedFigure.Y-1));
        while(localInspectedFigure.X < step.GetXAxisLenght() && localInspectedFigure.Y >= 0)
        {
            if(step[localInspectedFigure] is not BlankTile)
            {
                if (step[localInspectedFigure].GetChessPieceType() == ChessPieceType.King
                    && step[localInspectedFigure].GetChessPieceColor() == pieceColor)
                {
                    localInspectedFigure.X = (sbyte)(inspectedFigure.X + 1);
                    localInspectedFigure.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(localInspectedFigure.X >= 0 && localInspectedFigure.Y < step.GetYAxisLenght())
                    {
                        if (step[localInspectedFigure] is not BlankTile)
                        {
                            if ((step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Queen
                                 || step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Bishop)
                                && step[localInspectedFigure].GetChessPieceColor() != pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                        localInspectedFigure.X++;
                        localInspectedFigure.Y++;
                    }
                }
                else if ((step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Queen
                          || step[localInspectedFigure].GetChessPieceType() == ChessPieceType.Bishop)
                         && step[localInspectedFigure].GetChessPieceColor() != pieceColor)
                {
                    localInspectedFigure.X = (sbyte)(inspectedFigure.X + 1);
                    localInspectedFigure.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(localInspectedFigure.X >= 0 && localInspectedFigure.Y < step.GetYAxisLenght())
                    {
                        if(step[localInspectedFigure] is not BlankTile)
                        {
                            if (step[localInspectedFigure].GetChessPieceType() == ChessPieceType.King
                                && step[localInspectedFigure].GetChessPieceColor() == pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                        localInspectedFigure.X++;
                        localInspectedFigure.Y++;
                    }
                }
                break;
            }
            localInspectedFigure.X--;
            localInspectedFigure.Y--;
        }
        return false;
    }
    protected bool NotColumnPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        for(sbyte i = (sbyte)(inspectedFigure.Y-1); i >= 0; i--)
        {
            if(step[inspectedFigure.X, i] is not BlankTile)
            {
                if((step[inspectedFigure.X, i].GetChessPieceType() == ChessPieceType.Queen
                     || step[inspectedFigure.X, i].GetChessPieceType() == ChessPieceType.Rook)
                    && step[inspectedFigure.X, i].GetChessPieceColor() != pieceColor)
                {
                    for(sbyte j = (sbyte)(inspectedFigure.Y+1); j < step.GetYAxisLenght(); j++)
                    {
                        if(step[inspectedFigure.X, j] is not BlankTile)
                        {
                            if (step[inspectedFigure.X, j].GetChessPieceType() == ChessPieceType.King
                                && step[inspectedFigure.X, j].GetChessPieceColor() == pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                    }
                }
                else if (step[inspectedFigure.X, i].GetChessPieceType() == ChessPieceType.King
                         && step[inspectedFigure.X, i].GetChessPieceColor() == pieceColor)
                {
                    for(sbyte j = (sbyte)(inspectedFigure.Y+1); j < step.GetYAxisLenght(); j++)
                    {
                        if(step[inspectedFigure.X, j] is not BlankTile)
                        {
                            if ((step[inspectedFigure.X, j].GetChessPieceType() == ChessPieceType.Queen
                                 || step[inspectedFigure.X, j].GetChessPieceType() == ChessPieceType.Rook)
                                && step[inspectedFigure.X, j].GetChessPieceColor() != pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
        return false;
    }
    protected bool NotRowPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        for(sbyte i = (sbyte)(inspectedFigure.X-1); i >= 0; i--)
        {
            if(step[i, inspectedFigure.Y] is not BlankTile)
            {
                if((step[i, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Queen
                     || step[i, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Rook)
                    && step[i, inspectedFigure.Y].GetChessPieceColor() != pieceColor)
                {
                    for(sbyte j = (sbyte)(inspectedFigure.Y+1); j < step.GetYAxisLenght(); j++)
                    {
                        if(step[j, inspectedFigure.Y] is not BlankTile)
                        {
                            if (step[j, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.King
                                && step[j, inspectedFigure.Y].GetChessPieceColor() == pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                    }
                }
                else if (step[i, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.King
                         && step[i, inspectedFigure.Y].GetChessPieceColor() == pieceColor)
                {
                    for(sbyte j = (sbyte)(inspectedFigure.Y+1); j < step.GetYAxisLenght(); j++)
                    {
                        if(step[j, inspectedFigure.Y] is not BlankTile)
                        {
                            if ((step[j, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Queen
                                 || step[j, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Rook)
                                && step[j, inspectedFigure.Y].GetChessPieceColor() != pieceColor)
                            {
                                return true;
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
        return false;
    }
}