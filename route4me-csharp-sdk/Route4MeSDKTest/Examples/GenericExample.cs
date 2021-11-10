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

            //the api key of the account
            //the api key must have hierarchical ownership of the route being viewed (api key can't view routes of others)
            const string myApiKey = "11111111111111111111111111111111";

            var route4Me = new Route4MeManager(myApiKey);

            var genericParameters = new GenericParameters();

            //number of records per page
            genericParameters.ParametersCollection.Add("limit", "10");

            //the page offset starting at zero
            genericParameters.ParametersCollection.Add("Offset", "5");

            DataObjectRoute[] dataObjects = route4Me
                .GetJsonObjectFromAPI<DataObjectRoute[]>(genericParameters,
                                                    uri,
                                                    HttpMethodType.Get,
                                                    out string errorMessage);

            Console.WriteLine("");

            if (dataObjects != null)
            {
                Console.WriteLine("GenericExample executed successfully, {0} routes returned", dataObjects.Length);
                Console.WriteLine("");

                dataObjects.ForEach(dataObject =>
                {
                    Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
                    Console.WriteLine("RouteID: {0}", dataObject.RouteId);
                    Console.WriteLine("");
                });
            }
            else
            {
                Console.WriteLine("GenericExample error {0}", errorMessage);
            }
        }

        public void GenericExampleShortcut()
        {
            Route4MeManager route4Me = new Route4MeManager(ActualApiKey);

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
                    Console.WriteLine("RouteID: {0}", dataObject.RouteId);
                    Console.WriteLine("");
                });
            }
            else
            {
                Console.WriteLine("GenericExampleShortcut error {0}", errorMessage);
            }
        }
    }
}
