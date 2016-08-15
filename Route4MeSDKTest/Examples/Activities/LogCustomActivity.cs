using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// Create User Activity
    /// </summary>
    /// <param name="message"> Activity message </param>
    /// <param name="routeId"> Route identifier </param>
    /// <returns> True/False </returns>
    public bool LogCustomActivity(string message, string routeId)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      Activity activity = new Activity()
      {
        ActivityType = "user_message",
        ActivityMessage = message,
        RouteId = routeId
      };

      // Run the query
      string errorString;
      bool added = route4Me.LogCustomActivity(activity, out errorString);

      Console.WriteLine("");

      if (added)
      {
        Console.WriteLine("LogCustomActivity executed successfully");
        return added;
      }
      else
      {
        Console.WriteLine("LogCustomActivity error: {0}", errorString);
        return added;
      }
    }
  }
}

