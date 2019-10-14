using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void GetUsers()
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      GenericParameters parameters = new GenericParameters()
      {
      };

      // Run the query
      string errorString;
      Route4MeManager.GetUsersResponse dataObjects = route4Me.GetUsers(parameters, out errorString);

      Console.WriteLine("");

      if (dataObjects != null)
      {
        if (dataObjects.results != null)
        {
            Console.WriteLine("GetUsers executed successfully, {0} users returned", dataObjects.results.Length);
            Console.WriteLine("");
        } else Console.WriteLine("GetUsers error: {0}", errorString);

            }
      else
      {
        Console.WriteLine("GetUsers error: {0}", errorString);
      }
    }
  }
}
