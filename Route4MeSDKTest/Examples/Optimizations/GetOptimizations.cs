using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetOptimizations()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            RouteParametersQuery queryParameters = new RouteParametersQuery()
            {
                Limit = 10,
                Offset = 5
            };

            // Run the query
            string errorString;
            DataObject[] dataObjects = route4Me.GetOptimizations(queryParameters, out errorString);

            Console.WriteLine("");

            if (dataObjects != null)
            {
                Console.WriteLine("GetOptimizations executed successfully, {0} optimizations returned", dataObjects.Length);
                Console.WriteLine("");

                dataObjects.ForEach(optimization =>
                {
                    Console.WriteLine("Optimization Problem ID: {0}", optimization.OptimizationProblemId);
                    Console.WriteLine("");
                });
            }
            else
            {
                Console.WriteLine("GetOptimizations error: {0}", errorString);
            }
        }
    }
}
