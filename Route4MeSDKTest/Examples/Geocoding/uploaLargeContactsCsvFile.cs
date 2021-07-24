using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes;
using Route4MeSDK.FastProcessing;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {

        public void uploaLargeContactsCsvFile()
        {
            var fastProcessing = new FastBulkGeocoding(ActualApiKey, false)
            {
                ChankPause = 1000,
                CsvChankSize = 500
            };
            //var lsGeocodedAddressTotal = new List<AddressGeocoded>();
            var lsAddresses = new List<string>();

            int addressesInFile = 13;

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

            Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastProcessing.uploadLargeContactsCsvFile(stPath + @"Data\CSV\30k_geocoded.csv", out string errorString);

            Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        }

    }
}
