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
        public void HybridOptimizationFrom1000Addresses()
        {
            // Create the manager with the api key
            Route4MeManager route4Me = new Route4MeManager("11111111111111111111111111111111");

            #region ======= Add scheduled address book locations to an user account ================================
            string sAddressFile = @"Data/addresses_1000.csv";
            Schedule sched0 = new Schedule("daily", false);
            //var csv = new CsvReader(File.OpenText("file.csv"));

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
                    //var sched_date = csv.GetField(8);
                    var sched_mode = csv.GetField(8);
                    var sched_enabled = csv.GetField(9);
                    var sched_every = csv.GetField(10);
                    var sched_weekdays = csv.GetField(11);
                    var sched_monthly_mode = csv.GetField(12);
                    var sched_monthly_dates = csv.GetField(13);
                    var sched_annually_usenth = csv.GetField(14);
                    var sched_annually_months = csv.GetField(15);
                    var sched_nth_n = csv.GetField(16);
                    var sched_nth_what = csv.GetField(17);

                    string sAddress = "";

                    if (address1 != null) sAddress += address1.ToString().Trim();
                    if (city != null) sAddress += ", " + city.ToString().Trim();
                    if (state != null) sAddress += ", " + state.ToString().Trim();
                    if (zip != null) sAddress += ", " + zip.ToString().Trim();

                    if (sAddress == "") continue;

                    AddressBookContact newLocation = new AddressBookContact();

                    if (lng != null) newLocation.CachedLng = Convert.ToDouble(lng);
                    if (lat != null) newLocation.CachedLat = Convert.ToDouble(lat);
                    if (alias != null) newLocation.AddressAlias = alias.ToString().Trim();
                    newLocation.Address1 = sAddress;
                    if (phone != null) newLocation.AddressPhoneNumber = phone.ToString().Trim();

                    //newLocation.schedule = new Schedule[]{};
                    if (!sched0.ValidateScheduleMode(sched_mode)) continue;

                    bool blNth = false;
                    if (sched0.ValidateScheduleMonthlyMode(sched_monthly_mode))
                    {
                        if (sched_monthly_mode == "nth") blNth = true;
                    }
                    if (sched0.ValidateScheduleUseNth(sched_annually_usenth))
                    {
                        if (sched_annually_usenth.ToString().ToLower() == "true") blNth = true;
                    }

                    Schedule schedule = new Schedule(sched_mode.ToString(), blNth);

                    DateTime dt = DateTime.Now;
                    //if (schedule.ValidateScheduleMode(sched_mode))
                    //{
                    schedule.Mode = sched_mode.ToString();
                    if (schedule.ValidateScheduleEnabled(sched_enabled))
                    {
                        schedule.Enabled = Convert.ToBoolean(sched_enabled);
                        if (schedule.ValidateScheduleEvery(sched_every))
                        {
                            int iEvery = Convert.ToInt32(sched_every);
                            switch (schedule.Mode)
                            {
                                case "daily":
                                    schedule.Daily.Every = iEvery;
                                    break;
                                case "weekly":
                                    if (schedule.ValidateScheduleWeekdays(sched_weekdays))
                                    {
                                        schedule.Weekly.Every = iEvery;
                                        string[] arWeekdays = sched_weekdays.Split(',');
                                        List<int> lsWeekdays = new List<int>();
                                        for (int i = 0; i < arWeekdays.Length; i++)
                                        {
                                            lsWeekdays.Add(Convert.ToInt32(arWeekdays[i]));
                                        }
                                        schedule.Weekly.Weekdays = lsWeekdays.ToArray();
                                    }
                                    break;
                                case "monthly":
                                    if (schedule.ValidateScheduleMonthlyMode(sched_monthly_mode))
                                    {
                                        schedule.Monthly.Every = iEvery;
                                        schedule.Monthly.Mode = sched_monthly_mode.ToString();
                                        switch (schedule.Monthly.Mode)
                                        {
                                            case "dates":
                                                if (schedule.ValidateScheduleMonthDays(sched_monthly_dates))
                                                {
                                                    string[] arMonthdays = sched_monthly_dates.Split(',');
                                                    List<int> lsMonthdays = new List<int>();
                                                    for (int i = 0; i < arMonthdays.Length; i++)
                                                    {
                                                        lsMonthdays.Add(Convert.ToInt32(arMonthdays[i]));
                                                    }
                                                    schedule.Monthly.Dates = lsMonthdays.ToArray();
                                                }
                                                break;
                                            case "nth":
                                                if (schedule.ValidateScheduleNthN(sched_nth_n)) schedule.Monthly.Nth.N = Convert.ToInt32(sched_nth_n);
                                                if (schedule.ValidateScheduleNthWhat(sched_nth_what)) schedule.Monthly.Nth.What = Convert.ToInt32(sched_nth_what);
                                                break;
                                        }
                                    }
                                    break;
                                case "annually":
                                    if (schedule.ValidateScheduleUseNth(sched_annually_usenth))
                                    {
                                        schedule.Annually.Every = iEvery;
                                        schedule.Annually.UseNth = Convert.ToBoolean(sched_annually_usenth);
                                        if (schedule.Annually.UseNth)
                                        {
                                            if (schedule.ValidateScheduleNthN(sched_nth_n)) schedule.Annually.Nth.N = Convert.ToInt32(sched_nth_n);
                                            if (schedule.ValidateScheduleNthWhat(sched_nth_what)) schedule.Annually.Nth.What = Convert.ToInt32(sched_nth_what);
                                        }
                                        else
                                        {
                                            if (schedule.ValidateScheduleYearMonths(sched_annually_months))
                                            {
                                                string[] arYearmonths = sched_annually_months.Split(',');
                                                List<int> lsMonths = new List<int>();
                                                for (int i = 0; i < arYearmonths.Length; i++)
                                                {
                                                    lsMonths.Add(Convert.ToInt32(arYearmonths[i]));
                                                }
                                                schedule.Annually.Months = lsMonths.ToArray();
                                            }
                                        }
                                    }
                                    break;
                            }
                        }

                    }
                    newLocation.Schedule = (new List<Schedule>() { schedule }).ToArray();
                    //}

                    var resultContact = route4Me.AddAddressBookContact(newLocation, out string errorString);

                    showResult(resultContact, errorString);

                    Thread.Sleep(1000);

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
            //string[] ScheduledDays = new string[] { "2017-03-06", "2017-03-07", "2017-03-08", "2017-03-09", "2017-03-10" };

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
                    TimezoneOffsetMinutes = -240
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
                HybridDepotParameters hDepotParams = new HybridDepotParameters()
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
    }
}
