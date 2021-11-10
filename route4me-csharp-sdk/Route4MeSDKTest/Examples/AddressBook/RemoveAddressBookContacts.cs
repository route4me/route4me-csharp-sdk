using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Remove an array of the address book contacts
        /// </summary>
        /// <param name="addressIds"></param>
        public void RemoveAddressBookContacts(string[] addressIds = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestContacts();

            if (addressIds == null)
            {
                addressIds = new string[]
                {
                    contact1.AddressId.ToString(),
                    contact2.AddressId.ToString()
                };
            }

            // Run the query
            bool removed = route4Me.RemoveAddressBookContacts(addressIds, out string errorString);

            Console.WriteLine("");

            Console.WriteLine(removed
                ? String.Format("RemoveAddressBookContacts executed successfully, {0} contacts deleted", addressIds.Length)
                : String.Format("RemoveAddressBookContacts error: {0}", errorString)
                );

            ContactsToRemove.Remove(contact1.AddressId.ToString());
            ContactsToRemove.Remove(contact2.AddressId.ToString());

            RemoveTestContacts();
        }
    }
}
