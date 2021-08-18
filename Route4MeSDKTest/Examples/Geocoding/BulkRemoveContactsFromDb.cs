using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using System.Linq;

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

            int[] addressIDs = response.results
                                        .Where(x => x.address_id != null)
                                        .Select(x => (int)x.address_id)
                                        .ToArray();

            var fastBulkRemoveContacts = new FastProcessing.FastBulkRemoveContacts(ActualApiKey)
            {
                ChunkPause = 0,
                PrintMessagesOnConsole = true,
                RemoveContactsChunkSize = 500
            };

            fastBulkRemoveContacts.RemoveAddressBookContacts(addressIDs, out string errorString);
        }
    }
}
