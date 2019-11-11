using Route4MeSDK.DataTypes;
using System;

namespace Route4MeSDK.Examples
{
    /// <summary>
    /// Helper functions used by some of the examples.
    /// </summary>
    public sealed partial class Route4MeExamples
    {

        //your api key
        public const string c_ApiKey = "11111111111111111111111111111111";

        private void PrintExampleOptimizationResult(string exampleName, DataObjectRoute dataObjectRoute, string errorString)
        {
            Console.WriteLine("");

            if (dataObjectRoute != null)
            {
                Console.WriteLine("{0} executed successfully", exampleName);
                Console.WriteLine("");

                Console.WriteLine("Optimization Problem ID: {0}", dataObjectRoute.OptimizationProblemId);

                Console.WriteLine("");

                dataObjectRoute.Addresses.ForEach(address =>
                {
                    Console.WriteLine("Address: {0}", address.AddressString);
                    Console.WriteLine("Route ID: {0}", address.RouteId);
                });
            }
            else
            {
                Console.WriteLine("{0} error {1}", exampleName, errorString);
            }
        }

        private void PrintExampleOptimizationResult(string exampleName, DataObject dataObject, string errorString)
        {
            Console.WriteLine("");

            if (dataObject != null)
            {
                Console.WriteLine("{0} executed successfully", exampleName);
                Console.WriteLine("");

                Console.WriteLine("Optimization Problem ID: {0}", dataObject.OptimizationProblemId);
                Console.WriteLine("State: {0}", dataObject.State);

                dataObject.UserErrors.ForEach(error => Console.WriteLine("UserError : '{0}'", error));

                Console.WriteLine("");

                dataObject.Addresses.ForEach(address =>
                {
                    Console.WriteLine("Address: {0}", address.AddressString);
                    Console.WriteLine("Route ID: {0}", address.RouteId);
                });
            }
            else
            {
                Console.WriteLine("{0} error {1}", exampleName, errorString);
            }
        }
    }
}
