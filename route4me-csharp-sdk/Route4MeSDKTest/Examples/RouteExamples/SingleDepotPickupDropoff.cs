using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of creating an optimization 
        /// with pickup/dropof and joint addresses. 
        /// </summary>
        public void SingleDepotPickupDropoffJoint()
        {
            // Note: use an API key with permission for pickup/dropoff operations.
            var route4Me = new Route4MeManager(ActualApiKey);

            string jsonFile = AppDomain.CurrentDomain.BaseDirectory + @"\Data\JSON\pickupdropoff_request.json";

            StreamReader r = new StreamReader(jsonFile);
            string jsonString = r.ReadToEnd();

            var routeParamsFromJson = R4MeUtils.ReadObjectNew<DataObjectRoute>(jsonString);

            var optParams = new OptimizationParameters()
            {
                Parameters = routeParamsFromJson.Parameters,
                Addresses = routeParamsFromJson.Addresses,
                Depots = routeParamsFromJson.Addresses.Where(x => x.IsDepot == true)?.ToArray() ?? null
            };

            var dataObject = route4Me.RunOptimization(optParams, out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            if (dataObject == null && dataObject.GetType() != typeof(DataObject))
            {
                Console.WriteLine(
                    "SingleDepotPickupDropoff failed" +
                    Environment.NewLine +
                    "Cannot create the optimization. " +
                    Environment.NewLine +
                    errorString
                );

                return;
            }

            if ((dataObject?.Routes?.Length ?? 0) < 1)
            {
                Console.WriteLine("The optimization doesn't contain route");
                RemoveTestOptimizations();
                return;
            }

            var routeId = dataObject.Routes[0].RouteId;

            if ((routeId?.Length ?? 0) < 32)
            {
                Console.WriteLine("The route ID is not valid");
                RemoveTestOptimizations();
                return;
            }

            var routeQueryParameters = new RouteParametersQuery()
            {
                RouteId = routeId
            };

            var routePickDrop = route4Me.GetRoute(routeQueryParameters, out errorString);

            PrintExampleRouteResult(routePickDrop, errorString);

            RemoveTestOptimizations();
        }
    }
}
