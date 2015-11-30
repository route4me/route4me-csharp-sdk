using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetActivities()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      ActivityParameters activityParameters = new ActivityParameters()
      {
        RouteId = "7C0119495FBB74108F269DFA0E7FDED1",
        Limit = 10,
        Offset = 0
      };

      // Run the query
      string errorString;
      Activity[] activities = route4Me.GetActivityFeed(activityParameters, out errorString);

      Console.WriteLine("");

      if (activities != null)
      {
        Console.WriteLine("GetActivities executed successfully, {0} activities returned", activities.Length);
        Console.WriteLine("");

        activities.ForEach(activity =>
        {
          Console.WriteLine("Activity ID: {0}", activity.ActivityId);
        });
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine("GetActivities error: {0}", errorString);
      }
    }
  }
}
