using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.FastProcessing;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Upload and geocode large JSON file
        /// </summary>
        public void uploadAndGeocodeLargeJsonFile()
        {
            var fastProcessing = new FastBulkGeocoding(ActualApiKey, false);
            var lsGeocodedAddressTotal = new List<AddressGeocoded>();
            var lsAddresses = new List<string>();

            int addressesInFile = 13;

            fastProcessing.GeocodingIsFinished += (object sender, FastBulkGeocoding.GeocodingIsFinishedArgs e) =>
            {
                if (lsAddresses == null)
                {
                    Console.WriteLine("Geocoding process failed");
                    return;
                }

                if (addressesInFile != lsAddresses.Count)
                {
                    Console.WriteLine("Not all the addresses were geocoded");
                    return;
                }

                Console.WriteLine("Large addresses file geocoding is finished");
            };

            fastProcessing.AddressesChunkGeocoded += (object sender, FastBulkGeocoding.AddressesChunkGeocodedArgs e) =>
            {
                if (e.lsAddressesChunkGeocoded != null)
                {
                    foreach (var addr1 in e.lsAddressesChunkGeocoded)
                    {
                        var geocoding = addr1.GeocodedAddress.Geocodings.Length > 0
                        ? addr1.GeocodedAddress.Geocodings[0]
                        : null;

                        lsAddresses.Add(addr1.GeocodedAddress.AddressString + ":" +
                            (geocoding != null
                            ? geocoding.Latitude + ", " + geocoding.Longtude
                            : ""));
                    }
                }

                foreach (string geocodedAddress in lsAddresses)
                {
                    Console.WriteLine(geocodedAddress);
                }

                Console.WriteLine("Total Geocoded Addresses -> " + lsAddresses.Count);
            };

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastProcessing.uploadAndGeocodeLargeJsonFile(stPath + @"\Data\JSON\batch_socket_upload_error_addresses_data_5.json");
        }
    }
}
