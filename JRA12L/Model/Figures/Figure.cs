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
    //If it finds any other figure at some point the loop breaks and returns with true.
    //The other "pinned" functions work similarly
    protected bool NotLinearIncreasingPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        Coordinates loopCoordinates = inspectedFigure;
        loopCoordinates.X++;
        loopCoordinates.Y--;
        while(loopCoordinates.X < step.GetXAxisLenght() && loopCoordinates.Y >= 0)
        {
            if(step[loopCoordinates].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if (step[loopCoordinates].GetChessPieceType() == ChessPieceType.King
                    && step[loopCoordinates].GetChessPieceColor() == pieceColor)
                {
                    loopCoordinates.X = (sbyte)(inspectedFigure.X - 1);
                    loopCoordinates.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(loopCoordinates.X >= 0 && loopCoordinates.Y < step.GetYAxisLenght())
                    {
                        if (step[loopCoordinates].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if ((step[loopCoordinates].GetChessPieceType() == ChessPieceType.Queen
                                 || step[loopCoordinates].GetChessPieceType() == ChessPieceType.Bishop)
                                && step[loopCoordinates].GetChessPieceColor() != pieceColor)
                            {
                                return false;
                            }
                            break;
                        }
                        loopCoordinates.X--;
                        loopCoordinates.Y++;
                    }
                }
                else if ((step[loopCoordinates].GetChessPieceType() == ChessPieceType.Queen
                          || step[loopCoordinates].GetChessPieceType() == ChessPieceType.Bishop)
                         && step[loopCoordinates].GetChessPieceColor() != pieceColor)
                {
                    loopCoordinates.X = (sbyte)(inspectedFigure.X - 1);
                    loopCoordinates.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(loopCoordinates.X >= 0 && loopCoordinates.Y < step.GetYAxisLenght())
                    {
                        if(step[loopCoordinates].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if (step[loopCoordinates].GetChessPieceType() == ChessPieceType.King
                                && step[loopCoordinates].GetChessPieceColor() == pieceColor)
                            {
                                return false;
                            }
                            break;
                        }
                        loopCoordinates.X--;
                        loopCoordinates.Y++;
                    }
                }
                break;
            }
            loopCoordinates.X++;
            loopCoordinates.Y--;
        }
        return true;
    }
    protected bool NotLinearDecreasingPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        Coordinates loopCoordinates = inspectedFigure;
        loopCoordinates.X--;
        loopCoordinates.Y--;
        while(loopCoordinates is { X: >= 0, Y: >= 0 })
        {
            if(step[loopCoordinates].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if (step[loopCoordinates].GetChessPieceType() == ChessPieceType.King
                    && step[loopCoordinates].GetChessPieceColor() == pieceColor)
                {
                    loopCoordinates.X = (sbyte)(inspectedFigure.X + 1);
                    loopCoordinates.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(loopCoordinates.X < step.GetXAxisLenght() && loopCoordinates.Y < step.GetYAxisLenght())
                    {
                        if (step[loopCoordinates].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if ((step[loopCoordinates].GetChessPieceType() == ChessPieceType.Queen
                                 || step[loopCoordinates].GetChessPieceType() == ChessPieceType.Bishop)
                                && step[loopCoordinates].GetChessPieceColor() != pieceColor)
                            {
                                return false;
                            }
                            break;
                        }
                        loopCoordinates.X++;
                        loopCoordinates.Y++;
                    }
                }
                else if ((step[loopCoordinates].GetChessPieceType() == ChessPieceType.Queen
                          || step[loopCoordinates].GetChessPieceType() == ChessPieceType.Bishop)
                         && step[loopCoordinates].GetChessPieceColor() != pieceColor)
                {
                    loopCoordinates.X = (sbyte)(inspectedFigure.X + 1);
                    loopCoordinates.Y = (sbyte)(inspectedFigure.Y + 1);
                    while(loopCoordinates.X < step.GetXAxisLenght() && loopCoordinates.Y < step.GetYAxisLenght())
                    {
                        if(step[loopCoordinates].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if (step[loopCoordinates].GetChessPieceType() == ChessPieceType.King
                                && step[loopCoordinates].GetChessPieceColor() == pieceColor)
                            {
                                return false;
                            }
                            break;
                        }
                        loopCoordinates.X++;
                        loopCoordinates.Y++;
                    }
                }
                break;
            }
            loopCoordinates.X--;
            loopCoordinates.Y--;
        }
        return true;
    }
    protected bool NotColumnPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        for(sbyte i = (sbyte)(inspectedFigure.Y-1); i >= 0; i--)
        {
            if(step[inspectedFigure.X, i].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if((step[inspectedFigure.X, i].GetChessPieceType() == ChessPieceType.Queen
                     || step[inspectedFigure.X, i].GetChessPieceType() == ChessPieceType.Rook)
                    && step[inspectedFigure.X, i].GetChessPieceColor() != pieceColor)
                {
                    for(sbyte j = (sbyte)(inspectedFigure.Y+1); j < step.GetYAxisLenght(); j++)
                    {
                        if(step[inspectedFigure.X, j].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if (step[inspectedFigure.X, j].GetChessPieceType() == ChessPieceType.King
                                && step[inspectedFigure.X, j].GetChessPieceColor() == pieceColor)
                            {
                                return false;
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
                        if(step[inspectedFigure.X, j].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if ((step[inspectedFigure.X, j].GetChessPieceType() == ChessPieceType.Queen
                                 || step[inspectedFigure.X, j].GetChessPieceType() == ChessPieceType.Rook)
                                && step[inspectedFigure.X, j].GetChessPieceColor() != pieceColor)
                            {
                                return false;
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
        return true;
    }
    protected bool NotRowPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        for(sbyte i = (sbyte)(inspectedFigure.X-1); i >= 0; i--)
        {
            if(step[i, inspectedFigure.Y].GetChessPieceColor() != ChessPieceColor.Blank)
            {
                if((step[i, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Queen
                     || step[i, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Rook)
                    && step[i, inspectedFigure.Y].GetChessPieceColor() != pieceColor)
                {
                    for(sbyte j = (sbyte)(inspectedFigure.Y+1); j < step.GetYAxisLenght(); j++)
                    {
                        if(step[j, inspectedFigure.Y].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if (step[j, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.King
                                && step[j, inspectedFigure.Y].GetChessPieceColor() == pieceColor)
                            {
                                return false;
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
                        if(step[j, inspectedFigure.Y].GetChessPieceColor() != ChessPieceColor.Blank)
                        {
                            if ((step[j, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Queen
                                 || step[j, inspectedFigure.Y].GetChessPieceType() == ChessPieceType.Rook)
                                && step[j, inspectedFigure.Y].GetChessPieceColor() != pieceColor)
                            {
                                return false;
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
        return true;
    }
}