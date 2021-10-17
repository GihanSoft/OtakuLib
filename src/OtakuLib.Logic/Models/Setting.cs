namespace OtakuLib.Logic.Models
{
    public class Setting
    {
        public Setting(string id, object value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; set; }
        public object Value { get; set; }
    }
}
