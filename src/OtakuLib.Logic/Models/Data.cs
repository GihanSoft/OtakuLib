using LiteDB;

namespace OtakuLib.Logic.Models;

[CLSCompliant(false)]
public record Data(string Id, BsonValue Value)
{
    public Data()
        : this(string.Empty, BsonValue.Null)
    {
    }
}