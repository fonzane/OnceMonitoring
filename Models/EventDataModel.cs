namespace OnceMonitoring.Models
{
    public class EventDataModel
    {
        public EventSource? event_source { get; set; }
        public Organisation? organisation { get; set; }
        public EventSeverity? event_severity { get; set; }
        public SIM? sim { get; set; }
        public string? description { get; set; }
        public bool alert { get; set; }
        public long id { get; set; }
        public Endpoint? endpoint { get; set; }
        public EventType? event_type { get; set; }
        public DateTime timestamp { get; set; }
        public Detail? detail { get; set; }
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
        public int id { get; set; }
        public string? imsi { get; set; }
        public DateTime import_date { get; set; }
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

    public class Detail
    {
        public int id { get; set; }
        public string? name { get; set; }
        public Country? country { get; set; }
        public PdpContext? pdp_context { get; set; }
        public Volume? volume { get; set; }

    }

    public class Country
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? country_code { get; set; }
        public string? mcc { get; set; }
        public string? iso_code { get; set; }
    }

    public class PdpContext
    {
        public int pdp_context_id { get; set; }
        public DateTime tunnel_created { get; set; }
        public string? gpt_version { get; set; }
        public string? ggsn_control_plane_ip_address { get; set; }
        public string? ggsn_data_plane_ip_address { get; set; }
        public string? sgsn_control_plane_ip_address { get; set; }
        public string? sgsn_data_plane_ip_address { get; set; }
        public string? region { get; set; }
        public string? breakout_ip { get; set; }
        public string? apn { get; set; }
        public int? nsapi { get; set; }
        public string? ue_ip_address { get; set; }
        public string? imeisv { get; set; }
        public string? mcc { get; set; }
        public string? mnc { get; set; }
        public int? lac { get; set; }
        public int? sac { get; set; }
        public int? rac { get; set; }
        public int? ci { get; set; }
        public int? rat_type { get; set; }
    }

    public class Volume
    {
        public decimal rx { get; set; }
        public decimal tx { get; set; }
        public decimal total { get; set; }
    }
}
