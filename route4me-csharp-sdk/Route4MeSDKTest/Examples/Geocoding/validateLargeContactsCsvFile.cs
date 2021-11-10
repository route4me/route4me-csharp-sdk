using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.FastProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void validateLargeContactsCsvFile()
        {
            var fastValidating = new FastValidateData(ActualApiKey, false)
            {
                CsvChunkSize = 500,
                ChunkPause = 0,
                ConsoleWriteMessage = true
            };

            var ab = new AddressBookContact();

            var csvAddressMapping = new Dictionary<string, string>()
            {
                {"Alias", R4MeUtils.GetPropertyName(() => ab.AddressAlias)},
                {"Address", R4MeUtils.GetPropertyName(() => ab.Address1)},
                {"City", R4MeUtils.GetPropertyName(() => ab.AddressCity)},
                {"State", R4MeUtils.GetPropertyName(() => ab.AddressStateId)},
                {"Zip", R4MeUtils.GetPropertyName(() => ab.AddressZip)},
                {"Lat", R4MeUtils.GetPropertyName(() => ab.CachedLat)},
                {"Lng", R4MeUtils.GetPropertyName(() => ab.CachedLng)},
                {"Time", R4MeUtils.GetPropertyName(() => ab.ServiceTime)},
                {"Time_window_start", R4MeUtils.GetPropertyName(() => ab.LocalTimeWindowStart)},
                {"Time_window_end", R4MeUtils.GetPropertyName(() => ab.LocalTimeWindowEnd)},
                {"Custom_Data", R4MeUtils.GetPropertyName(() => ab.AddressCustomData)}
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
