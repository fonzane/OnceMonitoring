namespace WebServerExample.Models
{
    public class EventDataModel
    {
        public string Key { get; set; } = string.Empty;
        public int? OtherKey { get; set; } // Nullable if the field isn't mandatory
    }
}
