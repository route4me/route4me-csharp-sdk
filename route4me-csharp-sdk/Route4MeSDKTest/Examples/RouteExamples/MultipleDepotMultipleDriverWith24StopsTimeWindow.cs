using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating an optimization 
        /// with 24 stops and options: multi-depot, multi-driver, time windows options.
        /// </summary>
        public void MultipleDepotMultipleDriverWith24StopsTimeWindow()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
        #region Addresses

        new Address() { AddressString   = "3634 W Market St, Fairlawn, OH 44333",
                        IsDepot         = true,
                        Latitude        = 41.135762259364,
                        Longitude       = -81.629313826561,
                        Time            = 300,
                        TimeWindowStart = 28800,
                        TimeWindowEnd   = 29465 },

        new Address() { AddressString   = "1218 Ruth Ave, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.143505096435,
                        Longitude       = -81.46549987793,
                        Time            = 300,
                        TimeWindowStart = 29465,
                        TimeWindowEnd   = 30529 },

        new Address() { AddressString   = "512 Florida Pl, Barberton, OH 44203",
                        IsDepot         = true,
                        Latitude        = 41.003671512008,
                        Longitude       = -81.598461046815,
                        Time            = 300,
                        TimeWindowStart = 33479,
                        TimeWindowEnd   = 33944 },

        new Address() { AddressString   = "3495 Purdue St, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.162971496582,
                        Longitude       = -81.479049682617,
                        Time            = 300,
                        TimeWindowStart = 33944,
                        TimeWindowEnd   = 34801 },

        new Address() { AddressString   = "1659 Hibbard Dr, Stow, OH 44224",
                        Latitude        = 41.194505989552,
                        Longitude       = -81.443351581693,
                        Time            = 300,
                        TimeWindowStart = 34801,
                        TimeWindowEnd   = 36366 },

        new Address() { AddressString   = "2705 N River Rd, Stow, OH 44224",
                        Latitude        = 41.145240783691,
                        Longitude       = -81.410247802734,
                        Time            = 300,
                        TimeWindowStart = 36366,
                        TimeWindowEnd   = 39173 },

        new Address() { AddressString   = "10159 Bissell Dr, Twinsburg, OH 44087",
                        Latitude        = 41.340042114258,
                        Longitude       = -81.421226501465,
                        Time            = 300,
                        TimeWindowStart = 39173,
                        TimeWindowEnd   = 41617 },

        new Address() { AddressString   = "367 Cathy Dr, Munroe Falls, OH 44262",
                        Latitude        = 41.148578643799,
                        Longitude       = -81.429229736328,
                        Time            = 300,
                        TimeWindowStart = 41617,
                        TimeWindowEnd   = 43660 },

        new Address() { AddressString   = "367 Cathy Dr, Munroe Falls, OH 44262",
                        Latitude        = 41.148579,
                        Longitude       = -81.42923,
                        Time            = 300,
                        TimeWindowStart = 43660,
                        TimeWindowEnd   = 46392 },

        new Address() { AddressString   = "512 Florida Pl, Barberton, OH 44203",
                        Latitude        = 41.003671512008,
                        Longitude       = -81.598461046815,
                        Time            = 300,
                        TimeWindowStart = 46392,
                        TimeWindowEnd   = 48089 },

        new Address() { AddressString   = "559 W Aurora Rd, Northfield, OH 44067",
                        Latitude        = 41.315116882324,
                        Longitude       = -81.558746337891,
                        Time            = 300,
                        TimeWindowStart = 48089,
                        TimeWindowEnd   = 48449 },

        new Address() { AddressString   = "3933 Klein Ave, Stow, OH 44224",
                        Latitude        = 41.169467926025,
                        Longitude       = -81.429420471191,
                        Time            = 300,
                        TimeWindowStart = 48449,
                        TimeWindowEnd   = 50152 },

        new Address() { AddressString   = "2148 8th St, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.136692047119,
                        Longitude       = -81.493492126465,
                        Time            = 300,
                        TimeWindowStart = 50152,
                        TimeWindowEnd   = 51682 },

        new Address() { AddressString   = "3731 Osage St, Stow, OH 44224",
                        Latitude        = 41.161357879639,
                        Longitude       = -81.42293548584,
                        Time            = 300,
                        TimeWindowStart = 51682,
                        TimeWindowEnd   = 54379 },

        new Address() { AddressString   = "3862 Klein Ave, Stow, OH 44224",
                        Latitude        = 41.167895123363,
                        Longitude       = -81.429973393679,
                        Time            = 300,
                        TimeWindowStart = 54379,
                        TimeWindowEnd   = 54879 },

        new Address() { AddressString   = "138 Northwood Ln, Tallmadge, OH 44278",
                        Latitude        = 41.085464134812,
                        Longitude       = -81.447411775589,
                        Time            = 300,
                        TimeWindowStart = 54879,
                        TimeWindowEnd   = 56613 },

        new Address() { AddressString   = "3401 Saratoga Blvd, Stow, OH 44224",
                        Latitude        = 41.148849487305,
                        Longitude       = -81.407363891602,
                        Time            = 300,
                        TimeWindowStart = 56613,
                        TimeWindowEnd   = 57052 },

        new Address() { AddressString   = "5169 Brockton Dr, Stow, OH 44224",
                        Latitude        = 41.195003509521,
                        Longitude       = -81.392700195312,
                        Time            = 300,
                        TimeWindowStart = 57052,
                        TimeWindowEnd   = 59004 },

        new Address() { AddressString   = "5169 Brockton Dr, Stow, OH 44224",
                        Latitude        = 41.195003509521,
                        Longitude       = -81.392700195312,
                        Time            = 300,
                        TimeWindowStart = 59004,
                        TimeWindowEnd   = 60027 },

        new Address() { AddressString   = "458 Aintree Dr, Munroe Falls, OH 44262",
                        Latitude        = 41.1266746521,
                        Longitude       = -81.445808410645,
                        Time            = 300,
                        TimeWindowStart = 60027,
                        TimeWindowEnd   = 60375 },

        new Address() { AddressString   = "512 Florida Pl, Barberton, OH 44203",
                        Latitude        = 41.003671512008,
                        Longitude       = -81.598461046815,
                        Time            = 300,
                        TimeWindowStart = 60375,
                        TimeWindowEnd   = 63891 },

        new Address() { AddressString   = "2299 Tyre Dr, Hudson, OH 44236",
                        Latitude        = 41.250511169434,
                        Longitude       = -81.420433044434,
                        Time            = 300,
                        TimeWindowStart = 63891,
                        TimeWindowEnd   = 65277 },

        new Address() { AddressString   = "2148 8th St, Cuyahoga Falls, OH 44221",
                        Latitude        = 41.136692047119,
                        Longitude       = -81.493492126465,
                        Time            = 300,
                        TimeWindowStart = 65277,
                        TimeWindowEnd   = 68545 }

        #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Multiple Depot, Multiple Driver with 24 Stops, Time Window",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RouteMaxDuration = 86400,
                VehicleCapacity = 5,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Matrix
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            DataObject dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
