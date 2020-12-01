using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Validate Session
        /// </summary>
        public void ValidateSession()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var @params = new MemberParameters
            {
                SessionGuid = "ad9001f33ed6875b5f0e75bce52cbc34",
                MemberId = 1,
                Format = "json"
            };
            // Run the query
            MemberResponse result = route4Me.ValidateSession(@params, out string errorString);

            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("ValidateSession executed successfully");
                Console.WriteLine("status: " + result.Status);
                Console.WriteLine("api_key: " + result.ApiKey);
                Console.WriteLine("member_id: " + result.MemberId);
                Console.WriteLine("---------------------------");
            }
            else
            {
                Console.WriteLine("ValidateSession error: {0}", errorString);
            }
        }
    }
}
