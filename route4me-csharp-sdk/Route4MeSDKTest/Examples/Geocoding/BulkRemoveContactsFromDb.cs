using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void BulkRemoveContactsFromDb()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var addressBookParameters = new AddressBookParameters()
            {
                Query = "ToDelete"
            };

            var response = route4Me.GetAddressBookContacts(
                                            addressBookParameters,
                                            out ResultResponse resultResponse);

            int[] addressIDs = response.Results
                                        .Where(x => x.AddressId != null)
                                        .Select(x => (int)x.AddressId)
                                        .ToArray();

            var fastBulkRemoveContacts = new FastProcessing.FastBulkRemoveContacts(ActualApiKey)
            {
                ChunkPause = 0,
                PrintMessagesOnConsole = true,
                RemoveContactsChunkSize = 500
            };

            fastBulkRemoveContacts.RemoveAddressBookContacts(addressIDs, out string errorString);

            //fastBulkRemoveContacts.
        }
    }
}
