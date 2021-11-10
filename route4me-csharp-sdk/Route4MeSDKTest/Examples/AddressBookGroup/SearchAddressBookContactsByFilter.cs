using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Search the address book contacts by filter
        /// </summary>
        public void SearchAddressBookContactsByFilter()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var filterParam = new AddressBookGroupFilterParameter()
            {
                Query = "Louisville",
                Display = "all"
            };

            var addressBookGroupParameters = new AddressBookGroupParameters()
            {
                Fields = new string[] { "address_id", "address_1", "address_group" },
                offset = 0,
                limit = 10,
                filter = filterParam
            };

            // Run the query
            var response = route4Me
                .SearchAddressBookContactsByFilter(
                addressBookGroupParameters,
                out string errorString);

            Console.WriteLine(
                (response?.Results?.Length ?? 0) < 1
                ? "Cannot retrieve the contacts by filter" + Environment.NewLine + errorString
                : "Retrieved the contacts by filter: " + response.Results.Length
                );
        }
    }
}
