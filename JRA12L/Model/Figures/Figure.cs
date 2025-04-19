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
    //If it finds any other figure at sime point the loop breaks and returns with false.
    protected bool NotLinearIncreasingPinned(IStep step, Coordinates inspectedFigure, ChessPieceColor pieceColor)
    {
        if (inspectedFigure.X == 0 || inspectedFigure.Y == 0 || inspectedFigure.X == step.GetXAxisLenght() - 1
            || inspectedFigure.Y == step.GetYAxisLenght() - 1)
        {
            return false;
        }
        Coordinates localInspectedFigure = new Coordinates((byte)(inspectedFigure.X+1), (byte)(inspectedFigure.Y-1));
        while(localInspectedFigure.X < step.GetXAxisLenght() && localInspectedFigure.Y >= 0)
        {
            if(step[localInspectedFigure] is not BlankTile)
            {
                if (step[localInspectedFigure].GetChessPieceType() == ChessPieceType.King
                    && step[localInspectedFigure].GetChessPieceColor() == pieceColor)
                {
                    localInspectedFigure.X = (byte)(inspectedFigure.X - 1);
                    localInspectedFigure.Y = (byte)(inspectedFigure.Y + 1);
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
                    localInspectedFigure.X = (byte)(inspectedFigure.X - 1);
                    localInspectedFigure.Y = (byte)(inspectedFigure.Y + 1);
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
}