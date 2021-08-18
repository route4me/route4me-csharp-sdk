using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.FastProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void validateLargeContactsCsvFile()
        {
            var fastValidating = new FastValidateData(ActualApiKey, false)
            {
                CsvChankSize = 500,
                ChankPause = 0,
                ConsoleWriteMessage = true
            };

            var ab = new AddressBookContact();

            var csvAddressMapping = new Dictionary<string, string>()
            {
                {"Alias", R4MeUtils.GetPropertyName(() => ab.address_alias)},
                {"Address", R4MeUtils.GetPropertyName(() => ab.address_1)},
                {"City", R4MeUtils.GetPropertyName(() => ab.address_city)},
                {"State", R4MeUtils.GetPropertyName(() => ab.address_state_id)},
                {"Zip", R4MeUtils.GetPropertyName(() => ab.address_zip)},
                {"Lat", R4MeUtils.GetPropertyName(() => ab.cached_lat)},
                {"Lng", R4MeUtils.GetPropertyName(() => ab.cached_lng)},
                {"Time", R4MeUtils.GetPropertyName(() => ab.service_time)},
                {"Time_window_start", R4MeUtils.GetPropertyName(() => ab.local_time_window_start)},
                {"Time_window_end", R4MeUtils.GetPropertyName(() => ab.local_time_window_end)}
            };

            FastFileReading.csvAddressMapping = csvAddressMapping;

            fastValidating.MandatoryFields = csvAddressMapping.Values.ToArray();

            Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastValidating.readLargeContactsCsvFile(stPath + @"Data\CSV\60k.csv", out string errorString);

            Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}

