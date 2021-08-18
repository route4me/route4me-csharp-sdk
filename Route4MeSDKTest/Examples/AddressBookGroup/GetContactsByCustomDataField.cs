using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetContactsByCustomDataField()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            string[] customFieldValues = new string[]
            {
                "8694E0B2596E79E59AD93421F00689A6",
                "7DD2ECE2957DFF2269D32F008FF1085A"
            };

            var contactIDs = route4Me.GetAddressBookContactsByCustomField("uid", customFieldValues, out string errorString);

            Console.WriteLine((contactIDs?.Length ?? 0) < 1
                ? "Cannot retrieve contact IDs by custom field " + "uid" + Environment.NewLine + errorString
                : "Retrieved the contact IDs by custom field " + "uid" + ": " + contactIDs.Length
                );
        }
    }
}

