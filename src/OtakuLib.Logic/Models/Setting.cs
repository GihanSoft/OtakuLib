namespace OtakuLib.Logic.Models
{
    public class Setting
    {
        public Setting(string id, object value)
        {
            Id = id;
            Value = value;
        }

        public Setting(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public object? Value { get; set; }
    }
}
