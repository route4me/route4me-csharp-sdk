using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search Routed Locations
        /// </summary>
        public void SearchRoutedLocations()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            AddressBookParameters addressBookParameters = new AddressBookParameters
            {
                Display = "routed",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            uint total = 0;
            string errorString = "";
            AddressBookContact[] contacts = route4Me.GetAddressBookLocation(addressBookParameters, out total, out errorString);

            Console.WriteLine("");

            if (contacts != null)
            {
                Console.WriteLine("SearchRoutedLocations executed successfully, {0} contacts returned, total = {1}", contacts.Length, total);

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("SearchRoutedLocations error: {0}", errorString);
            }
        }
    }
}