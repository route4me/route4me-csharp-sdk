using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void AddEditCustomDataToUser()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateTestUser();

            int memberId = Convert.ToInt32(usersToRemove[usersToRemove.Count - 1]);

            var @customParams = new  MemberParametersV4
            {
                member_id = memberId,
                custom_data = new Dictionary<string, string>() { { "Custom Key 2", "Custom Value 2" } }
            };

            var result2 = route4Me.UserUpdate(@customParams, out string errorString);

            PrintTestUsers(result2, errorString);

            if (result2 != null && result2.GetType() == typeof(MemberResponseV4))
            {
                var customData = result2.CustomData;

                if (customData.Keys.ElementAt(0) != "Custom Key 2") Console.WriteLine("Custom Key is not 'Custom Key 2'");

                if (customData["Custom Key 2"] != "Custom Value 2") Console.WriteLine("Custom Value is not 'Custom Value 2'");
            }

            RemoveTestUsers();
        }
    }
}
