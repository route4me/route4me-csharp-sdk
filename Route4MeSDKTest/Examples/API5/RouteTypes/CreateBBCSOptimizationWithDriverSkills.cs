using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using System;
using System.Collections.Generic;
using static Route4MeSDK.Route4MeManagerV5;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void CreateBBCSOptimizationWithDriverSkills()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            Dictionary<string, string> r4metobbcsid = new Dictionary<string, string>();

            #region Create Skilled Drivers

            int? ownerId = GetOwnerMemberId();

            if (ownerId == null) return;

            #region 1st Driver
            // Driver 1
            var newMemberParameters1 = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "Vaishali",
                MemberLastName = "Deshpande",
                MemberCompany = "Test Member 1 Created",
                MemberEmail = GetTestEmail().Replace("+", "1+"),
                OwnerMemberId = (int)ownerId,
            };

            newMemberParameters1.SetMemberType(DataTypes.V5.MemberTypes.Driver);

            var route4MeV5 = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            var member1 = route4MeV5.CreateTeamMember(newMemberParameters1,
                                                    out DataTypes.V5.ResultResponse resultResponse);

            if (member1 != null && member1.GetType() == typeof(DataTypes.V5.TeamResponse)) membersToRemove.Add(member1);

            var queryParams1 = new MemberQueryParameters()
            {
                UserId = member1.MemberId.ToString()
            };

            string[] skills1 = new string[]
             {
                "MLT3PLUMB", "MLT3AUDIT"
             };

            var updatedMember1 = route4MeV5.AddSkillsToDriver(queryParams1,
                                                            skills1,
                                                            out resultResponse);

            #endregion

            #region 2nd Driver

            var newMemberParameters2 = new TeamRequest()
            {
                NewPassword = testPassword,
                MemberFirstName = "Guru",
                MemberLastName = "Deshpande",
                MemberCompany = "Test Member 2 Created",
                MemberEmail = GetTestEmail().Replace("+", "1+"),
                OwnerMemberId = (int)ownerId,

            };

            newMemberParameters2.SetMemberType(DataTypes.V5.MemberTypes.Driver);

            route4MeV5 = new Route4MeManagerV5(ActualApiKey);

            // Run the query
            var member2 = route4MeV5.CreateTeamMember(newMemberParameters2,
                                                    out resultResponse);

            if (member2 != null && member2.GetType() == typeof(DataTypes.V5.TeamResponse)) membersToRemove.Add(member2);

            var queryParams2 = new MemberQueryParameters()
            {
                UserId = member2.MemberId.ToString()
            };

            string[] skills2 = new string[]
             {
                "MLT3ELEC", "MLT3AUDIT"
             };

            var updatedMember2 = route4MeV5.AddSkillsToDriver(queryParams2,
                                                            skills2,
                                                            out resultResponse);

            #endregion

            #endregion

            // Prepare the addresses
            var addresses = new Address[]
            {
                #region Addresses

                new Address() 
                { 
                    AddressString   = "Orion Tower 1, 19/2, Devarbisana halli, KR puram Hobli",
                    //all possible originating locations are depots, should be marked as true
                    //stylistically we recommend all depots should be at the top of the destinations list
                    IsDepot         = true,
                    Latitude = 12.924868075027042,
                    Longitude = 77.686757839656,
                },

                new Address() 
                { 
                    AddressString   = "Janhavi Shelters,Koppa Rd,Yelenahalli, Akshayanagar",
                    Latitude = 12.867198760559722,
                    Longitude = 77.61945059918025,
                    Time            =  60*60,
                    TimeWindowStart = (10-5)*3600,
                    TimeWindowEnd   = (12-5)*3600,
                    Tags = new string[] {"MLT3PLUMB"},
                },

                new Address() 
                { 
                    AddressString   = "Bannerghatta Main Rd, Classic Orchards Layout, Hulimavu, Bengaluru, Karnataka 560076",
                    Latitude = 12.874972927579234,
                    Longitude = 77.59465999732771,

                    Time            =  60*60,
                    TimeWindowStart = (12-5)*3600,
                    TimeWindowEnd   = (14-5)*3600,
                    Tags = new string[] { "MLT3PLUMB" },
                },

                new Address() 
                { 
                    AddressString   = "DLF Newtown,Akshayanagar",
                    Latitude = 12.877644069349397,
                    Longitude = 77.61905398407804,

                    Time            =  60*60,
                    TimeWindowStart = (14-5)*3600,
                    TimeWindowEnd   = (16-5)*3600,
                    Tags            = new string[] { "MLT3ELEC" },
                },

                new Address() 
                {  
                    AddressString   = "9th Main, 6th Sector, HSR Layout, Bengaluru, Karnataka 560034",
                    Latitude = 12.914053742570712,
                    Longitude = 77.63510075685282,
                    Time            =  60*60,
                    TimeWindowStart = (16-5)*3600,
                    TimeWindowEnd   = (18-5)*3600,
                    Tags = new string[]{ "MLT3PLUMB" }
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                //specify capacitated vehicle routing with time windows and multiple depots, with multiple drivers
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,

                //set an arbitrary route name
                //this value shows up in the website, and all the connected mobile device
                RouteName = "Multiple Depot, Multiple Driver Fixed "+DateTime.Now,

                //the route start date in UTC, unix timestamp seconds (Tomorrow)
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                //the time in UTC when a route is starting (7AM)
                RouteTime = 60 * 60 * (9 - 5),

                //the maximum duration of a route
                VehicleCapacity = 7,
                VehicleMaxDistanceMI = 10000,

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),

                AdvancedConstraints = new DataTypes.V5.RouteAdvancedConstraints[]
                {
                    new DataTypes.V5.RouteAdvancedConstraints()
                    {
                        AvailableTimeWindows = new List<int[]>()   // drivers availability or working hours?
                        {
                            new int[] { 60*60*(9-5),60*60*(20-5) }
                        },
                        MaximumCapacity = 30,
                        MaximumCargoVolume = 15,
                        Tags = new string[] { "MLT3PLUMB" },
                        Route4meMembersId = new int[]
                        {
                            (int)updatedMember1.MemberId
                        }
                    },

                    new DataTypes.V5.RouteAdvancedConstraints()
                    {
                        AvailableTimeWindows = new List<int[]>()   // drivers availability or working hours?
                        {
                            new int[] { 60*60*(9-5),60*60*(20-5) }
                        },
                        MaximumCapacity = 30,
                        MaximumCargoVolume = 15,
                        MembersCount = 10,
                        Tags = new string[] { "MLT3ELEC" },
                        Route4meMembersId = new int[]
                        {
                            (int)updatedMember2.MemberId
                        }
                    }
                }
            };

            var optimizationParameters = new QueryTypes.OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            DataObject dataObject = route4Me.RunOptimization(
                                        optimizationParameters,
                                        out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();

            RemoveTestTeamMembers();
        }
    }
}
