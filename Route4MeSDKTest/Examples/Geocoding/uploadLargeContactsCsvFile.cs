using System;
using System.Collections.Generic;
using System.Linq;
using Route4MeSDK.DataTypes;
using Route4MeSDK.FastProcessing;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void uploadLargeContactsCsvFile()
        {
            var fastProcessing = new FastBulkGeocoding(ActualApiKey, false)
            {
                ChankPause = 0,
                CsvChankSize = 500,
                DoGeocoding = true,
                GeocodeOnlyEmpty = true
            };

            //var lsGeocodedAddressTotal = new List<AddressGeocoded>();
            var lsAddresses = new List<string>();

            var ab = new AddressBookContact();

            var csvAddressMapping = new Dictionary<string, string>()
            {
                {"Alias", R4MeUtils.GetPropertyName(() => ab.address_alias)},
                {"Address", R4MeUtils.GetPropertyName(() => ab.address_1)},
                {"City", R4MeUtils.GetPropertyName(() => ab.address_city)},
                {"State", R4MeUtils.GetPropertyName(() => ab.address_state_id)},
                {"Group", R4MeUtils.GetPropertyName(() => ab.address_group)},
                {"Zip", R4MeUtils.GetPropertyName(() => ab.address_zip)},
                {"Lat", R4MeUtils.GetPropertyName(() => ab.cached_lat)},
                {"Lng", R4MeUtils.GetPropertyName(() => ab.cached_lng)},
                {"Time", R4MeUtils.GetPropertyName(() => ab.service_time)},
                {"Time_window_start", R4MeUtils.GetPropertyName(() => ab.local_time_window_start)},
                {"Time_window_end", R4MeUtils.GetPropertyName(() => ab.local_time_window_end)}
            };

            FastFileReading.csvAddressMapping = csvAddressMapping;

            fastProcessing.MandatoryFields = csvAddressMapping.Values.ToArray();

            Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastProcessing.uploadLargeContactsCsvFile(stPath + @"Data\CSV\60k_prob.csv", out string errorString);

            Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

    }
}
