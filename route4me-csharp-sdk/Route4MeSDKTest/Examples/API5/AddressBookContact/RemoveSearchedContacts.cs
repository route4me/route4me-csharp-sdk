using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void RemoveSearchedContacts()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var addressBookParameters = new AddressBookParameters
            {
                Query = "ToDelete",
                Offset = 0,
                Limit = 500
            };

            // Run the query
            var response = route4Me.GetAddressBookContacts(
                addressBookParameters,
                out ResultResponse resultResponse);

            int[] addressIDs = response.Results
                .Where(x => x.AddressId != null)
                .Select(x => (int)x.AddressId)
                .ToArray();

            Console.WriteLine("Length:", String.Join(",", addressIDs));

            var removed = route4Me.RemoveAddressBookContacts(addressIDs, out resultResponse);

            Console.WriteLine(resultResponse == null
                ? addressIDs.Length + " contacts removed from database"
                : "Cannot remove " + addressIDs.Length + " contacts." + Environment.NewLine +
                "Exit code: " + (resultResponse?.ExitCode.ToString() ?? "") + Environment.NewLine +
                "Code: " + (resultResponse?.Code.ToString() ?? "") + Environment.NewLine +
                "Status: " + (resultResponse?.Status.ToString() ?? "") + Environment.NewLine
                );

            if (resultResponse != null)
            {
                foreach (var msg in resultResponse.Messages)
                {
                    Console.WriteLine(msg.Key + ": " + msg.Value + Environment.NewLine);
                }
            }

            Console.WriteLine("=======================================");
        }
    }
}
