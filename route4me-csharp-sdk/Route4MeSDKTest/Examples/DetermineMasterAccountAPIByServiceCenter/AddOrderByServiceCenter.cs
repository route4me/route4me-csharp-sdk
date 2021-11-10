using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Route4MeSDK.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of creating an order by specified service center.
        /// </summary>
        public Order AddOrderByServiceCenter(string serviceCenter)
        {
            var serviceMapping = (ReadSetting("account_to_api_key_map") as Dictionary<string, object>)
                                            .ToDictionary(k => k.Key, k => k.Value.ToString());

            if (!serviceMapping.Keys.Contains(serviceCenter))
            {
                Console.WriteLine($"The service mapping table does not contain {serviceCenter}");
                return null;
            }

            var apiKey = serviceMapping[serviceCenter];

            if (!ValidateApiKey(apiKey))
            {
                Console.WriteLine($"The API key {apiKey} is wrong");
                return null;
            }

            var route4Me = new Route4MeManager(apiKey);

            var orderParams = new Order()
            {
                Address1 = "318 S 39th St, Louisville, KY 40212, USA",
                CachedLat = 38.259326,
                CachedLng = -85.814979,
                CurbsideLat = 38.259326,
                CurbsideLng = -85.814979,
                AddressAlias = "318 S 39th St 40212",
                AddressCity = "Louisville",
                ExtFieldFirstName = "Lui",
                ExtFieldLastName = "Carol",
                ExtFieldEmail = "lcarol654@yahoo.com",
                ExtFieldPhone = "897946541",
                ExtFieldCustomData = new Dictionary<string, string>() { { "ServiceCenter", serviceCenter } },
                DayScheduledFor_YYYYMMDD = "2017-12-20",
                LocalTimeWindowEnd = 39000,
                LocalTimeWindowEnd2 = 46200,
                LocalTimeWindowStart = 37800,
                LocalTimeWindowStart2 = 45000,
                LocalTimezoneString = "America/New_York",
                OrderIcon = "emoji/emoji-bank"
            };

            var newOrder = route4Me.AddOrder(orderParams, out string errorString);

            PrintExampleOrder(newOrder, errorString);

            return newOrder;
        }

        bool ValidateApiKey(string apiKey)
        {
            string pattern = @"[a-fA-F0-9]{32}";
            Regex regex = new Regex(pattern);

            return regex.Match(apiKey).Success;
        }

        static object ReadSetting(string key_path)
        {
            var startPath = AppDomain.CurrentDomain.BaseDirectory;

            string jsonfile = startPath + @"appsettings.json";

            using (StreamReader file = File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject appSettings = (JObject)JToken.ReadFrom(reader);

                    if (key_path.Contains('.'))
                    {
                        string[] paths = key_path.Split('.');

                        if (paths.Length == 2)
                        {
                            try
                            {
                                var value1 = appSettings[paths[0]] as JObject;
                                var value2 = value1[paths[1]];
                                return value2;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Wrong appsettings key path: {key_path}. {ex.Message}");
                                return null;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Wrong appsettings key: {key_path}.");
                            return null;
                        }
                    }
                    else
                    {
                        var dictValue = appSettings[key_path] as JObject;

                        return dictValue.ToObject<Dictionary<string, object>>();
                    }
                }
            }
        }
    }
}
