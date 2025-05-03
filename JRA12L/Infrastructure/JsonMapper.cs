using JRA12L.Model;

namespace JRA12L.Infrastructure;

public class JsonMapper
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
}