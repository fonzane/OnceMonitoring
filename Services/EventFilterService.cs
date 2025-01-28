using OnceMonitoring.Models;

namespace WebServerExample.Services
{
  public static class DataFilterService
  {
    private static List<int> validEventIDs = new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,50,51};
    public static bool FilterData(EventDataModel data)
    {
      if (validEventIDs.Contains(data.event_type.id))
      {
        Console.WriteLine("Received valid data. " + data.timestamp.ToLocalTime() + " Description: " + data.description + " - Event Type: " + data.event_type.description + " - Source: " + data.event_source.description + " - IMEI: " + data.endpoint?.imei);
        return true;
      }
      else
      {
        Console.WriteLine("Received Data not matching event type: " + data.event_type.description);
        return false;
      }
    }
  }
}
