using Route4MeSDK.DataTypes;
using Route4MeSDK.Examples;
using System.Collections.Generic;

namespace Route4MeSDKTest
{
  internal sealed class Program
  {
    static void Main(string[] args)
    {
      Route4MeExamples examples = new Route4MeExamples();

      DataObject dataObject = null;

      dataObject = examples.SingleDriverRoute10Stops();
      string routeId_SingleDriverRoute10Stops = (dataObject != null && dataObject.Routes != null && dataObject.Routes.Length > 0) ? dataObject.Routes[0].RouteID : null;

      int[] destinationIds = examples.AddRouteDestinations(routeId_SingleDriverRoute10Stops);
      if (destinationIds != null && destinationIds.Length > 0)
      {
        examples.RemoveRouteDestination(routeId_SingleDriverRoute10Stops, destinationIds[0]);
      }

      dataObject = examples.SingleDriverRoundTrip();
      string routeId_SingleDriverRoundTrip = (dataObject != null && dataObject.Routes != null && dataObject.Routes.Length > 0) ? dataObject.Routes[0].RouteID : null;

      examples.SingleDriverRoundTripGeneric();

      dataObject = examples.MultipleDepotMultipleDriver();
      string routeId_MultipleDepotMultipleDriver = (dataObject != null && dataObject.Routes != null && dataObject.Routes.Length > 0) ? dataObject.Routes[0].RouteID : null;

      dataObject = examples.MultipleDepotMultipleDriverTimeWindow();
      string routeId_MultipleDepotMultipleDriverTimeWindow = (dataObject != null && dataObject.Routes != null && dataObject.Routes.Length > 0) ? dataObject.Routes[0].RouteID : null;

      dataObject = examples.SingleDepotMultipleDriverNoTimeWindow();
      string routeId_SingleDepotMultipleDriverNoTimeWindow = (dataObject != null && dataObject.Routes != null && dataObject.Routes.Length > 0) ? dataObject.Routes[0].RouteID : null;

      dataObject = examples.MultipleDepotMultipleDriverWith24StopsTimeWindow();
      string routeId_MultipleDepotMultipleDriverWith24StopsTimeWindow = (dataObject != null && dataObject.Routes != null && dataObject.Routes.Length > 0) ? dataObject.Routes[0].RouteID : null;

      examples.GetOptimization();
      examples.GetOptimizations();
      examples.ReOptimization();
      examples.GetRoute();
      examples.GetRoutes();
      examples.GetUsers();
      examples.GetActivities();
      examples.GetAddress();
      examples.GetAddressNotes();

      string routeId_DuplicateRoute = examples.DuplicateRoute();

      List<string> routeIdsToDelete = new List<string>();
      if (routeId_SingleDriverRoute10Stops != null)
        routeIdsToDelete.Add(routeId_SingleDriverRoute10Stops);
      if (routeId_SingleDriverRoundTrip != null)
        routeIdsToDelete.Add(routeId_SingleDriverRoundTrip);
      if (routeId_DuplicateRoute != null)
        routeIdsToDelete.Add(routeId_DuplicateRoute);
      if (routeId_MultipleDepotMultipleDriver != null)
        routeIdsToDelete.Add(routeId_MultipleDepotMultipleDriver);
      if (routeId_MultipleDepotMultipleDriverTimeWindow != null)
        routeIdsToDelete.Add(routeId_MultipleDepotMultipleDriverTimeWindow);
      if (routeId_SingleDepotMultipleDriverNoTimeWindow != null)
        routeIdsToDelete.Add(routeId_SingleDepotMultipleDriverNoTimeWindow);
      if (routeId_MultipleDepotMultipleDriverWith24StopsTimeWindow != null)
        routeIdsToDelete.Add(routeId_MultipleDepotMultipleDriverWith24StopsTimeWindow);

      if (routeIdsToDelete.Count > 0)
        examples.DeleteRoutes(routeIdsToDelete.ToArray());
      else
        System.Console.WriteLine("routeIdsToDelete.Count == 0. DeleteRoutes not called.");

      //disabled by default
      //examples.AddAddressNote();
      
      //disabled by default, not necessary for optimization tests
      //not all accounts are capable of storing gps data
      //examples.SetGPSPosition();
      //examples.TrackDeviceLastLocationHistory();

      examples.GenericExample();
      examples.GenericExampleShortcut();

      System.Console.WriteLine("Press any key");
      System.Console.ReadKey();
    }
  }
}
