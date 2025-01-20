using WebServerExample.Models;

namespace WebServerExample.Services
{
  public static class DataFilterService
  {
    public static bool FilterData(EventDataModel data)
    {
      // Example: Filter for "Key" having a specific value
      return !string.IsNullOrEmpty(data.Key) && data.Key == "specificValue";
    }
  }
}
