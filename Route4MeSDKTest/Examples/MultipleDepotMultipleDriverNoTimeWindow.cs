using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void MultipleDepotMultipleDriverNoTimeWindow()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
      {
        #region Addresses

        new Address() { AddressString   = "Av. Mal. Floriano, 168 - Centro, Rio de Janeiro - RJ",
                        IsDepot         = true,
                        Latitude        = -22.9027818,
                        Longitude       = -43.1870476,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "Av. Ces\u00e1rio de Melo, 3489 - Campo Grande, Rio de Janeiro - RJ",
                        Latitude        = -22.9077464,
                        Longitude       = -43.565462,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "Av. Maestro Paulo e Silva, 400 - Ilha do Governador Rio de Janeiro - RJ 21920-445",
                        Latitude        = -22.8019278,
                        Longitude       = -43.2018285,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },
        new Address() { 
                        AddressString   = "R. Carolina Reidner, 50 - Catumbi, Rio de Janeiro - RJ, 20211-280",
                        Latitude        = -22.9144974,
                        Longitude       = -43.1971116,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "R. Primeiro de Mar\u00e7o, 11 - Centro Rio de Janeiro - RJ 20010-000",
                        Latitude        = -22.9029114,
                        Longitude       = -43.1754409,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "R. Bar\u00e3o de Ipanema - Copacabana Rio de Janeiro - RJ 22050-032",
                        Latitude        = -22.9747092,
                        Longitude       = -43.1901323,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "R. Zacarias da Silva - Barra da Tijuca, Rio de Janeiro - RJ, 22793-190",
                        Latitude        = -22.9963096,
                        Longitude       = -43.3825409,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "Estr. do Tindiba, 110 - Pechincha, Rio de Janeiro - RJ, 22740-360",
                        Latitude        = -22.9281354,
                        Longitude       = -43.3567916,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "R. Doze de Fevereiro, 571 - Bangu, Rio de Janeiro - RJ",
                        Latitude        = -22.8811549,
                        Longitude       = -43.4622415,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "Av. Br\u00e1s de Pina, 148 - Penha, Rio de Janeiro - RJ - Brazil",
                        Latitude        = -22.8411315,
                        Longitude       = -43.2798001,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "R. Luiz Guimaraes, 546 - Centro Nova Igua\u00e7u - RJ 26210-022",
                        Latitude        = -22.7572727,
                        Longitude       = -43.4454884,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "Estr. do Tindiba, 1608 - Pechincha Rio de Janeiro - RJ 22740-362",
                        Latitude        = -22.9259932,
                        Longitude       = -43.3687776,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null },

        new Address() { AddressString   = "Rodovia Washington Luiz 3968 Duque de Caxias - RJ 25065-007",
                        Latitude        = -22.7751069,
                        Longitude       = -43.2859847,
                        Time            = 0,
                        TimeWindowStart = null,
                        TimeWindowEnd   = null }

        #endregion
      };

            // Set parameters
            RouteParameters parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Multiple Depot, Multiple Driver, No Time Window",
                StoreRoute = false,

                RouteTime = 0,
                RT = true,
                RouteMaxDuration = 86400,
                VehicleCapacity = "99",
                VehicleMaxDistanceMI = "99999",

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Geodesic
            };

            OptimizatonParameters optimizatonParameters = new OptimizatonParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            string errorString;
            DataObject dataObject = route4Me.RunOptimization(optimizatonParameters, out errorString);

            // Output the result
            PrintExampleOptimizationResult("MultipleDepotMultipleDriverNoTimeWindow", dataObject, errorString);
        }
    }
}
