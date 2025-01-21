namespace WebServerExample.Models
{
    public class EventDataModel
    {
        public EventSource event_source { get; set; }
        public Organisation organisation {get;set;}
        public EventSeverity event_severity {get; set;}
        public SIM? sim { get; set; }
        public string? description { get; set; }
        public bool alert { get; set; }
        public long id { get; set; }
        public Endpoint? endpoint { get; set; }
        public EventType event_type { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class EventType
    {
        public int id { get; set; }
        public string? description { get; set; }
    }

    public class EventSeverity
    {
        public int id { get; set; }
        public string? description { get; set; }
    }

    public class EventSource
    {
        public int id { get; set; }
        public string? description { get; set; }
    }

    public class Organisation
    {
        public int id { get; set; }
        public string? name { get; set; }
    }

    public class IMSI
    {

    }

    public class SIM
    {
        public string? msisdn { get; set; }
        public string? iccid { get; set; }
        public int id { get; set; }
        public DateTime production_date { get; set; }
    }

    public class Endpoint
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? ip_address { get; set; }
        public string? tags { get; set; }
        public string? imei { get; set; }
    }
}
