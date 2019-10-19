using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search for Specified Text, Show Specified Fields
        /// </summary>
        public void GetSpecifiedFieldsSearchText()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager(c_ApiKey);

            AddressBookParameters addressBookParameters = new AddressBookParameters
            {
                Query = "david",
                Fields = "address_id,first_name,address_email,address_group,first_name,cached_lat,schedule",
                Offset = 0,
                Limit = 20
            };

            // Run the query
            //uint total = 0;
            string errorString = "";
            var response = route4Me.SearchAddressBookLocation(addressBookParameters, out errorString);

            Console.WriteLine("");

            if (response != null)
            {
                Console.WriteLine("GetSpecifiedFieldsSearchText executed successfully, {0} contacts returned, total = {1}", response.Results.Count, response.Total);

                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("GetSpecifiedFieldsSearchText error: {0}", errorString);
            }
        }
    }
}
