using LiteDB;

namespace OtakuLib.Logic.Models
{
    public class Setting
    {
        public Setting(string id, BsonValue value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; set; }
        public BsonValue Value { get; set; }
    }
}
