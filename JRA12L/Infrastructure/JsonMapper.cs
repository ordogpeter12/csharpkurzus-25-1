using JRA12L.Core.ChessGame;
using JRA12L.Factories;
using JRA12L.Model;
using JRA12L.Model.Figures;

namespace JRA12L.Infrastructure;

public static class JsonMapper
{
    public static JsonStepDto ToDto(StepSavableInformation step)
    {
        return new JsonStepDto
        {
            TableValue = step.TableValue,
            WhiteShortCastle = step.WhiteShortCastle,
            WhiteLongCastle = step.WhiteLongCastle,
            BlackShortCastle = step.BlackShortCastle,
            BlackLongCastle = step.BlackLongCastle,
            WhoseTurn = step.WhoseTurn,
        };
    }
    public static Step ToDomainObject(JsonStepDto dto)
    {
        ChessPieceColor[] chessPieceColors = dto.TableValue.Select(color => (ChessPieceColor)color)
            .Where((value, index) => index % 2 == 0).ToArray();
        ChessPieceType[] chessPieceTypes = dto.TableValue.Select(color => (ChessPieceType)color)
            .Where((value, index) => index % 2 == 1).ToArray();
        if (chessPieceColors.Length != chessPieceTypes.Length
            || chessPieceColors.Count(color => !Enum.IsDefined(typeof(ChessPieceColor), color)) != 0
            || chessPieceTypes.Count(color => !Enum.IsDefined(typeof(ChessPieceType), color)) != 0)
        {
            throw new ArgumentException("The stored string contains invalid characters.");
        }
        if(!Enum.IsDefined(typeof(ChessPieceColor), dto.WhoseTurn))
        {
            throw new ArgumentException("The turn indicator is invalid.");
        }
        List<Figure> figures = [];
        BaseChessSimpleFigureFactory factory = new BaseChessSimpleFigureFactory();
        for(var i = 0; i < chessPieceColors.Length; i++)
        {
            figures.Add(factory.GetFigure(chessPieceColors[i], chessPieceTypes[i]));
        }
        return new Step(figures.ToArray(), dto.WhiteShortCastle, dto.WhiteLongCastle, dto.BlackShortCastle, dto.BlackLongCastle, dto.WhoseTurn);
    }
}