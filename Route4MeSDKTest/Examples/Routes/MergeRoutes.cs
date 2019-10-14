using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    public void MergeRoutes(MergeRoutesQuery mergeRoutesParameters)
    {
      // Create the manager with the api key
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      // Run the query
      string errorString;
      bool result = route4Me.MergeRoutes(mergeRoutesParameters, out errorString);

      Console.WriteLine("");

      if (result)
      {
          Console.WriteLine("MergeRoutes executed successfully, {0} routes merged", mergeRoutesParameters.RouteIds);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine("MergeRoutes error {0}", errorString);
      }
    }
  }
}
