using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
  public sealed partial class Route4MeExamples
  {
    /// <summary>
    /// This example demonstares how to use the API in a generic way, not bounded to the proposed Route4MeManager shortcucts
    /// For the same functionality using shortcuts check the Route4MeExamples.GenericExampleShortcut()
    /// </summary>
    public void GenericExample()
    {
      const string uri = R4MEInfrastructureSettings.MainHost + "/api.v4/route.php";
      const string myApiKey = "11111111111111111111111111111111";
      
      Route4MeManager route4Me = new Route4MeManager(myApiKey);

      GenericParameters genericParameters = new GenericParameters();
      genericParameters.ParametersCollection.Add("limit", "10");
      genericParameters.ParametersCollection.Add("Offset", "5");

      string errorMessage;
      DataObjectRoute[] dataObjects = route4Me.GetJsonObjectFromAPI<DataObjectRoute[]>(genericParameters,
                                                                                       uri,
                                                                                       HttpMethodType.Get,
                                                                                       out errorMessage);

      if (dataObjects != null)
      {
        Console.WriteLine("GenericExample executed successfully, {0} routes returned", dataObjects.Length);
        Console.WriteLine("");

        dataObjects.ForEach(dataObject =>
        {
          Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
          Console.WriteLine("RouteID: {0}", dataObject.RouteID);
          Console.WriteLine("");
        });
      }
      else
      {
        // TODO error handling
        Console.WriteLine("GenericExample error {0}", errorMessage);
      }
    }

    public void GenericExampleShortcut()
    {
      Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

      RouteParametersQuery routeQueryParameters = new RouteParametersQuery()
      {
        Limit = 10,
        Offset = 5
      };

      string errorMessage;
      DataObjectRoute[] dataObjects = route4Me.GetRoutes(routeQueryParameters, out errorMessage);

      if (dataObjects != null)
      {
        Console.WriteLine("GenericExampleShortcut executed successfully, {0} routes returned", dataObjects.Length);
        Console.WriteLine("");

        dataObjects.ForEach(dataObject =>
        {
          Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
          Console.WriteLine("RouteID: {0}", dataObject.RouteID);
          Console.WriteLine("");
        });
      }
      else
      {
        // TODO error handling
        Console.WriteLine("GenericExampleShortcut error {0}", errorMessage);
      }
    }
  }
}
