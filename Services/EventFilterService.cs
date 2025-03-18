using OnceMonitoring.Models;

namespace WebServerExample.Services
{
  public static class DataFilterService
  {
    private static List<long> validEventIDs = new List<long>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,50,51};
    public static bool FilterData(EventDataModel data)
    {
      if (validEventIDs.Contains(data.event_type.id))
      {
        Console.WriteLine(
          data.timestamp.ToLocalTime() +
          " Description: " + data.description +
          " - Event Type: " + (data.event_type?.description ?? "keine Event Type") +
          " - Source: " + (data.event_source?.description ?? "keine Source") +
          " - IMEI: " + (data.endpoint?.imei ?? "keine IMEI")
        );
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
