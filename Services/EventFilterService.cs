using WebServerExample.Models;

namespace WebServerExample.Services
{
  public static class DataFilterService
  {
    private static List<int> validEventIDs = new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,50,51};
    public static bool FilterData(EventDataModel data)
    {
      if (validEventIDs.Contains(data.event_type.id)) return true;
      else return false;
    }
  }
}
