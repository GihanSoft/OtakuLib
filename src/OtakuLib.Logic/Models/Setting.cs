using LiteDB;

namespace OtakuLib.Logic.Models;

[CLSCompliant(false)]
public record Setting(string Id, BsonValue Value)
{
    public Setting()
        : this(string.Empty, BsonValue.Null)
    {
    }
}