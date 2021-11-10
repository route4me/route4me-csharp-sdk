using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;
using System.IO;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The Example refers to the process of creating an optimization 
        /// with advanced constraints. All the generating routes are started from one depot and finish in another depot. 
        /// </summary>
        public void StartDepotEndDepotConstraint()
        {
			// The example requires API key with access to the advanced constraint feature.
            var route4Me = new Route4MeManagerV5("11111111111111111111111111111111");

            string jsonFile = AppDomain.CurrentDomain.BaseDirectory + @"\Data\JSON\start_depot_end_depot_data.json";

            StreamReader r = new StreamReader(jsonFile);
            string jsonString = r.ReadToEnd();

            var routeParamsFromJson = R4MeUtils.ReadObjectNew<DataObjectRoute>(jsonString);

            var optParams = new OptimizationParameters()
            {
                Parameters = routeParamsFromJson.Parameters,
                Addresses = routeParamsFromJson.Addresses
            };

            var dataObject = route4Me.RunOptimization(optParams, out ResultResponse resultResponse);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            if ((dataObject?.Routes?.Length ?? 0)<2)
            {
                PrintExampleOptimizationResult(null, "Created less than 2 routes. Modify route parameters or addresses");
                return;
            }

            Console.WriteLine(
                    $"1st route start address: {dataObject.Routes[0]?.Addresses[0]?.AddressString ?? "Error"}"
                );
            Console.WriteLine(
                    $"1st route end address: {dataObject.Routes[0]?.Addresses[dataObject.Routes[0].Addresses.Length-1]?.AddressString ?? "Error"}"
                );

            Console.WriteLine(
                    $"2nd route start address: {dataObject.Routes[1]?.Addresses[0]?.AddressString ?? "Error"}"
                );
            Console.WriteLine(
                    $"2nd route end address: {dataObject.Routes[1]?.Addresses[dataObject.Routes[1].Addresses.Length - 1]?.AddressString ?? "Error"}"
                );

            //PrintExampleOptimizationResult(
            //    dataObject,
            //    (resultResponse?.Messages?.Count ?? 0) > 0 ? String.Join(Environment.NewLine, resultResponse.Messages) : ""
            //    );

            RemoveTestOptimizations();
        }
    }
}
