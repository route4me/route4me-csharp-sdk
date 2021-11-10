using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating an optimization 
        /// with multi-depot, multi-driver, time windows options.
        /// </summary>
        public void MultipleDepotMultipleDriverTimeWindow()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            var addresses = new Address[]
            {
        #region Addresses

        new Address() { AddressString   = "455 S 4th St, Louisville, KY 40202",
                        IsDepot         = true,
                        Latitude        = 38.251698,
                        Longitude       = -85.757308,
                        Time            = 300,
                        TimeWindowStart = 28800,
                        TimeWindowEnd   = 30477 },

        new Address() { AddressString   = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                        Latitude        = 38.141598,
                        Longitude       = -85.793846,
                        Time            = 300,
                        TimeWindowStart = 30477,
                        TimeWindowEnd   = 33406 },

        new Address() { AddressString   = "1407 א53MCCOY, Louisville, KY, 40215",
                        Latitude        = 38.202496,
                        Longitude       = -85.786514,
                        Time            = 300,
                        TimeWindowStart = 33406,
                        TimeWindowEnd   = 36228 },
        new Address() {
                        AddressString   = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                        Latitude        = 38.178844,
                        Longitude       = -85.774864,
                        Time            = 300,
                        TimeWindowStart = 36228,
                        TimeWindowEnd   = 37518 },

        new Address() { AddressString   = "730 CECIL AVENUE, Louisville, KY, 40211",
                        Latitude        = 38.248684,
                        Longitude       = -85.821121,
                        Time            = 300,
                        TimeWindowStart = 37518,
                        TimeWindowEnd   = 39550 },

        new Address() { AddressString   = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                        Latitude        = 38.251923,
                        Longitude       = -85.800034,
                        Time            = 300,
                        TimeWindowStart = 39550,
                        TimeWindowEnd   = 41348 },

        new Address() { AddressString   = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                        Latitude        = 38.176067,
                        Longitude       = -85.824638,
                        Time            = 300,
                        TimeWindowStart = 41348,
                        TimeWindowEnd   = 42261 },

        new Address() { AddressString   = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                        Latitude        = 38.179806,
                        Longitude       = -85.775558,
                        Time            = 300,
                        TimeWindowStart = 42261,
                        TimeWindowEnd   = 45195 },

        new Address() { AddressString   = "318 SO. 39TH STREET, Louisville, KY, 40212",
                        Latitude        = 38.259335,
                        Longitude       = -85.815094,
                        Time            = 300,
                        TimeWindowStart = 45195,
                        TimeWindowEnd   = 46549 },

        new Address() { AddressString   = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                        IsDepot         = true,
                        Latitude        = 38.179253,
                        Longitude       = -85.785118,
                        Time            = 300,
                        TimeWindowStart = 46549,
                        TimeWindowEnd   = 47353 },

        new Address() { AddressString   = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                        Latitude        = 38.162472,
                        Longitude       = -85.792854,
                        Time            = 300,
                        TimeWindowStart = 47353,
                        TimeWindowEnd   = 50924 },

        new Address() { AddressString   = "1661 W HILL ST, Louisville, KY, 40210",
                        Latitude        = 38.229584,
                        Longitude       = -85.783966,
                        Time            = 300,
                        TimeWindowStart = 50924,
                        TimeWindowEnd   = 51392 },

        new Address() { AddressString   = "3222 KINGSWOOD WAY, Louisville, KY, 40216",
                        Latitude        = 38.210606,
                        Longitude       = -85.822594,
                        Time            = 300,
                        TimeWindowStart = 51392,
                        TimeWindowEnd   = 52451 },

        new Address() { AddressString   = "1922 PALATKA RD, Louisville, KY, 40214",
                        Latitude        = 38.153767,
                        Longitude       = -85.796783,
                        Time            = 300,
                        TimeWindowStart = 52451,
                        TimeWindowEnd   = 55631 },

        new Address() { AddressString   = "1314 SOUTH 26TH STREET, Louisville, KY, 40210",
                        Latitude        = 38.235847,
                        Longitude       = -85.796852,
                        Time            = 300,
                        TimeWindowStart = 55631,
                        TimeWindowEnd   = 58516 },

        new Address() { AddressString   = "2135 MCCLOSKEY AVENUE, Louisville, KY, 40210",
                        Latitude        = 38.218662,
                        Longitude       = -85.789032,
                        Time            = 300,
                        TimeWindowStart = 58516,
                        TimeWindowEnd   = 61080 },

        new Address() { AddressString   = "1409 PHYLLIS AVE, Louisville, KY, 40215",
                        Latitude        = 38.206154,
                        Longitude       = -85.781387,
                        Time            = 100,
                        TimeWindowStart = 61080,
                        TimeWindowEnd   = 61504 },

        new Address() { AddressString   = "4504 SUNFLOWER AVE, Louisville, KY, 40216",
                        Latitude        = 38.187511,
                        Longitude       = -85.839149,
                        Time            = 300,
                        TimeWindowStart = 61504,
                        TimeWindowEnd   = 62061 },

        new Address() { AddressString   = "2512 GREENWOOD AVE, Louisville, KY, 40210",
                        Latitude        = 38.241405,
                        Longitude       = -85.795059,
                        Time            = 300,
                        TimeWindowStart = 62061,
                        TimeWindowEnd   = 65012 },

        new Address() { AddressString   = "5500 WILKE FARM AVE, Louisville, KY, 40216",
                        Latitude        = 38.166065,
                        Longitude       = -85.863319,
                        Time            = 300,
                        TimeWindowStart = 65012,
                        TimeWindowEnd   = 67541 },

        new Address() { AddressString   = "3640 LENTZ AVE, Louisville, KY, 40215",
                        Latitude        = 38.193283,
                        Longitude       = -85.786201,
                        Time            = 300,
                        TimeWindowStart = 67541,
                        TimeWindowEnd   = 69120 },

        new Address() { AddressString   = "1020 BLUEGRASS AVE, Louisville, KY, 40215",
                        Latitude        = 38.17952,
                        Longitude       = -85.780037,
                        Time            = 300,
                        TimeWindowStart = 69120,
                        TimeWindowEnd   = 70572 },

        new Address() { AddressString   = "123 NORTH 40TH ST, Louisville, KY, 40212",
                        Latitude        = 38.26498,
                        Longitude       = -85.814156,
                        Time            = 300,
                        TimeWindowStart = 70572,
                        TimeWindowEnd   = 73177 },

        new Address() { AddressString   = "7315 ST ANDREWS WOODS CIRCLE UNIT 104, Louisville, KY, 40214",
                        Latitude        = 38.151072,
                        Longitude       = -85.802867,
                        Time            = 300,
                        TimeWindowStart = 73177,
                        TimeWindowEnd   = 75231 },

        new Address() { AddressString   = "3210 POPLAR VIEW DR, Louisville, KY, 40216",
                        Latitude        = 38.182594,
                        Longitude       = -85.849937,
                        Time            = 300,
                        TimeWindowStart = 75231,
                        TimeWindowEnd   = 77663 },

        new Address() { AddressString   = "4519 LOUANE WAY, Louisville, KY, 40216",
                        Latitude        = 38.1754,
                        Longitude       = -85.811447,
                        Time            = 300,
                        TimeWindowStart = 77663,
                        TimeWindowEnd   = 79796 },

        new Address() { AddressString   = "6812 MANSLICK RD, Louisville, KY, 40214",
                        Latitude        = 38.161839,
                        Longitude       = -85.798279,
                        Time            = 300,
                        TimeWindowStart = 79796,
                        TimeWindowEnd   = 80813 },

        new Address() { AddressString   = "1524 HUNTOON AVENUE, Louisville, KY, 40215",
                        Latitude        = 38.172031,
                        Longitude       = -85.788353,
                        Time            = 300,
                        TimeWindowStart = 80813,
                        TimeWindowEnd   = 83956 },

        new Address() { AddressString   = "1307 LARCHMONT AVE, Louisville, KY, 40215",
                        Latitude        = 38.209663,
                        Longitude       = -85.779816,
                        Time            = 300,
                        TimeWindowStart = 83956,
                        TimeWindowEnd   = 84365 },

        new Address() { AddressString   = "434 N 26TH STREET #2, Louisville, KY, 40212",
                        Latitude        = 38.26844,
                        Longitude       = -85.791962,
                        Time            = 300,
                        TimeWindowStart = 84365,
                        TimeWindowEnd   = 85367 },

        new Address() { AddressString   = "678 WESTLAWN ST, Louisville, KY, 40211",
                        Latitude        = 38.250397,
                        Longitude       = -85.80629,
                        Time            = 300,
                        TimeWindowStart = 85367,
                        TimeWindowEnd   = 86400 },

        new Address() { AddressString   = "2308 W BROADWAY, Louisville, KY, 40211",
                        Latitude        = 38.248882,
                        Longitude       = -85.790421,
                        Time            = 300,
                        TimeWindowStart = 86400,
                        TimeWindowEnd   = 88703},

        new Address() { AddressString   = "2332 WOODLAND AVE, Louisville, KY, 40210",
                        Latitude        = 38.233579,
                        Longitude       = -85.794257,
                        Time            = 300,
                        TimeWindowStart = 88703,
                        TimeWindowEnd   = 89320 },

        new Address() { AddressString   = "1706 WEST ST. CATHERINE, Louisville, KY, 40210",
                        Latitude        = 38.239697,
                        Longitude       = -85.783928,
                        Time            = 300,
                        TimeWindowStart = 89320,
                        TimeWindowEnd   = 90054 },

        new Address() { AddressString   = "1699 WATHEN LN, Louisville, KY, 40216",
                        Latitude        = 38.216465,
                        Longitude       = -85.792397,
                        Time            = 300,
                        TimeWindowStart = 90054,
                        TimeWindowEnd   = 91150 },

        new Address() { AddressString   = "2416 SUNSHINE WAY, Louisville, KY, 40216",
                        Latitude        = 38.186245,
                        Longitude       = -85.831787,
                        Time            = 300,
                        TimeWindowStart = 91150,
                        TimeWindowEnd   = 91915 },

        new Address() { AddressString   = "6925 MANSLICK RD, Louisville, KY, 40214",
                        Latitude        = 38.158466,
                        Longitude       = -85.798355,
                        Time            = 300,
                        TimeWindowStart = 91915,
                        TimeWindowEnd   = 93407 },

        new Address() { AddressString   = "2707 7TH ST, Louisville, KY, 40215",
                        Latitude        = 38.212438,
                        Longitude       = -85.785082,
                        Time            = 300,
                        TimeWindowStart = 93407,
                        TimeWindowEnd   = 95992 },

        new Address() { AddressString   = "2014 KENDALL LN, Louisville, KY, 40216",
                        Latitude        = 38.179394,
                        Longitude       = -85.826668,
                        Time            = 300,
                        TimeWindowStart = 95992,
                        TimeWindowEnd   = 99307 },

        new Address() { AddressString   = "612 N 39TH ST, Louisville, KY, 40212",
                        Latitude        = 38.273354,
                        Longitude       = -85.812012,
                        Time            = 300,
                        TimeWindowStart = 99307,
                        TimeWindowEnd   = 102906 },

        new Address() { AddressString   = "2215 ROWAN ST, Louisville, KY, 40212",
                        Latitude        = 38.261703,
                        Longitude       = -85.786781,
                        Time            = 300,
                        TimeWindowStart = 102906,
                        TimeWindowEnd   = 106021 },

        new Address() { AddressString   = "1826 W. KENTUCKY ST, Louisville, KY, 40210",
                        Latitude        = 38.241611,
                        Longitude       = -85.78653,
                        Time            = 300,
                        TimeWindowStart = 106021,
                        TimeWindowEnd   = 107276 },

        new Address() { AddressString   = "1810 GREGG AVE, Louisville, KY, 40210",
                        Latitude        = 38.224716,
                        Longitude       = -85.796211,
                        Time            = 300,
                        TimeWindowStart = 107276,
                        TimeWindowEnd   = 107948 },

        new Address() { AddressString   = "4103 BURRRELL DRIVE, Louisville, KY, 40216",
                        Latitude        = 38.191753,
                        Longitude       = -85.825836,
                        Time            = 300,
                        TimeWindowStart = 107948,
                        TimeWindowEnd   = 108414 },

        new Address() { AddressString   = "359 SOUTHWESTERN PKWY, Louisville, KY, 40212",
                        Latitude        = 38.259903,
                        Longitude       = -85.823463,
                        Time            = 200,
                        TimeWindowStart = 108414,
                        TimeWindowEnd   = 108685 },

        new Address() { AddressString   = "2407 W CHESTNUT ST, Louisville, KY, 40211",
                        Latitude        = 38.252781,
                        Longitude       = -85.792109,
                        Time            = 300,
                        TimeWindowStart = 108685,
                        TimeWindowEnd   = 110109 },

        new Address() { AddressString   = "225 S 22ND ST, Louisville, KY, 40212",
                        Latitude        = 38.257616,
                        Longitude       = -85.786658,
                        Time            = 300,
                        TimeWindowStart = 110109,
                        TimeWindowEnd   = 111375 },

        new Address() { AddressString   = "1404 MCCOY AVE, Louisville, KY, 40215",
                        Latitude        = 38.202122,
                        Longitude       = -85.786072,
                        Time            = 300,
                        TimeWindowStart = 111375,
                        TimeWindowEnd   = 112120 },

        new Address() { AddressString   = "117 FOUNT LANDING CT, Louisville, KY, 40212",
                        Latitude        = 38.270061,
                        Longitude       = -85.799438,
                        Time            = 300,
                        TimeWindowStart = 112120,
                        TimeWindowEnd   = 114095 },

        new Address() { AddressString   = "5504 SHOREWOOD DRIVE, Louisville, KY, 40214",
                        Latitude        = 38.145851,
                        Longitude       = -85.7798,
                        Time            = 300,
                        TimeWindowStart = 114095,
                        TimeWindowEnd   = 115743 },

        new Address() { AddressString   = "1406 CENTRAL AVE, Louisville, KY, 40208",
                        Latitude        = 38.211025,
                        Longitude       = -85.780251,
                        Time            = 300,
                        TimeWindowStart = 115743,
                        TimeWindowEnd   = 117716 },

        new Address() { AddressString   = "901 W WHITNEY AVE, Louisville, KY, 40215",
                        Latitude        = 38.194115,
                        Longitude       = -85.77494,
                        Time            = 300,
                        TimeWindowStart = 117716,
                        TimeWindowEnd   = 119078 },

        new Address() { AddressString   = "2109 SCHAFFNER AVE, Louisville, KY, 40210",
                        Latitude        = 38.219699,
                        Longitude       = -85.779363,
                        Time            = 300,
                        TimeWindowStart = 119078,
                        TimeWindowEnd   = 121147 },

        new Address() { AddressString   = "2906 DIXIE HWY, Louisville, KY, 40216",
                        Latitude        = 38.209278,
                        Longitude       = -85.798653,
                        Time            = 300,
                        TimeWindowStart = 121147,
                        TimeWindowEnd   = 124281 },

        new Address() { AddressString   = "814 WWHITNEY AVE, Louisville, KY, 40215",
                        Latitude        = 38.193596,
                        Longitude       = -85.773521,
                        Time            = 300,
                        TimeWindowStart = 124281,
                        TimeWindowEnd   = 124675 },

        new Address() { AddressString   = "1610 ALGONQUIN PWKY, Louisville, KY, 40210",
                        Latitude        = 38.222153,
                        Longitude       = -85.784187,
                        Time            = 300,
                        TimeWindowStart = 124675,
                        TimeWindowEnd   = 127148 },

        new Address() { AddressString   = "3524 WHEELER AVE, Louisville, KY, 40215",
                        Latitude        = 38.195293,
                        Longitude       = -85.788643,
                        Time            = 300,
                        TimeWindowStart = 127148,
                        TimeWindowEnd   = 130667 },

        new Address() { AddressString   = "5009 NEW CUT RD, Louisville, KY, 40214",
                        Latitude        = 38.165905,
                        Longitude       = -85.779701,
                        Time            = 300,
                        TimeWindowStart = 130667,
                        TimeWindowEnd   = 131980 },

        new Address() { AddressString   = "3122 ELLIOTT AVE, Louisville, KY, 40211",
                        Latitude        = 38.251213,
                        Longitude       = -85.804199,
                        Time            = 300,
                        TimeWindowStart = 131980,
                        TimeWindowEnd   = 134402 },

        new Address() { AddressString   = "911 GAGEL AVE, Louisville, KY, 40216",
                        Latitude        = 38.173512,
                        Longitude       = -85.807854,
                        Time            = 300,
                        TimeWindowStart = 134402,
                        TimeWindowEnd   = 136787 },

        new Address() { AddressString   = "4020 GARLAND AVE #lOOA, Louisville, KY, 40211",
                        Latitude        = 38.246181,
                        Longitude       = -85.818901,
                        Time            = 300,
                        TimeWindowStart = 136787,
                        TimeWindowEnd   = 138073 },

        new Address() { AddressString   = "5231 MT HOLYOKE DR, Louisville, KY, 40216",
                        Latitude        = 38.169369,
                        Longitude       = -85.85704,
                        Time            = 300,
                        TimeWindowStart = 138073,
                        TimeWindowEnd   = 141407 },

        new Address() { AddressString   = "1339 28TH S #2, Louisville, KY, 40211",
                        Latitude        = 38.235275,
                        Longitude       = -85.800156,
                        Time            = 300,
                        TimeWindowStart = 141407,
                        TimeWindowEnd   = 143561 },

        new Address() { AddressString   = "836 S 36TH ST, Louisville, KY, 40211",
                        Latitude        = 38.24651,
                        Longitude       = -85.811234,
                        Time            = 300,
                        TimeWindowStart = 143561,
                        TimeWindowEnd   = 145941 },

        new Address() { AddressString   = "2132 DUNCAN STREET, Louisville, KY, 40212",
                        Latitude        = 38.262135,
                        Longitude       = -85.785172,
                        Time            = 300,
                        TimeWindowStart = 145941,
                        TimeWindowEnd   = 148296 },

        new Address() { AddressString   = "3529 WHEELER AVE, Louisville, KY, 40215",
                        Latitude        = 38.195057,
                        Longitude       = -85.787949,
                        Time            = 300,
                        TimeWindowStart = 148296,
                        TimeWindowEnd   = 150177 },

        new Address() { AddressString   = "2829 DE MEL #11, Louisville, KY, 40214",
                        Latitude        = 38.171662,
                        Longitude       = -85.807271,
                        Time            = 300,
                        TimeWindowStart = 150177,
                        TimeWindowEnd   = 150981 },

        new Address() { AddressString   = "1325 EARL AVENUE, Louisville, KY, 40215",
                        Latitude        = 38.204556,
                        Longitude       = -85.781555,
                        Time            = 300,
                        TimeWindowStart = 150981,
                        TimeWindowEnd   = 151854 },

        new Address() { AddressString   = "3632 MANSLICK RD #10, Louisville, KY, 40215",
                        Latitude        = 38.193542,
                        Longitude       = -85.801147,
                        Time            = 300,
                        TimeWindowStart = 151854,
                        TimeWindowEnd   = 152613 },

        new Address() { AddressString   = "637 S 41ST ST, Louisville, KY, 40211",
                        Latitude        = 38.253632,
                        Longitude       = -85.81897,
                        Time            = 300,
                        TimeWindowStart = 152613,
                        TimeWindowEnd   = 156131 },

        new Address() { AddressString   = "3420 VIRGINIA AVENUE, Louisville, KY, 40211",
                        Latitude        = 38.238693,
                        Longitude       = -85.811386,
                        Time            = 300,
                        TimeWindowStart = 156131,
                        TimeWindowEnd   = 157212 },

        new Address() { AddressString   = "3501 MALIBU CT APT 6, Louisville, KY, 40216",
                        Latitude        = 38.166481,
                        Longitude       = -85.825928,
                        Time            = 300,
                        TimeWindowStart = 157212,
                        TimeWindowEnd   = 158655 },

        new Address() { AddressString   = "4912 DIXIE HWY, Louisville, KY, 40216",
                        Latitude        = 38.170728,
                        Longitude       = -85.826817,
                        Time            = 300,
                        TimeWindowStart = 158655,
                        TimeWindowEnd   = 159145 },

        new Address() { AddressString   = "7720 DINGLEDELL RD, Louisville, KY, 40214",
                        Latitude        = 38.162472,
                        Longitude       = -85.792854,
                        Time            = 300,
                        TimeWindowStart = 159145,
                        TimeWindowEnd   = 161831 },

        new Address() { AddressString   = "2123 RATCLIFFE AVE, Louisville, KY, 40210",
                        Latitude        = 38.21978,
                        Longitude       = -85.797615,
                        Time            = 300,
                        TimeWindowStart = 161831,
                        TimeWindowEnd   = 163705 },

        new Address() {
                        AddressString   = "1321 OAKWOOD AVE, Louisville, KY, 40215",
                        Latitude        = 38.17704,
                        Longitude       = -85.783829,
                        Time            = 300,
                        TimeWindowStart = 163705,
                        TimeWindowEnd   = 164953 },

        new Address() { AddressString   = "2223 WEST KENTUCKY STREET, Louisville, KY, 40210",
                        Latitude        = 38.242516,
                        Longitude       = -85.790695,
                        Time            = 300,
                        TimeWindowStart = 164953,
                        TimeWindowEnd   = 166189 },

        new Address() { AddressString   = "8025 GLIMMER WAY #3308, Louisville, KY, 40214",
                        Latitude        = 38.131981,
                        Longitude       = -85.77935,
                        Time            = 300,
                        TimeWindowStart = 166189,
                        TimeWindowEnd   = 166640 },

        new Address() { AddressString   = "1155 S 28TH ST, Louisville, KY, 40211",
                        Latitude        = 38.238621,
                        Longitude       = -85.799911,
                        Time            = 300,
                        TimeWindowStart = 166640,
                        TimeWindowEnd   = 168147 },

        new Address() { AddressString   = "840 IROQUOIS AVE, Louisville, KY, 40214",
                        Latitude        = 38.166355,
                        Longitude       = -85.779396,
                        Time            = 300,
                        TimeWindowStart = 168147,
                        TimeWindowEnd   = 170385
        },

        new Address() { AddressString   = "5573 BRUCE AVE, Louisville, KY, 40214",
                        Latitude        = 38.145222,
                        Longitude       = -85.779205,
                        Time            = 300,
                        TimeWindowStart = 170385,
                        TimeWindowEnd   = 171096 },

        new Address() { AddressString   = "1727 GALLAGHER, Louisville, KY, 40210",
                        Latitude        = 38.239334,
                        Longitude       = -85.784882,
                        Time            = 300,
                        TimeWindowStart = 171096,
                        TimeWindowEnd   = 171951 },

        new Address() { AddressString   = "1309 CATALPA ST APT 204, Louisville, KY, 40211",
                        Latitude        = 38.236524,
                        Longitude       = -85.801619,
                        Time            = 300,
                        TimeWindowStart = 171951,
                        TimeWindowEnd   = 172393 },

        new Address() { AddressString   = "1330 ALGONQUIN PKWY, Louisville, KY, 40208",
                        Latitude        = 38.219846,
                        Longitude       = -85.777344,
                        Time            = 300,
                        TimeWindowStart = 172393,
                        TimeWindowEnd   = 175337 },

        new Address() { AddressString   = "823 SUTCLIFFE, Louisville, KY, 40211",
                        Latitude        = 38.246956,
                        Longitude       = -85.811569,
                        Time            = 300,
                        TimeWindowStart = 175337,
                        TimeWindowEnd   = 176867 },

        new Address() { AddressString   = "4405 CHURCHMAN AVENUE #2, Louisville, KY, 40215",
                        Latitude        = 38.177768,
                        Longitude       = -85.792545,
                        Time            = 300,
                        TimeWindowStart = 176867,
                        TimeWindowEnd   = 178051 },

        new Address() { AddressString   = "3211 DUMESNIL ST #1, Louisville, KY, 40211",
                        Latitude        = 38.237789,
                        Longitude       = -85.807968,
                        Time            = 300,
                        TimeWindowStart = 178051,
                        TimeWindowEnd   = 179083 },

        new Address() { AddressString   = "3904 WEWOKA AVE, Louisville, KY, 40212",
                        Latitude        = 38.270367,
                        Longitude       = -85.813118,
                        Time            = 300,
                        TimeWindowStart = 179083,
                        TimeWindowEnd   = 181543 },

        new Address() { AddressString   = "660 SO. 42ND STREET, Louisville, KY, 40211",
                        Latitude        = 38.252865,
                        Longitude       = -85.822624,
                        Time            = 300,
                        TimeWindowStart = 181543,
                        TimeWindowEnd   = 184193 },

        new Address() { AddressString   = "3619  LENTZ  AVE, Louisville, KY, 40215",
                        Latitude        = 38.193249,
                        Longitude       = -85.785492,
                        Time            = 300,
                        TimeWindowStart = 184193,
                        TimeWindowEnd   = 185853 },

        new Address() { AddressString   = "4305  STOLTZ  CT, Louisville, KY, 40215",
                        Latitude        = 38.178707,
                        Longitude       = -85.787292,
                        Time            = 300,
                        TimeWindowStart = 185853,
                        TimeWindowEnd   = 187252 }

        #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Multiple Depot, Multiple Driver, Time Window",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RT = true,
                RouteMaxDuration = 86400 * 3,
                VehicleCapacity = 99,
                VehicleMaxDistanceMI = 99999,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Matrix
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunOptimization(
                                    optimizationParameters,
                                    out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
