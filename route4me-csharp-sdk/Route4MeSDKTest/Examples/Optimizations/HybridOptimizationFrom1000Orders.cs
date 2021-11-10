using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using CsvHelper;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void HybridOptimizationFrom1000Orders()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager("11111111111111111111111111111111");

            #region ======= Add scheduled address book locations to an user account ================================
            string sAddressFile = @"Data/orders_1000.csv";

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                var csv = new CsvReader(reader);
                //int iCount = 0;
                while (csv.Read())
                {
                    var lng = csv.GetField(0);
                    var lat = csv.GetField(1);
                    var alias = csv.GetField(2);
                    var address1 = csv.GetField(3);
                    var city = csv.GetField(4);
                    var state = csv.GetField(5);
                    var zip = csv.GetField(6);
                    var phone = csv.GetField(7);
                    var sched_date = csv.GetField(8);

                    string sAddress = "";

                    if (address1 != null) sAddress += address1.ToString().Trim();
                    if (city != null) sAddress += ", " + city.ToString().Trim();
                    if (state != null) sAddress += ", " + state.ToString().Trim();
                    if (zip != null) sAddress += ", " + zip.ToString().Trim();

                    if (sAddress == "") continue;

                    Order newOrder = new Order();

                    if (lng != null) newOrder.CachedLat = Convert.ToDouble(lng);
                    if (lat != null) newOrder.CachedLng = Convert.ToDouble(lat);
                    if (alias != null) newOrder.AddressAlias = alias.ToString().Trim();
                    newOrder.Address1 = sAddress;
                    if (phone != null) newOrder.ExtFieldPhone = phone.ToString().Trim();

                    DateTime dt = DateTime.Now;

                    if (sched_date != null)
                    {
                        if (DateTime.TryParse(sched_date.ToString(), out dt))
                        {
                            dt = Convert.ToDateTime(sched_date);
                            newOrder.DayScheduledFor_YYYYMMDD = dt.ToString("yyyy-MM-dd");
                        }
                    }

                    Order resultOrder = route4Me.AddOrder(newOrder, out string errorString);
                    showResult(resultOrder, errorString);

                    Thread.Sleep(1000);
                    //iCount++;
                    //if (iCount == 60) break;
                }
            };

            #endregion

            Thread.Sleep(2000);

            #region ======= Get Hybrid Optimization ================================
            TimeSpan tsp1day = new TimeSpan(1, 0, 0, 0);
            List<string> lsScheduledDays = new List<string>();
            DateTime curDate = DateTime.Now;
            for (int i = 0; i < 5; i++)
            {
                curDate += tsp1day;
                lsScheduledDays.Add(curDate.ToString("yyyy-MM-dd"));
            }

            Address[] Depots = new Address[] {
                new Address {
                        AddressString = "2017 Ambler Ave, Abilene, TX, 79603-2239",
                        IsDepot = true,
                        Latitude = 32.474395,
                        Longitude = -99.7447021,
                        CurbsideLatitude = 32.474395,
                        CurbsideLongitude = -99.7447021
                    },
                new Address {
                        AddressString = "807 Ridge Rd, Alamo, TX, 78516-9596",
                        IsDepot = true,
                        Latitude = 26.170834,
                        Longitude = -98.116201,
                        CurbsideLatitude = 26.170834,
                        CurbsideLongitude = -98.116201
                    },
                new Address {
                        AddressString = "1430 W Amarillo Blvd, Amarillo, TX, 79107-5505",
                        IsDepot = true,
                        Latitude = 35.221969,
                        Longitude = -101.835288,
                        CurbsideLatitude = 35.221969,
                        CurbsideLongitude = -101.835288
                    },
                new Address {
                        AddressString = "3611 Ne 24Th Ave, Amarillo, TX, 79107-7242",
                        IsDepot = true,
                        Latitude = 35.236626,
                        Longitude = -101.795117,
                        CurbsideLatitude = 35.236626,
                        CurbsideLongitude = -101.795117
                    },
                new Address {
                        AddressString = "1525 New York Ave, Arlington, TX, 76010-4723",
                        IsDepot = true,
                        Latitude = 32.720524,
                        Longitude = -97.080195,
                        CurbsideLatitude = 32.720524,
                        CurbsideLongitude = -97.080195
                    }
            };

            string errorString1;
            string errorString2;
            string errorString3;

            foreach (string ScheduledDay in lsScheduledDays)
            {
                HybridOptimizationParameters hparams = new HybridOptimizationParameters()
                {
                    TargetDateString = ScheduledDay,
                    TimezoneOffsetMinutes = 480
                };

                DataObject resultOptimization = route4Me.GetHybridOptimization(hparams, out errorString1);

                string HybridOptimizationId = "";

                if (resultOptimization != null)
                {
                    HybridOptimizationId = resultOptimization.OptimizationProblemId;
                    Console.WriteLine("Hybrid optimization generating executed successfully");

                    Console.WriteLine("Generated hybrid optimization ID: {0}", HybridOptimizationId);
                }
                else
                {
                    Console.WriteLine("Hybrid optimization generating error: {0}", errorString1);
                    continue;
                }

                //============== Add Depot To Hybrid Optimization ===============
                var hDepotParams = new HybridDepotParameters()
                {
                    OptimizationProblemId = HybridOptimizationId,
                    DeleteOldDepots = true,
                    NewDepots = new Address[] { Depots[lsScheduledDays.IndexOf(ScheduledDay)] }
                };

                var addDepotResult = route4Me.AddDepotsToHybridOptimization(hDepotParams, out errorString3);

                Thread.Sleep(5000);

                //============== Reoptimization =================================
                OptimizationParameters optimizationParameters = new OptimizationParameters()
                {
                    OptimizationProblemID = HybridOptimizationId,
                    ReOptimize = true
                };

                DataObject finalOptimization = route4Me.UpdateOptimization(optimizationParameters, out errorString2);

                Console.WriteLine("");

                if (finalOptimization != null)
                {
                    Console.WriteLine("ReOptimization executed successfully");

                    Console.WriteLine("Optimization Problem ID: {0}", finalOptimization.OptimizationProblemId);
                    Console.WriteLine("State: {0}", finalOptimization.State);
                }
                else
                {
                    Console.WriteLine("ReOptimization error: {0}", errorString2);
                }

                Thread.Sleep(5000);
                //=================================================================
            }

            #endregion
        }

        private void showResult(object result, string errorString)
        {
            if (result == null)
            {
                Console.WriteLine("AddAddressBookContact error: {0}", errorString); return;
            }

            string addressId = "";
            string sAddressbookType = "Route4MeSDK.DataTypes.AddressBookContact";
            if (result.GetType().ToString() == sAddressbookType)
            {
                AddressBookContact contact = (AddressBookContact)result;
                addressId = contact.AddressId.ToString();
            }
            else
            {
                Order order = (Order)result;
                addressId = order.OrderId.ToString();
            }
            Console.WriteLine("");

            if (result != null)
            {
                Console.WriteLine("AddAddressBookContact executed successfully");

                Console.WriteLine("AddressId: {0}", addressId);
            }
            else
            {
                Console.WriteLine("AddAddressBookContact error: {0}", errorString);
            }
        }
    }
}
