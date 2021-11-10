using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating  
        /// an optimization with 300 stops.
        /// </summary>
        public void Route300Stops()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            Address[] addresses = new Address[]
                {
                    #region Addresses

                    new Address() {
                                AddressString = "449 Schiller st Elizabeth, NJ, 07206",
                                Alias         = "449 Schiller st Elizabeth, NJ, 07206",
                                IsDepot       = true,
                                Latitude      = 40.6608211,
                                Longitude     = -74.1827578,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401",
                                Alias         = "24 Convenience Store LLC, 2519 Pacific Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.355035,
                                Longitude     = -74.441433,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "24/7, 1406 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.361713,
                                Longitude     = -74.428145,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746",
                                Alias         = "6-12 Convienece, 1 South Main Street, Marlboro, NJ, 07746",
                                IsDepot       = false,
                                Latitude      = 40.314719,
                                Longitude     = -74.248578,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102",
                                Alias         = "6Th Ave Groc, 214 6th Ave, Newark, NJ, 07102",
                                IsDepot       = false,
                                Latitude      = 40.756385,
                                Longitude     = -74.187419,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726",
                                Alias         = "76 Express Mart, 46 Ryan Rd, Manalapan, NJ, 07726",
                                IsDepot       = false,
                                Latitude      = 40.301426,
                                Longitude     = -74.259929,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401",
                                Alias         = "801 W Groc, 801 N Indiana, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.368782,
                                Longitude     = -74.438739,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724",
                                Alias         = "91 Farmers Market, 34 Lanes Mill Road, Bricktown, NJ, 08724",
                                IsDepot       = false,
                                Latitude      = 40.095338,
                                Longitude     = -74.144739,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "A&L Mini, 103 Central Ave, Newark, NJ, 07103",
                                Alias         = "A&L Mini, 103 Central Ave, Newark, NJ, 07103",
                                IsDepot       = false,
                                Latitude      = 40.763848,
                                Longitude     = -74.228196,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401",
                                Alias         = "AC Deli & Food Market, 3104 Pacific Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.351864,
                                Longitude     = -74.448293,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401",
                                Alias         = "AC Food Market & Deli 2, 2401 Pacific Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.357207,
                                Longitude     = -74.440922,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055",
                                Alias         = "Ag Mini, 503 Gregory Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.864225,
                                Longitude     = -74.139027,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108",
                                Alias         = "Alexca Groc, 525 Elizabeth Ave, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.708466,
                                Longitude     = -74.201882,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401",
                                Alias         = "Alpha And Omega, 404 Oriental Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.36423,
                                Longitude     = -74.414019,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857",
                                Alias         = "Always at Home Adult Day care, 8a Jocama Blvd, OldBridge, NJ, 08857",
                                IsDepot       = false,
                                Latitude      = 40.37812,
                                Longitude     = -74.305547,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "AM PM, 1338 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.362037,
                                Longitude     = -74.427806,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021",
                                Alias         = "Amaury Groc, 84 North Walnut, East Orange, NJ, 07021",
                                IsDepot       = false,
                                Latitude      = 40.76518,
                                Longitude     = -74.211008,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755",
                                Alias         = "American Way Food, 2005 Route 35 North, Oakhurst, NJ, 07755",
                                IsDepot       = false,
                                Latitude      = 40.263924,
                                Longitude     = -74.040861,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524",
                                Alias         = "Amezquita, 126 Gouvernor St, Paterson, NJ, 07524",
                                IsDepot       = false,
                                Latitude      = 40.922167,
                                Longitude     = -74.163824,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861",
                                Alias         = "Anita's Corner Deli, 664 Brace Avenue, Perth Amboy, NJ, 08861",
                                IsDepot       = false,
                                Latitude      = 40.524289,
                                Longitude     = -74.287035,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734",
                                Alias         = "Anthony's Pizza, 65 Church Street, Keansburg, NJ, 07734",
                                IsDepot       = false,
                                Latitude      = 40.441791,
                                Longitude     = -74.133082,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740",
                                Alias         = "Antonia's Café, 47 3rd Avenue, Long Branch, NJ, 07740",
                                IsDepot       = false,
                                Latitude      = 40.302707,
                                Longitude     = -73.987299,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407",
                                Alias         = "AP Grocery, 534 Broadway, Elmwood Park, NJ, 07407",
                                IsDepot       = false,
                                Latitude      = 40.918104,
                                Longitude     = -74.151194,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108",
                                Alias         = "Ashley Groc, 506 Clinton St, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.721587,
                                Longitude     = -74.201352,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760",
                                Alias         = "Atlantic Bagel Co, 113 E River Road, Rumson, NJ, 07760",
                                IsDepot       = false,
                                Latitude      = 40.371677,
                                Longitude     = -73.999631,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701",
                                Alias         = "Atlantic Bagel Co, 283 Route 35, Middletown, NJ, 07701",
                                IsDepot       = false,
                                Latitude      = 40.366843,
                                Longitude     = -74.08326,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738",
                                Alias         = "Atlantic Bagel Co, 642 Newman spring Rd, Lincroft, NJ, 07738",
                                IsDepot       = false,
                                Latitude      = 40.366843,
                                Longitude     = -74.08326,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732",
                                Alias         = "Atlantic Bagel Co, 74 1st Avenue, Atlantic Highlands, NJ, 07732",
                                IsDepot       = false,
                                Latitude      = 40.4138,
                                Longitude     = -74.037514,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232",
                                Alias         = "Atlantic City Fuel, 864 N Main St, Pleasantville, NJ, 08232",
                                IsDepot       = false,
                                Latitude      = 39.403741,
                                Longitude     = -74.511303,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232",
                                Alias         = "Atlantic City Gas, 8006 Black Horse Pike, Pleasantville, NJ, 08232",
                                IsDepot       = false,
                                Latitude      = 39.380853,
                                Longitude     = -74.495093,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401",
                                Alias         = "Awan Convience, 3701 Vetnor Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.351437,
                                Longitude     = -74.455519,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751",
                                Alias         = "Babes Corner, 132 Sumner Avenue, Seaside Heights, NJ, 08751",
                                IsDepot       = false,
                                Latitude      = 39.941312,
                                Longitude     = -74.074916,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879",
                                Alias         = "Bagel Mania, 347 Matawan Rd, Lawrence Harbor, NJ, 08879",
                                IsDepot       = false,
                                Latitude      = 40.430159,
                                Longitude     = -74.251723,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882",
                                Alias         = "Bagel One, 700 Old Bridge Tpke, South River, NJ, 08882",
                                IsDepot       = false,
                                Latitude      = 40.462466,
                                Longitude     = -74.402632,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bagel One, 777 Washington Road, Parlin, NJ, 08859",
                                Alias         = "Bagel One, 777 Washington Road, Parlin, NJ, 08859",
                                IsDepot       = false,
                                Latitude      = 40.462783,
                                Longitude     = -74.327999,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721",
                                Alias         = "Bagel Station, 168 Monmouth St, Red Bank, NJ, 07721",
                                IsDepot       = false,
                                Latitude      = 40.348985,
                                Longitude     = -74.073624,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504",
                                Alias         = "Barry Mini Mart, 498 12th st, Paterson, NJ, 07504",
                                IsDepot       = false,
                                Latitude      = 40.91279,
                                Longitude     = -74.138676,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055",
                                Alias         = "Best Tropical Grocery 2, 284 Verona Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.782701,
                                Longitude     = -74.166163,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748",
                                Alias         = "Bevaquas, 305 Port Monmouth Rd, Middleton, NJ, 07748",
                                IsDepot       = false,
                                Latitude      = 40.442036,
                                Longitude     = -74.116429,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859",
                                Alias         = "Bhavani, 1050 Route 9 South, Old Bridge, NJ, 08859",
                                IsDepot       = false,
                                Latitude      = 40.452799,
                                Longitude     = -74.299858,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514",
                                Alias         = "Big Hamilton Grocery, 117 Hamilton Avenue, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.920487,
                                Longitude     = -74.166298,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104",
                                Alias         = "BP Gas Station, 409 Rt 46 West, Newark, NJ, 07104",
                                IsDepot       = false,
                                Latitude      = 40.893342,
                                Longitude     = -74.107102,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "BP Gas Station, 780 Market St, Newark, NJ, 07112",
                                Alias         = "BP Gas Station, 780 Market St, Newark, NJ, 07112",
                                IsDepot       = false,
                                Latitude      = 40.905749,
                                Longitude     = -74.147813,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748",
                                Alias         = "Bray Ave Deli, 190 Bray Ave, Middletown, NJ, 07748",
                                IsDepot       = false,
                                Latitude      = 40.436711,
                                Longitude     = -74.111739,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760",
                                Alias         = "Brennans Deli, 4 W River Rd, Rumson, NJ, 07760",
                                IsDepot       = false,
                                Latitude      = 40.374892,
                                Longitude     = -74.013428,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723",
                                Alias         = "Brick Convenience, 438 Mantoloking Road, Brick, NJ, 08723",
                                IsDepot       = false,
                                Latitude      = 40.045475,
                                Longitude     = -74.094392,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055",
                                Alias         = "Brothers Produce, 327 East Railway Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.891322,
                                Longitude     = -74.149694,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831",
                                Alias         = "Brown Bag Convience, 297 Spotswood Englishtown Rd, Monroe, NJ, 08831",
                                IsDepot       = false,
                                Latitude      = 40.380837,
                                Longitude     = -74.388253,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901",
                                Alias         = "Butler Food Store, 109 Easton Avenue, New Brunswick, NJ, 08901",
                                IsDepot       = false,
                                Latitude      = 40.499122,
                                Longitude     = -74.451908,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111",
                                Alias         = "Café Columbia, 495 Mcbride Ave, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.734721,
                                Longitude     = -74.223831,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Café Sical, 56 Obert Street, South River, NJ, 08882",
                                Alias         = "Café Sical, 56 Obert Street, South River, NJ, 08882",
                                IsDepot       = false,
                                Latitude      = 40.45067,
                                Longitude     = -74.380567,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "Calis General Str, 2701 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.35569,
                                Longitude     = -74.444721,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601",
                                Alias         = "Carolyn Park Ave Groc, 76 Park Ave, Hackensack, NJ, 07601",
                                IsDepot       = false,
                                Latitude      = 40.888972,
                                Longitude     = -74.045214,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401",
                                Alias         = "Cavo Crepe, 122 North Michigan Avenue, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.361182,
                                Longitude     = -74.437285,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009",
                                Alias         = "Ccs - New Vista, 300 Broadway, Cedar Grove, NJ, 07009",
                                IsDepot       = false,
                                Latitude      = 40.76121,
                                Longitude     = -74.169224,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701",
                                Alias         = "Ccs- Fountainview, 527 River Avenue, Lakewood, NJ, 08701",
                                IsDepot       = false,
                                Latitude      = 40.074549,
                                Longitude     = -74.215903,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721",
                                Alias         = "Ccs-Tallwoods, 18 Butler Blvd, Bayville, NJ, 08721",
                                IsDepot       = false,
                                Latitude      = 39.887461,
                                Longitude     = -74.156648,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "Cedar 15, 501 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.368863,
                                Longitude     = -74.416528,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406",
                                Alias         = "Cedar Meat Market, 6407 Vetnor Avenue, Vetnor, NJ, 08406",
                                IsDepot       = false,
                                Latitude      = 39.338153,
                                Longitude     = -74.482597,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "Center City Deli, 1714 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.360264,
                                Longitude     = -74.432264,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734",
                                Alias         = "Charlie'S Deli, 164 Port Monmouth, Keansburg, NJ, 07734",
                                IsDepot       = false,
                                Latitude      = 40.441981,
                                Longitude     = -74.12276,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401",
                                Alias         = "Chikeeza, 1305 Baltic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.365509,
                                Longitude     = -74.429001,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735",
                                Alias         = "Choice Food, 182 Route 35 North, Keyport, NJ, 07735",
                                IsDepot       = false,
                                Latitude      = 40.449313,
                                Longitude     = -74.236787,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732",
                                Alias         = "Circle K, 102 Bay Avenue, Highlands, NJ, 07732",
                                IsDepot       = false,
                                Latitude      = 40.400419,
                                Longitude     = -73.984715,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757",
                                Alias         = "Circle K, 2001 Ridgeway Road, Toms River, NJ, 08757",
                                IsDepot       = false,
                                Latitude      = 40.006828,
                                Longitude     = -74.242188,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721",
                                Alias         = "Circle K, 220 Oceangate Drive, Bayville, NJ, 08721",
                                IsDepot       = false,
                                Latitude      = 39.916714,
                                Longitude     = -74.15386,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535",
                                Alias         = "Citgi Come and Go, 519 Route 33, Millstone, NJ, 08535",
                                IsDepot       = false,
                                Latitude      = 40.260143,
                                Longitude     = -74.409921,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501",
                                Alias         = "Citgo Gas Station, 473 Broadway, Paterson, NJ, 07501",
                                IsDepot       = false,
                                Latitude      = 40.918597,
                                Longitude     = -74.154093,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424",
                                Alias         = "Citrus Rest, 305 Main St, W Paterson, NJ, 07424",
                                IsDepot       = false,
                                Latitude      = 40.887559,
                                Longitude     = -74.041441,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "City Farm, 294 North 8th St, Paterson, NJ, 07501",
                                Alias         = "City Farm, 294 North 8th St, Paterson, NJ, 07501",
                                IsDepot       = false,
                                Latitude      = 40.933369,
                                Longitude     = -74.172208,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232",
                                Alias         = "City Mkt, 26 S Main St, Pleasantville, NJ, 08232",
                                IsDepot       = false,
                                Latitude      = 39.391235,
                                Longitude     = -74.522571,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055",
                                Alias         = "Clinton News, 31 Clinton Street, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.735982,
                                Longitude     = -74.169955,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025",
                                Alias         = "Collins Convience, 201 E Collins Ave, Galloway, NJ, 08025",
                                IsDepot       = false,
                                Latitude      = 39.491728,
                                Longitude     = -74.503715,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Community Deli, 546 Market St, East Orange, NJ, 07042",
                                Alias         = "Community Deli, 546 Market St, East Orange, NJ, 07042",
                                IsDepot       = false,
                                Latitude      = 40.911747,
                                Longitude     = -74.155516,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764",
                                Alias         = "Convenience Corner, 355 Monmouth Road, West Long Branch, NJ, 07764",
                                IsDepot       = false,
                                Latitude      = 40.2842,
                                Longitude     = -74.02012,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514",
                                Alias         = "Correita El Paisa, 326 21st Ave, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.906704,
                                Longitude     = -74.158671,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504",
                                Alias         = "Cositas Ricas, 535 21st Ave, Paterson, NJ, 07504",
                                IsDepot       = false,
                                Latitude      = 40.90601,
                                Longitude     = -74.150362,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750",
                                Alias         = "Country Farm, 1320 Seagirt Avenue, Seagirt, NJ, 08750",
                                IsDepot       = false,
                                Latitude      = 40.135683,
                                Longitude     = -74.062333,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720",
                                Alias         = "Country Farms, 125 Main Street, Bradley Beach, NJ, 07720",
                                IsDepot       = false,
                                Latitude      = 40.200035,
                                Longitude     = -74.019095,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234",
                                Alias         = "Country Farms, 2588 Tilton Rd, Egg Harbor, NJ, 08234",
                                IsDepot       = false,
                                Latitude      = 39.416868,
                                Longitude     = -74.569141,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742",
                                Alias         = "Country Farms, 3122 Route 88, Point Pleasant, NJ, 08742",
                                IsDepot       = false,
                                Latitude      = 40.079909,
                                Longitude     = -74.083889,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753",
                                Alias         = "Country Farms, 892 Fisher Blvd, Toms River, NJ, 08753",
                                IsDepot       = false,
                                Latitude      = 39.973935,
                                Longitude     = -74.137087,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721",
                                Alias         = "Country Food Market, 921 Atlantic City Blvd, Bayville, NJ, 08721",
                                IsDepot       = false,
                                Latitude      = 39.882705,
                                Longitude     = -74.164435,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726",
                                Alias         = "Country Store Raceway, 454 Rt 33 West, Millstone, NJ, 07726",
                                IsDepot       = false,
                                Latitude      = 40.258843,
                                Longitude     = -74.398019,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751",
                                Alias         = "Crossroads Deli, 479 Route 79, Morganville, NJ, 07751",
                                IsDepot       = false,
                                Latitude      = 40.383938,
                                Longitude     = -74.241525,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859",
                                Alias         = "Crystal Express Deli, 308 Ernston Road, Parlin, NJ, 08859",
                                IsDepot       = false,
                                Latitude      = 40.458048,
                                Longitude     = -74.305937,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831",
                                Alias         = "Crystal Kitchen, 1600 1600 Perrinville Rd, Monroe, NJ, 08831",
                                IsDepot       = false,
                                Latitude      = 40.316134,
                                Longitude     = -74.440031,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723",
                                Alias         = "Deal Food, 112 Norwood Ave, Deal, NJ, 67723",
                                IsDepot       = false,
                                Latitude      = 40.253485,
                                Longitude     = -74.000852,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055",
                                Alias         = "Dehuit Market, 70 Market Street, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.863711,
                                Longitude     = -74.116357,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701",
                                Alias         = "Delta Gas, 100 Madison Avenue, Lakewood, NJ, 08701",
                                IsDepot       = false,
                                Latitude      = 40.091107,
                                Longitude     = -74.216751,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401",
                                Alias         = "Demarcos, 3809 Crossan Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.358413,
                                Longitude     = -74.462155,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050",
                                Alias         = "Dios Fe Ezperanza, 548 Market St, Orange, NJ, 07050",
                                IsDepot       = false,
                                Latitude      = 40.768005,
                                Longitude     = -74.232605,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Dollar Variety, 292 Main St, Paterson, NJ, 07502",
                                Alias         = "Dollar Variety, 292 Main St, Paterson, NJ, 07502",
                                IsDepot       = false,
                                Latitude      = 40.915152,
                                Longitude     = -74.173859,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                                Alias         = "Doms Cherry Street Deli, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                                IsDepot       = false,
                                Latitude      = 40.332559,
                                Longitude     = -74.074423,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712",
                                Alias         = "Doniele Lotz, 206-220 First Ave, Asbury Park, NJ, 07712",
                                IsDepot       = false,
                                Latitude      = 40.219227,
                                Longitude     = -74.003708,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401",
                                Alias         = "Dover Market, 3920 Vetnor Avenue, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.349847,
                                Longitude     = -74.457832,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513",
                                Alias         = "Dunkin Donuts 117-02, 545 Chancellor Ave, Paterson, NJ, 07513",
                                IsDepot       = false,
                                Latitude      = 40.713875,
                                Longitude     = -74.229677,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "El Apache, 44 East Front Street, Keyport, NJ, 07735",
                                Alias         = "El Apache, 44 East Front Street, Keyport, NJ, 07735",
                                IsDepot       = false,
                                Latitude      = 40.438094,
                                Longitude     = -74.199867,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "El Bohio, 510 Park Ave, Paterson, NJ, 07504",
                                Alias         = "El Bohio, 510 Park Ave, Paterson, NJ, 07504",
                                IsDepot       = false,
                                Latitude      = 40.913352,
                                Longitude     = -74.143493,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055",
                                Alias         = "El Colmado Supermarket, 126 Hope Street, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.867712,
                                Longitude     = -74.122705,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "El Paisa, 471 21st Ave, Irvington, NJ, 07111",
                                Alias         = "El Paisa, 471 21st Ave, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.906332,
                                Longitude     = -74.153318,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753",
                                Alias         = "El Poblano Deli & Grocery, 1241 Lakewood Rd, Toms River, NJ, 08753",
                                IsDepot       = false,
                                Latitude      = 39.985037,
                                Longitude     = -74.20969,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514",
                                Alias         = "El Triangulo, 156 Hawthorne Ave, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.949274,
                                Longitude     = -74.149605,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108",
                                Alias         = "Eliany Groc, 146 Sherman Ave, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.720118,
                                Longitude     = -74.186768,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Eli's Bagels, 1055 Route 34 North, Matawan, NJ, 07747",
                                Alias         = "Eli's Bagels, 1055 Route 34 North, Matawan, NJ, 07747",
                                IsDepot       = false,
                                Latitude      = 40.401578,
                                Longitude     = -74.228494,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Era Min Mart, 291 Clinton Place, Newark, NJ, 07105",
                                Alias         = "Era Min Mart, 291 Clinton Place, Newark, NJ, 07105",
                                IsDepot       = false,
                                Latitude      = 40.713666,
                                Longitude     = -74.214332,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Essex County Hospital Center, 204 Grove St, Irvington, NJ, 07111",
                                Alias         = "Essex County Hospital Center, 204 Grove St, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.851854,
                                Longitude     = -74.234064,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Exxon Mart, 3164  Highway 88, Point Pleasant, NJ, 08742",
                                Alias         = "Exxon Mart, 3164  Highway 88, Point Pleasant, NJ, 08742",
                                IsDepot       = false,
                                Latitude      = 40.079245,
                                Longitude     = -74.087066,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Exxon, 200 Rt 46 West, Passaic, NJ, 07055",
                                Alias         = "Exxon, 200 Rt 46 West, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.872671,
                                Longitude     = -74.192393,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Exxon, 70 US Rt 9 North, Morganville, NJ, 07751",
                                Alias         = "Exxon, 70 US Rt 9 North, Morganville, NJ, 07751",
                                IsDepot       = false,
                                Latitude      = 40.353539,
                                Longitude     = -74.306081,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ez Check, 1015 N Wood Ave, Linden, NJ, 07036",
                                Alias         = "Ez Check, 1015 N Wood Ave, Linden, NJ, 07036",
                                IsDepot       = false,
                                Latitude      = 40.637435,
                                Longitude     = -74.265105,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ez Check, 80 Main St, Sayerville, NJ, 08872",
                                Alias         = "Ez Check, 80 Main St, Sayerville, NJ, 08872",
                                IsDepot       = false,
                                Latitude      = 40.460145,
                                Longitude     = -74.360907,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "F & L Groc, 133 Parker Street, Passaic, NJ, 07055",
                                Alias         = "F & L Groc, 133 Parker Street, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.872287,
                                Longitude     = -74.12229,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Famous Deli, 400 N Massachusetss Ave, Atlantic City, NJ, 08401",
                                Alias         = "Famous Deli, 400 N Massachusetss Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.372044,
                                Longitude     = -74.421096,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Fatima, 222 Park Ave, Paterson, NJ, 07514",
                                Alias         = "Fatima, 222 Park Ave, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.914935,
                                Longitude     = -74.156819,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "First Cup, 96 First ave, Atlantic Highlands, NJ, 07716",
                                Alias         = "First Cup, 96 First ave, Atlantic Highlands, NJ, 07716",
                                IsDepot       = false,
                                Latitude      = 40.413219,
                                Longitude     = -74.037804,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Five 2 Nine, 241 Highway 36 N, Hazlet, NJ, 07730",
                                Alias         = "Five 2 Nine, 241 Highway 36 N, Hazlet, NJ, 07730",
                                IsDepot       = false,
                                Latitude      = 40.437628,
                                Longitude     = -74.141357,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Flagship, 60 N Main Ave, Atlantic City, NJ, 08401",
                                Alias         = "Flagship, 60 N Main Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.369874,
                                Longitude     = -74.412611,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Florida Grocery, 2501 Artic Ave, Atlantic City, NJ, 08401",
                                Alias         = "Florida Grocery, 2501 Artic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.358182,
                                Longitude     = -74.443172,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "G.A.P., 620 15th Avenue, Paterson, NJ, 07501",
                                Alias         = "G.A.P., 620 15th Avenue, Paterson, NJ, 07501",
                                IsDepot       = false,
                                Latitude      = 40.9144,
                                Longitude     = -74.140202,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "George Street Ale House, 378 George street, New Brunswick, NJ, 08901",
                                Alias         = "George Street Ale House, 378 George street, New Brunswick, NJ, 08901",
                                IsDepot       = false,
                                Latitude      = 40.495678,
                                Longitude     = -74.444192,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Getty Mart, 1940 St Rt 34, Wall, NJ, 07719",
                                Alias         = "Getty Mart, 1940 St Rt 34, Wall, NJ, 07719",
                                IsDepot       = false,
                                Latitude      = 40.158248,
                                Longitude     = -74.099694,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Glenny Groc, 237 Roseland Ave, East Orange, NJ, 07018",
                                Alias         = "Glenny Groc, 237 Roseland Ave, East Orange, NJ, 07018",
                                IsDepot       = false,
                                Latitude      = 40.831038,
                                Longitude     = -74.283425,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Golden Years Adult Day Care, 108 Woodward Rd, Manalapan, NJ, 07726",
                                Alias         = "Golden Years Adult Day Care, 108 Woodward Rd, Manalapan, NJ, 07726",
                                IsDepot       = false,
                                Latitude      = 40.249947,
                                Longitude     = -74.366984,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Good Neighbor Mini Mkt, 918 Lee Avenue, New Brunswick, NJ, 08902",
                                Alias         = "Good Neighbor Mini Mkt, 918 Lee Avenue, New Brunswick, NJ, 08902",
                                IsDepot       = false,
                                Latitude      = 40.473348,
                                Longitude     = -74.461946,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Grandma's Bagels, 479 Prospect Ave, Little Silver, NJ, 07739",
                                Alias         = "Grandma's Bagels, 479 Prospect Ave, Little Silver, NJ, 07739",
                                IsDepot       = false,
                                Latitude      = 40.339464,
                                Longitude     = -74.042152,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Green Leaf, 870 Springfield Ave, Newark, NJ, 07104",
                                Alias         = "Green Leaf, 870 Springfield Ave, Newark, NJ, 07104",
                                IsDepot       = false,
                                Latitude      = 40.728082,
                                Longitude     = -74.22216,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Grover Groc, 509 Grove St, Irvington, NJ, 07111",
                                Alias         = "Grover Groc, 509 Grove St, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.739888,
                                Longitude     = -74.213131,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Hazlet Shell, 1355 Rt 36, Hazlet, NJ, 07730",
                                Alias         = "Hazlet Shell, 1355 Rt 36, Hazlet, NJ, 07730",
                                IsDepot       = false,
                                Latitude      = 40.429843,
                                Longitude     = -74.188241,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Hole Lot of Bagels, 1171 Route 35, Middletown, NJ, 07748",
                                Alias         = "Hole Lot of Bagels, 1171 Route 35, Middletown, NJ, 07748",
                                IsDepot       = false,
                                Latitude      = 40.39891,
                                Longitude     = -74.111004,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Hong Kong Supermarket, 275 Rt 18 South, East Brunswick, NJ, 08816",
                                Alias         = "Hong Kong Supermarket, 275 Rt 18 South, East Brunswick, NJ, 08816",
                                IsDepot       = false,
                                Latitude      = 40.459219,
                                Longitude     = -74.404777,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Hudson Manor, 40 Hudson Street, Old Bridge, NJ, 07728",
                                Alias         = "Hudson Manor, 40 Hudson Street, Old Bridge, NJ, 07728",
                                IsDepot       = false,
                                Latitude      = 40.26011,
                                Longitude     = -74.270311,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "I&K Conv Deli, 3109 Bordontown Avenue, Parlin, NJ, 08859",
                                Alias         = "I&K Conv Deli, 3109 Bordontown Avenue, Parlin, NJ, 08859",
                                IsDepot       = false,
                                Latitude      = 40.450615,
                                Longitude     = -74.314199,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Indiana Chicken, 501 N Indiana, Atlantic City, NJ, 08401",
                                Alias         = "Indiana Chicken, 501 N Indiana, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.366317,
                                Longitude     = -74.437075,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "International Supermarket, 128 Ocean Ave, Lakewood, NJ, 08701",
                                Alias         = "International Supermarket, 128 Ocean Ave, Lakewood, NJ, 08701",
                                IsDepot       = false,
                                Latitude      = 40.090073,
                                Longitude     = -74.209407,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Italian Delight, 226 Adephia Road, Howell, NJ, 07737",
                                Alias         = "Italian Delight, 226 Adephia Road, Howell, NJ, 07737",
                                IsDepot       = false,
                                Latitude      = 40.20265,
                                Longitude     = -74.196459,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jackies Deli, 3201 Fairmount Avenue, Atlantic City, NJ, 08401",
                                Alias         = "Jackies Deli, 3201 Fairmount Avenue, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.356058,
                                Longitude     = -74.452228,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jays Food Market, 358 Herbertsville Road, Brick, NJ, 08724",
                                Alias         = "Jays Food Market, 358 Herbertsville Road, Brick, NJ, 08724",
                                IsDepot       = false,
                                Latitude      = 40.097688,
                                Longitude     = -74.10064,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jersey Pride Food Mart, 18 Snowhill Street, Spotswood, NJ, 08884",
                                Alias         = "Jersey Pride Food Mart, 18 Snowhill Street, Spotswood, NJ, 08884",
                                IsDepot       = false,
                                Latitude      = 40.391643,
                                Longitude     = -74.39227,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jersey Shore Coffee, 64 Thompson Avenue, Leonardo, NJ, 07737",
                                Alias         = "Jersey Shore Coffee, 64 Thompson Avenue, Leonardo, NJ, 07737",
                                IsDepot       = false,
                                Latitude      = 40.414913,
                                Longitude     = -74.058999,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jersey Shore Deli, 331 Main Street, West Creek, NJ, 08092",
                                Alias         = "Jersey Shore Deli, 331 Main Street, West Creek, NJ, 08092",
                                IsDepot       = false,
                                Latitude      = 39.666109,
                                Longitude     = -74.280843,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jersey Shore Deli, 613 Hope Road, Eatontown, NJ, 07724",
                                Alias         = "Jersey Shore Deli, 613 Hope Road, Eatontown, NJ, 07724",
                                IsDepot       = false,
                                Latitude      = 40.289643,
                                Longitude     = -74.078431,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jiminez Groc, 310 14th Ave, Newark, NJ, 07103",
                                Alias         = "Jiminez Groc, 310 14th Ave, Newark, NJ, 07103",
                                IsDepot       = false,
                                Latitude      = 40.740142,
                                Longitude     = -74.206301,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jimmy Leeds, 68 W Jimmie Leeds Rd, Galloway, NJ, 08025",
                                Alias         = "Jimmy Leeds, 68 W Jimmie Leeds Rd, Galloway, NJ, 08025",
                                IsDepot       = false,
                                Latitude      = 39.47564,
                                Longitude     = -74.541319,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jj Groc, 246 Summer St, Passaic, NJ, 07055",
                                Alias         = "Jj Groc, 246 Summer St, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.867849,
                                Longitude     = -74.13855,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Joes Food Mkt, 401 N Dr Martin King Blvd, Atlantic City, NJ, 08401",
                                Alias         = "Joes Food Mkt, 401 N Dr Martin King Blvd, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.366221,
                                Longitude     = -74.435404,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jose Mini Market, 75 3rd Street, Newark, NJ, 07104",
                                Alias         = "Jose Mini Market, 75 3rd Street, Newark, NJ, 07104",
                                IsDepot       = false,
                                Latitude      = 40.743912,
                                Longitude     = -74.196031,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jose Spmkt, 219 Tremont Ave, Orange, NJ, 07050",
                                Alias         = "Jose Spmkt, 219 Tremont Ave, Orange, NJ, 07050",
                                IsDepot       = false,
                                Latitude      = 40.751429,
                                Longitude     = -74.227549,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Jr Freeway, 301 Osbourne, Newark, NJ, 07108",
                                Alias         = "Jr Freeway, 301 Osbourne, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.712976,
                                Longitude     = -74.210091,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Juquila Mexican Groc, 100 Lee Ave, New Brunswick, NJ, 08901",
                                Alias         = "Juquila Mexican Groc, 100 Lee Ave, New Brunswick, NJ, 08901",
                                IsDepot       = false,
                                Latitude      = 40.486612,
                                Longitude     = -74.448257,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Keyport News, 37 W Front St, Keyport, NJ, 07735",
                                Alias         = "Keyport News, 37 W Front St, Keyport, NJ, 07735",
                                IsDepot       = false,
                                Latitude      = 40.437284,
                                Longitude     = -74.203406,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Kings Highway Glatt, 250 Norwood Avenue, Oakhurst, NJ, 07755",
                                Alias         = "Kings Highway Glatt, 250 Norwood Avenue, Oakhurst, NJ, 07755",
                                IsDepot       = false,
                                Latitude      = 40.25995,
                                Longitude     = -74.000243,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Kpp Grocery, 108 William St, Newark, NJ, 07107",
                                Alias         = "Kpp Grocery, 108 William St, Newark, NJ, 07107",
                                IsDepot       = false,
                                Latitude      = 40.752345,
                                Longitude     = -74.187634,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krausers, 1227 Halsey St, Newark, NJ, 07103",
                                Alias         = "Krausers, 1227 Halsey St, Newark, NJ, 07103",
                                IsDepot       = false,
                                Latitude      = 40.734446,
                                Longitude     = -74.174573,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krausers, 200 North Broadway, South Amboy, NJ, 08879",
                                Alias         = "Krausers, 200 North Broadway, South Amboy, NJ, 08879",
                                IsDepot       = false,
                                Latitude      = 40.485749,
                                Longitude     = -74.283706,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krausers, 595 Palmer Avenue, Hazlet, NJ, 07734",
                                Alias         = "Krausers, 595 Palmer Avenue, Hazlet, NJ, 07734",
                                IsDepot       = false,
                                Latitude      = 40.429671,
                                Longitude     = -74.132774,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krauszer's Food Store, 3193 Washington Ave, Parlin, NJ, 08859",
                                Alias         = "Krauszer's Food Store, 3193 Washington Ave, Parlin, NJ, 08859",
                                IsDepot       = false,
                                Latitude      = 40.468223,
                                Longitude     = -74.306587,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krauzer Foods, 58 Jackson Street, South River, NJ, 08882",
                                Alias         = "Krauzer Foods, 58 Jackson Street, South River, NJ, 08882",
                                IsDepot       = false,
                                Latitude      = 40.450583,
                                Longitude     = -74.382182,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krauzers Food Market, 546 Park Avenue, Freehold, NJ, 07728",
                                Alias         = "Krauzers Food Market, 546 Park Avenue, Freehold, NJ, 07728",
                                IsDepot       = false,
                                Latitude      = 40.249606,
                                Longitude     = -74.271902,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krauzers South River, 81 Main Street, South River, NJ, 08882",
                                Alias         = "Krauzers South River, 81 Main Street, South River, NJ, 08882",
                                IsDepot       = false,
                                Latitude      = 40.450583,
                                Longitude     = -74.382182,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Krauzers, 309 Main Street, Sayerville, NJ, 08872",
                                Alias         = "Krauzers, 309 Main Street, Sayerville, NJ, 08872",
                                IsDepot       = false,
                                Latitude      = 40.470359,
                                Longitude     = -74.359202,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Kushira, 22-24 Paterson Ave, Newark, NJ, 07105",
                                Alias         = "Kushira, 22-24 Paterson Ave, Newark, NJ, 07105",
                                IsDepot       = false,
                                Latitude      = 40.723126,
                                Longitude     = -74.141612,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Kwik Farms, 590 Shrewsbury Ave, Tinton Falls, NJ, 07724",
                                Alias         = "Kwik Farms, 590 Shrewsbury Ave, Tinton Falls, NJ, 07724",
                                IsDepot       = false,
                                Latitude      = 40.330025,
                                Longitude     = -74.074258,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Bagel #3, 377 Georges Street, New Brunswick, NJ, 08776",
                                Alias         = "La Bagel #3, 377 Georges Street, New Brunswick, NJ, 08776",
                                IsDepot       = false,
                                Latitude      = 40.495696,
                                Longitude     = -74.443867,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La China Poblana, 114-116 Shrewbury Avenue, Red Bank, NJ, 07701",
                                Alias         = "La China Poblana, 114-116 Shrewbury Avenue, Red Bank, NJ, 07701",
                                IsDepot       = false,
                                Latitude      = 40.346942,
                                Longitude     = -74.076597,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Chiquita, 36 Astor St, Newark, NJ, 07108",
                                Alias         = "La Chiquita, 36 Astor St, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.722866,
                                Longitude     = -74.183561,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Esperamza Food Market, 279 Ellis Avenue, Newark, NJ, 07103",
                                Alias         = "La Esperamza Food Market, 279 Ellis Avenue, Newark, NJ, 07103",
                                IsDepot       = false,
                                Latitude      = 40.756185,
                                Longitude     = -74.17351,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Mich Oceana, 109 Taylor Ave, Manasquan, NJ, 08736",
                                Alias         = "La Mich Oceana, 109 Taylor Ave, Manasquan, NJ, 08736",
                                IsDepot       = false,
                                Latitude      = 40.124934,
                                Longitude     = -74.046072,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Nany, 356 Union Ave, Paterson, NJ, 07503",
                                Alias         = "La Nany, 356 Union Ave, Paterson, NJ, 07503",
                                IsDepot       = false,
                                Latitude      = 40.919728,
                                Longitude     = -74.187345,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Palma Villa Grocery 1, 18 Broad Street, Freehold, NJ, 07728",
                                Alias         = "La Palma Villa Grocery 1, 18 Broad Street, Freehold, NJ, 07728",
                                IsDepot       = false,
                                Latitude      = 40.259859,
                                Longitude     = -74.278887,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Posada, 1055 Main Ave, Passaic, NJ, 07055",
                                Alias         = "La Posada, 1055 Main Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.872648,
                                Longitude     = -74.137303,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "La Tipica Grocery, 2500 Artic Avenue, Atlantic City, NJ, 08401",
                                Alias         = "La Tipica Grocery, 2500 Artic Avenue, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.357862,
                                Longitude     = -74.442969,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Las Paisas, 143 Broadway, Prospect Park, NJ, 07508",
                                Alias         = "Las Paisas, 143 Broadway, Prospect Park, NJ, 07508",
                                IsDepot       = false,
                                Latitude      = 40.858621,
                                Longitude     = -74.130554,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Las Placitas Mexicana, 317 Handy St, New Brunswick, NJ, 08070",
                                Alias         = "Las Placitas Mexicana, 317 Handy St, New Brunswick, NJ, 08070",
                                IsDepot       = false,
                                Latitude      = 40.490362,
                                Longitude     = -74.452509,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Latino Groc, 752 River St, Paterson, NJ, 07524",
                                Alias         = "Latino Groc, 752 River St, Paterson, NJ, 07524",
                                IsDepot       = false,
                                Latitude      = 40.93793,
                                Longitude     = -74.151234,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Linar, 162 Monmouth St, Red Bank, NJ, 07701",
                                Alias         = "Linar, 162 Monmouth St, Red Bank, NJ, 07701",
                                IsDepot       = false,
                                Latitude      = 40.349033,
                                Longitude     = -74.073362,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Lincroft Senior Center, 41 Hurley Lane, Lincroft, NJ, 07758",
                                Alias         = "Lincroft Senior Center, 41 Hurley Lane, Lincroft, NJ, 07758",
                                IsDepot       = false,
                                Latitude      = 40.33218,
                                Longitude     = -74.124581,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Long Branch Deli, 156 Long Branch Avenue, Long Branch, NJ, 07740",
                                Alias         = "Long Branch Deli, 156 Long Branch Avenue, Long Branch, NJ, 07740",
                                IsDepot       = false,
                                Latitude      = 40.31097,
                                Longitude     = -73.984022,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Lopez Spmkt, 995 Bergen St, Newark, NJ, 07108",
                                Alias         = "Lopez Spmkt, 995 Bergen St, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.709927,
                                Longitude     = -74.206793,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ls Deli, 175 Elizabeth Ave, Newark, NJ, 07108",
                                Alias         = "Ls Deli, 175 Elizabeth Ave, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.717116,
                                Longitude     = -74.190947,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Lucky 7 Deli, 1017 Rt 36, Union Beach, NJ, 07735",
                                Alias         = "Lucky 7 Deli, 1017 Rt 36, Union Beach, NJ, 07735",
                                IsDepot       = false,
                                Latitude      = 40.438336,
                                Longitude     = -74.163793,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Lucky Superstore, 715 Main Street, Asbury Park, NJ, 07712",
                                Alias         = "Lucky Superstore, 715 Main Street, Asbury Park, NJ, 07712",
                                IsDepot       = false,
                                Latitude      = 40.220269,
                                Longitude     = -74.012208,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Luisa Deli & Groc, 123 Elizabeth Ave, Newark, NJ, 07108",
                                Alias         = "Luisa Deli & Groc, 123 Elizabeth Ave, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.718769,
                                Longitude     = -74.190058,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Lymensada Mini Mart, 132 East 4th Street, Lakewood, NJ, 08701",
                                Alias         = "Lymensada Mini Mart, 132 East 4th Street, Lakewood, NJ, 08701",
                                IsDepot       = false,
                                Latitude      = 40.094515,
                                Longitude     = -74.206256,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Madison Deli, 69 Route 34, South Amboy, NJ, 08831",
                                Alias         = "Madison Deli, 69 Route 34, South Amboy, NJ, 08831",
                                IsDepot       = false,
                                Latitude      = 40.432301,
                                Longitude     = -74.297294,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Manhattan Bagel, 720 Newman Springs Rd, Tinton Falls, NJ, 07738",
                                Alias         = "Manhattan Bagel, 720 Newman Springs Rd, Tinton Falls, NJ, 07738",
                                IsDepot       = false,
                                Latitude      = 40.331405,
                                Longitude     = -74.123225,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Manhattan Bagels, 881 Main Street, Sayerville, NJ, 08872",
                                Alias         = "Manhattan Bagels, 881 Main Street, Sayerville, NJ, 08872",
                                IsDepot       = false,
                                Latitude      = 40.476242,
                                Longitude     = -74.31866,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Maywood Mkt, 74 West Pleasant Ave, Hackensack, NJ, 07601",
                                Alias         = "Maywood Mkt, 74 West Pleasant Ave, Hackensack, NJ, 07601",
                                IsDepot       = false,
                                Latitude      = 40.904762,
                                Longitude     = -74.063701,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Mcbride Conv, 500 Mcbride Ave, Paterson, NJ, 07513",
                                Alias         = "Mcbride Conv, 500 Mcbride Ave, Paterson, NJ, 07513",
                                IsDepot       = false,
                                Latitude      = 40.907133,
                                Longitude     = -74.195232,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "McKinley Convenience, 100 McKinley Convenience, Manahawkin, NJ, 08050",
                                Alias         = "McKinley Convenience, 100 McKinley Convenience, Manahawkin, NJ, 08050",
                                IsDepot       = false,
                                Latitude      = 39.69202,
                                Longitude     = -74.268679,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Mejias Grc, 164 Madison Ave, Passaic, NJ, 07055",
                                Alias         = "Mejias Grc, 164 Madison Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.864086,
                                Longitude     = -74.124176,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Mendoker Quality Bakery, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                                Alias         = "Mendoker Quality Bakery, 530 Shrewsbury Avenue, Tinton Falls, NJ, 07701",
                                IsDepot       = false,
                                Latitude      = 40.348865,
                                Longitude     = -74.437009,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Metropan, 420 21st Ave, Paterson, NJ, 07514",
                                Alias         = "Metropan, 420 21st Ave, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.906114,
                                Longitude     = -74.154414,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Mi Casa, 67 E South St, Freehold, NJ, 07728",
                                Alias         = "Mi Casa, 67 E South St, Freehold, NJ, 07728",
                                IsDepot       = false,
                                Latitude      = 40.256763,
                                Longitude     = -74.273335,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Mid Shore Meats, 801 Fisher Blvd, Toms River, NJ, 08753",
                                Alias         = "Mid Shore Meats, 801 Fisher Blvd, Toms River, NJ, 08753",
                                IsDepot       = false,
                                Latitude      = 39.967992,
                                Longitude     = -74.131572,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Millstone Food Market, 673 Route 33, Millstone, NJ, 08535",
                                Alias         = "Millstone Food Market, 673 Route 33, Millstone, NJ, 08535",
                                IsDepot       = false,
                                Latitude      = 40.261439,
                                Longitude     = -74.435975,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Minute Mart, 148 White Head Ave, South River, NJ, 08882",
                                Alias         = "Minute Mart, 148 White Head Ave, South River, NJ, 08882",
                                IsDepot       = false,
                                Latitude      = 40.447007,
                                Longitude     = -74.373784,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Mm Groc, 1272 Springfield Ave, Passaic, NJ, 07055",
                                Alias         = "Mm Groc, 1272 Springfield Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.724698,
                                Longitude     = -74.241679,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Moosas Market, 230 N. New Jersey Ave, Atlantic City, NJ, 08401",
                                Alias         = "Moosas Market, 230 N. New Jersey Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.369259,
                                Longitude     = -74.421825,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Morell & Cepeda, 315 21st Street, Paterson, NJ, 07514",
                                Alias         = "Morell & Cepeda, 315 21st Street, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.907441,
                                Longitude     = -74.16033,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Morganville Deli & Liquor, 172 Tenant Road, Morganville, NJ, 07751",
                                Alias         = "Morganville Deli & Liquor, 172 Tenant Road, Morganville, NJ, 07751",
                                IsDepot       = false,
                                Latitude      = 40.368815,
                                Longitude     = -74.264081,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Munchies, 314 Sylvania Ave, Neptune, NJ, 07753",
                                Alias         = "Munchies, 314 Sylvania Ave, Neptune, NJ, 07753",
                                IsDepot       = false,
                                Latitude      = 40.205113,
                                Longitude     = -74.045022,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "My Placita, 204 Dayron, Paterson, NJ, 07504",
                                Alias         = "My Placita, 204 Dayron, Paterson, NJ, 07504",
                                IsDepot       = false,
                                Latitude      = 40.91279,
                                Longitude     = -74.138676,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Nashville Market, 5101 Vetnor Ave, Vetnor City, NJ, 08406",
                                Alias         = "Nashville Market, 5101 Vetnor Ave, Vetnor City, NJ, 08406",
                                IsDepot       = false,
                                Latitude      = 39.344312,
                                Longitude     = -74.469856,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Natures Reward, 3124 Bridge Ave, Point Pleasant, NJ, 08742",
                                Alias         = "Natures Reward, 3124 Bridge Ave, Point Pleasant, NJ, 08742",
                                IsDepot       = false,
                                Latitude      = 40.076345,
                                Longitude     = -74.085964,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Neptune Deli, 1-211 Route 35 South, Neptune City, NJ, 07753",
                                Alias         = "Neptune Deli, 1-211 Route 35 South, Neptune City, NJ, 07753",
                                IsDepot       = false,
                                Latitude      = 40.214545,
                                Longitude     = -74.030095,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "New Bergen Food, 943 Bergen St, Elizabeth, NJ, 07206",
                                Alias         = "New Bergen Food, 943 Bergen St, Elizabeth, NJ, 07206",
                                IsDepot       = false,
                                Latitude      = 40.653461,
                                Longitude     = -74.197261,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "New City Grocery, 2608 Pacific Ave, Atlantic City, NJ, 08401",
                                Alias         = "New City Grocery, 2608 Pacific Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.354239,
                                Longitude     = -74.442334,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "New Farmers Market, 2732 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "New Farmers Market, 2732 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.354931,
                                Longitude     = -74.445481,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "New Latin Deli, 3623 Winchester Ave, Atlantic City, NJ, 08401",
                                Alias         = "New Latin Deli, 3623 Winchester Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.35306,
                                Longitude     = -74.455537,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "New York Deli, 649 N. New York avenue, Atlantic City, NJ, 08401",
                                Alias         = "New York Deli, 649 N. New York avenue, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.369198,
                                Longitude     = -74.434158,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Nicasias, 253 Main St, Keansburg, NJ, 07734",
                                Alias         = "Nicasias, 253 Main St, Keansburg, NJ, 07734",
                                IsDepot       = false,
                                Latitude      = 40.443224,
                                Longitude     = -74.129878,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Nohelis Groc, 364 5th Ave, Newark, NJ, 07112",
                                Alias         = "Nohelis Groc, 364 5th Ave, Newark, NJ, 07112",
                                IsDepot       = false,
                                Latitude      = 40.760559,
                                Longitude     = -74.18531,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Nouri Bros, 999 Main St, Paterson, NJ, 07503",
                                Alias         = "Nouri Bros, 999 Main St, Paterson, NJ, 07503",
                                IsDepot       = false,
                                Latitude      = 40.894001,
                                Longitude     = -74.158519,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Noya Bazaar, 139 Wayne Ave, Paterson, NJ, 07505",
                                Alias         = "Noya Bazaar, 139 Wayne Ave, Paterson, NJ, 07505",
                                IsDepot       = false,
                                Latitude      = 40.919132,
                                Longitude     = -74.186458,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Oakhill Deli, 78 South Street, Freehold, NJ, 07728",
                                Alias         = "Oakhill Deli, 78 South Street, Freehold, NJ, 07728",
                                IsDepot       = false,
                                Latitude      = 40.2566,
                                Longitude     = -74.273895,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ocean Bay Diner, 1803 Route 35 South, Sayerville, NJ, 08872",
                                Alias         = "Ocean Bay Diner, 1803 Route 35 South, Sayerville, NJ, 08872",
                                IsDepot       = false,
                                Latitude      = 40.464455,
                                Longitude     = -74.267104,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ocean County Correctional Facility, 114 Hooper Ave, Toms River, NJ, 08753",
                                Alias         = "Ocean County Correctional Facility, 114 Hooper Ave, Toms River, NJ, 08753",
                                IsDepot       = false,
                                Latitude      = 39.953836,
                                Longitude     = -74.194426,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ocean Gate Market, 216 Ocean Gate Market, Ocean Gate, NJ, 08740",
                                Alias         = "Ocean Gate Market, 216 Ocean Gate Market, Ocean Gate, NJ, 08740",
                                IsDepot       = false,
                                Latitude      = 39.928493,
                                Longitude     = -74.140786,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "One Stop Deli, 1826 Rt 35 North, Sayerville, NJ, 08872",
                                Alias         = "One Stop Deli, 1826 Rt 35 North, Sayerville, NJ, 08872",
                                IsDepot       = false,
                                Latitude      = 40.465032,
                                Longitude     = -74.267977,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "One Stop, 450 Rt 35 N, Neptune, NJ, 07753",
                                Alias         = "One Stop, 450 Rt 35 N, Neptune, NJ, 07753",
                                IsDepot       = false,
                                Latitude      = 40.219447,
                                Longitude     = -74.032187,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Original Quality Market, 416 11th Avenue, East Orange, NJ, 07017",
                                Alias         = "Original Quality Market, 416 11th Avenue, East Orange, NJ, 07017",
                                IsDepot       = false,
                                Latitude      = 40.921862,
                                Longitude     = -74.150214,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pacific Mini, 2610 Pacific Ave, Atlantic City, NJ, 08401",
                                Alias         = "Pacific Mini, 2610 Pacific Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.35422,
                                Longitude     = -74.442381,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Patel Foods, 989 Route 9 North, Parlin, NJ, 08859",
                                Alias         = "Patel Foods, 989 Route 9 North, Parlin, NJ, 08859",
                                IsDepot       = false,
                                Latitude      = 40.455067,
                                Longitude     = -74.295686,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pathway Market, 42 Pilgrim pathway, Ocean Grove, NJ, 07756",
                                Alias         = "Pathway Market, 42 Pilgrim pathway, Ocean Grove, NJ, 07756",
                                IsDepot       = false,
                                Latitude      = 40.212786,
                                Longitude     = -74.00703,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pats Mkt, 677 Newman Springs Rd, Lincroft, NJ, 07738",
                                Alias         = "Pats Mkt, 677 Newman Springs Rd, Lincroft, NJ, 07738",
                                IsDepot       = false,
                                Latitude      = 40.330632,
                                Longitude     = -74.119752,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pena And Morei, 307 Broadway, Heldon, NJ, 07508",
                                Alias         = "Pena And Morei, 307 Broadway, Heldon, NJ, 07508",
                                IsDepot       = false,
                                Latitude      = 40.956753,
                                Longitude     = -74.188582,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Picks Deli, 1500 Main Street, Belmar, NJ, 07719",
                                Alias         = "Picks Deli, 1500 Main Street, Belmar, NJ, 07719",
                                IsDepot       = false,
                                Latitude      = 40.175265,
                                Longitude     = -74.026946,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pinebrook Liquors & Deli, 1870 Wayside Road, Tinton Falls, NJ, 07724",
                                Alias         = "Pinebrook Liquors & Deli, 1870 Wayside Road, Tinton Falls, NJ, 07724",
                                IsDepot       = false,
                                Latitude      = 40.28636,
                                Longitude     = -74.095283,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pleasantville Gas, 1101  S Main St, Pleasantville, NJ, 08232",
                                Alias         = "Pleasantville Gas, 1101  S Main St, Pleasantville, NJ, 08232",
                                IsDepot       = false,
                                Latitude      = 39.381348,
                                Longitude     = -74.532306,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pochi, 1341 Springfield Ave, Maywood, NJ, 07607",
                                Alias         = "Pochi, 1341 Springfield Ave, Maywood, NJ, 07607",
                                IsDepot       = false,
                                Latitude      = 40.724029,
                                Longitude     = -74.244734,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Ponte Vecchio, 3863 Route 516, Old Bridge, NJ, 08857",
                                Alias         = "Ponte Vecchio, 3863 Route 516, Old Bridge, NJ, 08857",
                                IsDepot       = false,
                                Latitude      = 40.402437,
                                Longitude     = -74.298044,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Prime Deli, 1221 Asbury Avenue, Asbury Park, NJ, 07712",
                                Alias         = "Prime Deli, 1221 Asbury Avenue, Asbury Park, NJ, 07712",
                                IsDepot       = false,
                                Latitude      = 40.222173,
                                Longitude     = -74.01957,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Prime Market, 449 Herbertsville Road, Bricktown, NJ, 08724",
                                Alias         = "Prime Market, 449 Herbertsville Road, Bricktown, NJ, 08724",
                                IsDepot       = false,
                                Latitude      = 40.102325,
                                Longitude     = -74.104611,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Prompt Catering LLC, 521 Raritan Street, Sayerville, NJ, 08872",
                                Alias         = "Prompt Catering LLC, 521 Raritan Street, Sayerville, NJ, 08872",
                                IsDepot       = false,
                                Latitude      = 40.478299,
                                Longitude     = -74.297118,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Pse & G, 60 S Newark St, Paterson, NJ, 07514",
                                Alias         = "Pse & G, 60 S Newark St, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.915172,
                                Longitude     = -74.171049,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Que Chula Es Puebla, 210 Dayton Avenue, Newark, NJ, 07108",
                                Alias         = "Que Chula Es Puebla, 210 Dayton Avenue, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.874758,
                                Longitude     = -74.122109,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Quick Food Mart, 250 Route 9, Barnegat, NJ, 08005",
                                Alias         = "Quick Food Mart, 250 Route 9, Barnegat, NJ, 08005",
                                IsDepot       = false,
                                Latitude      = 39.753011,
                                Longitude     = -74.222953,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Quick Food, 234 Old Stage Rd, East Brunswick, NJ, 08816",
                                Alias         = "Quick Food, 234 Old Stage Rd, East Brunswick, NJ, 08816",
                                IsDepot       = false,
                                Latitude      = 40.40657,
                                Longitude     = -74.386443,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Quick Stop Deli, 814 Amboy Ave, Perth Amboy, NJ, 08861",
                                Alias         = "Quick Stop Deli, 814 Amboy Ave, Perth Amboy, NJ, 08861",
                                IsDepot       = false,
                                Latitude      = 40.527608,
                                Longitude     = -74.275038,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Quick Stop Foods, 120 West Sylvania, Neptune City, NJ, 07753",
                                Alias         = "Quick Stop Foods, 120 West Sylvania, Neptune City, NJ, 07753",
                                IsDepot       = false,
                                Latitude      = 40.198761,
                                Longitude     = -74.03182,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "R & G Food Ctr, 144 Vreeland Ave, Newark, NJ, 07107",
                                Alias         = "R & G Food Ctr, 144 Vreeland Ave, Newark, NJ, 07107",
                                IsDepot       = false,
                                Latitude      = 40.76638,
                                Longitude     = -74.185647,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "R&R Groc, 286 14th Ave, Irvington, NJ, 07111",
                                Alias         = "R&R Groc, 286 14th Ave, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.73986,
                                Longitude     = -74.204978,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Rainbow Deli & L, 292 Lakeview Avenue, Little Falls, NJ, 07424",
                                Alias         = "Rainbow Deli & L, 292 Lakeview Avenue, Little Falls, NJ, 07424",
                                IsDepot       = false,
                                Latitude      = 40.885017,
                                Longitude     = -74.138497,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Randy Grocery, 46 Quincy, Passaic, NJ, 07055",
                                Alias         = "Randy Grocery, 46 Quincy, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.867337,
                                Longitude     = -74.122781,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Rd Spmkt, 346 14th Ave, Newark, NJ, 07103",
                                Alias         = "Rd Spmkt, 346 14th Ave, Newark, NJ, 07103",
                                IsDepot       = false,
                                Latitude      = 40.73986,
                                Longitude     = -74.204978,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Rhode Island Market, 101 N Rhone Island Ave, Atlantic City, NJ, 08401",
                                Alias         = "Rhode Island Market, 101 N Rhone Island Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.368863,
                                Longitude     = -74.416528,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Rio Via Supermarket, 562 S Park St, Clifton, NJ, 07011",
                                Alias         = "Rio Via Supermarket, 562 S Park St, Clifton, NJ, 07011",
                                IsDepot       = false,
                                Latitude      = 40.881156,
                                Longitude     = -74.141612,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Robert Grocery, 71 Clinton Place, Newark, NJ, 07102",
                                Alias         = "Robert Grocery, 71 Clinton Place, Newark, NJ, 07102",
                                IsDepot       = false,
                                Latitude      = 40.720399,
                                Longitude     = -74.210768,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Rodriguez Grocery, 224 S. Orange Ave, Paterson, NJ, 07513",
                                Alias         = "Rodriguez Grocery, 224 S. Orange Ave, Paterson, NJ, 07513",
                                IsDepot       = false,
                                Latitude      = 40.738746,
                                Longitude     = -74.192629,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "S&M Groc, 487 Market St, Irvington, NJ, 07111",
                                Alias         = "S&M Groc, 487 Market St, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.726324,
                                Longitude     = -74.228643,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sams Food Mart, 303 Rt 36 N, Hazlet, NJ, 07730",
                                Alias         = "Sams Food Mart, 303 Rt 36 N, Hazlet, NJ, 07730",
                                IsDepot       = false,
                                Latitude      = 40.438034,
                                Longitude     = -74.143822,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "San Martin Grocery, 127 Passaic Ave, Passaic, NJ, 07055",
                                Alias         = "San Martin Grocery, 127 Passaic Ave, Passaic, NJ, 07055",
                                IsDepot       = false,
                                Latitude      = 40.859614,
                                Longitude     = -74.124275,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sandwich Stop, 104 S Franklin Ave, Pleasantville, NJ, 08232",
                                Alias         = "Sandwich Stop, 104 S Franklin Ave, Pleasantville, NJ, 08232",
                                IsDepot       = false,
                                Latitude      = 39.389619,
                                Longitude     = -74.52014,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Save More, 506 21st Ave, Paterson, NJ, 07513",
                                Alias         = "Save More, 506 21st Ave, Paterson, NJ, 07513",
                                IsDepot       = false,
                                Latitude      = 40.905816,
                                Longitude     = -74.151835,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Scarlett Groc, 75 5th St, W Paterson, NJ, 07424",
                                Alias         = "Scarlett Groc, 75 5th St, W Paterson, NJ, 07424",
                                IsDepot       = false,
                                Latitude      = 40.927511,
                                Longitude     = -74.176373,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Seastreak, 325 Shore Dr, Highland, NJ, 07732",
                                Alias         = "Seastreak, 325 Shore Dr, Highland, NJ, 07732",
                                IsDepot       = false,
                                Latitude      = 40.409412,
                                Longitude     = -73.996244,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Shahin Groc, 1542 Atlantic Ave, Atlantic City, NJ, 08401",
                                Alias         = "Shahin Groc, 1542 Atlantic Ave, Atlantic City, NJ, 08401",
                                IsDepot       = false,
                                Latitude      = 39.361097,
                                Longitude     = -74.430216,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Shalay Shaleish Café, 130 Livingston ave, New Brunswick, NJ, 08091",
                                Alias         = "Shalay Shaleish Café, 130 Livingston ave, New Brunswick, NJ, 08091",
                                IsDepot       = false,
                                Latitude      = 40.488921,
                                Longitude     = -74.448212,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sheepshead Bagels, 2095 Rt 35 N, Holmdel, NJ, 07748",
                                Alias         = "Sheepshead Bagels, 2095 Rt 35 N, Holmdel, NJ, 07748",
                                IsDepot       = false,
                                Latitude      = 40.414517,
                                Longitude     = -74.142318,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Shell Gas, 390 Shrewsbury Avenue, Red Bank, NJ, 07701",
                                Alias         = "Shell Gas, 390 Shrewsbury Avenue, Red Bank, NJ, 07701",
                                IsDepot       = false,
                                Latitude      = 40.337487,
                                Longitude     = -74.075389,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Shop Smart, 773 Springfield Ave, Newark, NJ, 07108",
                                Alias         = "Shop Smart, 773 Springfield Ave, Newark, NJ, 07108",
                                IsDepot       = false,
                                Latitude      = 40.728866,
                                Longitude     = -74.217217,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Silverton Pharm, 1824 Hooper Ave, Toms River, NJ, 08753",
                                Alias         = "Silverton Pharm, 1824 Hooper Ave, Toms River, NJ, 08753",
                                IsDepot       = false,
                                Latitude      = 40.011365,
                                Longitude     = -74.147465,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sipo's Bakery, 365 Smith Street, Perth Amboy, NJ, 08861",
                                Alias         = "Sipo's Bakery, 365 Smith Street, Perth Amboy, NJ, 08861",
                                IsDepot       = false,
                                Latitude      = 40.511425,
                                Longitude     = -74.278813,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Smiths Farm Market, 2810 Allaire Road, Wall Township, NJ, 07719",
                                Alias         = "Smiths Farm Market, 2810 Allaire Road, Wall Township, NJ, 07719",
                                IsDepot       = false,
                                Latitude      = 40.152529,
                                Longitude     = -74.073501,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Snack Station Barnet Hospital, 680 Broadway, Irvington, NJ, 07111",
                                Alias         = "Snack Station Barnet Hospital, 680 Broadway, Irvington, NJ, 07111",
                                IsDepot       = false,
                                Latitude      = 40.917592,
                                Longitude     = -74.144103,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "St.Benedicts, 165 Bethany Rd, Holmdel, NJ, 07730",
                                Alias         = "St.Benedicts, 165 Bethany Rd, Holmdel, NJ, 07730",
                                IsDepot       = false,
                                Latitude      = 40.40416,
                                Longitude     = -74.203567,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Stop & Save Mini Market, 420 Central Ave, Paterson, NJ, 07514",
                                Alias         = "Stop & Save Mini Market, 420 Central Ave, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.757528,
                                Longitude     = -74.218494,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Subzi Mundi, 2058 Route 130 Suite#10, Monmouth Junction, NJ, 08852",
                                Alias         = "Subzi Mundi, 2058 Route 130 Suite#10, Monmouth Junction, NJ, 08852",
                                IsDepot       = false,
                                Latitude      = 40.40805,
                                Longitude     = -74.506502,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sunoco A Plus Food Store, 943 Route 9 North, Sayreville, NJ, 08879",
                                Alias         = "Sunoco A Plus Food Store, 943 Route 9 North, Sayreville, NJ, 08879",
                                IsDepot       = false,
                                Latitude      = 40.409682,
                                Longitude     = -74.348256,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sunoco, 95 Highway 36, Keyport, NJ, 07735",
                                Alias         = "Sunoco, 95 Highway 36, Keyport, NJ, 07735",
                                IsDepot       = false,
                                Latitude      = 40.427168,
                                Longitude     = -74.197201,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sunrise, 1600 Main Street, Belmar, NJ, 07719",
                                Alias         = "Sunrise, 1600 Main Street, Belmar, NJ, 07719",
                                IsDepot       = false,
                                Latitude      = 40.174388,
                                Longitude     = -74.026944,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Sunshine Deli, 61 White Head Ave, South River, NJ, 08882",
                                Alias         = "Sunshine Deli, 61 White Head Ave, South River, NJ, 08882",
                                IsDepot       = false,
                                Latitude      = 40.445177,
                                Longitude     = -74.371003,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "The Broadway Diner, 126 Broadway (North), South Amboy, NJ, 08879",
                                Alias         = "The Broadway Diner, 126 Broadway (North), South Amboy, NJ, 08879",
                                IsDepot       = false,
                                Latitude      = 40.484253,
                                Longitude     = -74.280944,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "The Country Kitchen, 2501 Belmar Blvd, Belmar, NJ, 07719",
                                Alias         = "The Country Kitchen, 2501 Belmar Blvd, Belmar, NJ, 07719",
                                IsDepot       = false,
                                Latitude      = 40.176369,
                                Longitude     = -74.062386,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "The New Eastside Groc, 462 10th Ave, Paterson, NJ, 07514",
                                Alias         = "The New Eastside Groc, 462 10th Ave, Paterson, NJ, 07514",
                                IsDepot       = false,
                                Latitude      = 40.923346,
                                Longitude     = -74.145269,
                                Time          = 0
                    },

                    new Address() {
                                AddressString = "Tinton Falls Deli, 1191 Sycamore Avenue, Tinton Falls, NJ, 07724",
                                Alias         = "Tinton Falls Deli, 1191 Sycamore Avenue, Tinton Falls, NJ, 07724",
                                IsDepot       = false,
                                Latitude      = 40.305772,
                                Longitude     = -74.09978,
                                Time          = 0
                    },

                    new Address() {
                        AddressString = "Tm Family Conv, 51 N Main St, Paterson, NJ, 07514",
                        Alias         = "Tm Family Conv, 51 N Main St, Paterson, NJ, 07514",
                        IsDepot       = false,
                        Latitude      = 40.924336,
                        Longitude     = -74.17162,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Tnc Mini Mart, 80 Eats 1St Ave, Atlantic Highlands, NJ, 07716",
                        Alias         = "Tnc Mini Mart, 80 Eats 1St Ave, Atlantic Highlands, NJ, 07716",
                        IsDepot       = false,
                        Latitude      = 40.413674,
                        Longitude     = -74.037695,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Tony's Freehold Grill, 59 East Main Street, Freehold, NJ, 07728",
                        Alias         = "Tony's Freehold Grill, 59 East Main Street, Freehold, NJ, 07728",
                        IsDepot       = false,
                        Latitude      = 40.261613,
                        Longitude     = -74.272369,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Tony'S Mini Mart, 305 Park Ave, Paterson, NJ, 07524",
                        Alias         = "Tony'S Mini Mart, 305 Park Ave, Paterson, NJ, 07524",
                        IsDepot       = false,
                        Latitude      = 40.914833,
                        Longitude     = -74.152895,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Tony'S Pizza, 759 Main Ave, Paterson, NJ, 07501",
                        Alias         = "Tony'S Pizza, 759 Main Ave, Paterson, NJ, 07501",
                        IsDepot       = false,
                        Latitude      = 40.863545,
                        Longitude     = -74.128925,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Torres Mini Market, 403 Bruck Avenue, Perth Amboy, NJ, 08861",
                        Alias         = "Torres Mini Market, 403 Bruck Avenue, Perth Amboy, NJ, 08861",
                        IsDepot       = false,
                        Latitude      = 40.528262,
                        Longitude     = -74.271842,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Torres Supermarket, 168 Grove Street, Newark, NJ, 07105",
                        Alias         = "Torres Supermarket, 168 Grove Street, Newark, NJ, 07105",
                        IsDepot       = false,
                        Latitude      = 40.749295,
                        Longitude     = -74.207298,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Town & Surf, 77 First Ave, Atlantic Highlands, NJ, 07716",
                        Alias         = "Town & Surf, 77 First Ave, Atlantic Highlands, NJ, 07716",
                        IsDepot       = false,
                        Latitude      = 40.413983,
                        Longitude     = -74.038003,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Town n Country Inn, 35 Broadway @ Rt 35 North, Keyport, NJ, 07735",
                        Alias         = "Town n Country Inn, 35 Broadway @ Rt 35 North, Keyport, NJ, 07735",
                        IsDepot       = false,
                        Latitude      = 40.38752,
                        Longitude     = -74.097893,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Tropico Mini Mart, 40 Broad Street, Keyport, NJ, 07725",
                        Alias         = "Tropico Mini Mart, 40 Broad Street, Keyport, NJ, 07725",
                        IsDepot       = false,
                        Latitude      = 40.437838,
                        Longitude     = -74.202413,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Tulcingo Grocery, 256 Ocean Ave, Lakewood, NJ, 08701",
                        Alias         = "Tulcingo Grocery, 256 Ocean Ave, Lakewood, NJ, 08701",
                        IsDepot       = false,
                        Latitude      = 40.090165,
                        Longitude     = -74.205323,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Twin Pond Farm, 1459 - 1473 Route 9 North, Howell, NJ, 07731",
                        Alias         = "Twin Pond Farm, 1459 - 1473 Route 9 North, Howell, NJ, 07731",
                        IsDepot       = false,
                        Latitude      = 40.192329,
                        Longitude     = -74.25131,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Victoria Mini Mt, 394 E 18th St, Clifton, NJ, 07011",
                        Alias         = "Victoria Mini Mt, 394 E 18th St, Clifton, NJ, 07011",
                        IsDepot       = false,
                        Latitude      = 40.881156,
                        Longitude     = -74.141612,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Viva Mexico, 296 River Avenue, Lakeood, NJ, 08701",
                        Alias         = "Viva Mexico, 296 River Avenue, Lakeood, NJ, 08701",
                        IsDepot       = false,
                        Latitude      = 40.079432,
                        Longitude     = -74.216707,
                        Time          = 0
            },

            new Address() {
                        AddressString = "W Fresh, 4412 Rt 9 South, Freehold, NJ, 07728",
                        Alias         = "W Fresh, 4412 Rt 9 South, Freehold, NJ, 07728",
                        IsDepot       = false,
                        Latitude      = 40.286583,
                        Longitude     = -74.295474,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Waterside, 101 Boardwalk, Atlantic City, NJ, 08401",
                        Alias         = "Waterside, 101 Boardwalk, Atlantic City, NJ, 08401",
                        IsDepot       = false,
                        Latitude      = 39.365196,
                        Longitude     = -74.410547,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Welsh Farms (76 Gas), 659 Rt 36, Belford, NJ, 07718",
                        Alias         = "Welsh Farms (76 Gas), 659 Rt 36, Belford, NJ, 07718",
                        IsDepot       = false,
                        Latitude      = 40.41911,
                        Longitude     = -74.084993,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Welsh Farms, 33 West Main Street, Farmingdale, NJ, 07727",
                        Alias         = "Welsh Farms, 33 West Main Street, Farmingdale, NJ, 07727",
                        IsDepot       = false,
                        Latitude      = 40.199729,
                        Longitude     = -74.174155,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Willow Deli, 290 Willow Drive, Little Silver, NJ, 07739",
                        Alias         = "Willow Deli, 290 Willow Drive, Little Silver, NJ, 07739",
                        IsDepot       = false,
                        Latitude      = 40.328604,
                        Longitude     = -74.040089,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Wilson Ave Deli, 198 Wilson Ave, Port Monmouth, NJ, 07758",
                        Alias         = "Wilson Ave Deli, 198 Wilson Ave, Port Monmouth, NJ, 07758",
                        IsDepot       = false,
                        Latitude      = 40.426408,
                        Longitude     = -74.103064,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Winward Deli, 254 Maple Ave, Red Bank, NJ, 07701",
                        Alias         = "Winward Deli, 254 Maple Ave, Red Bank, NJ, 07701",
                        IsDepot       = false,
                        Latitude      = 40.342252,
                        Longitude     = -74.067954,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Wp Grocery, 497 12th Ave, Paterson, NJ, 07513",
                        Alias         = "Wp Grocery, 497 12th Ave, Paterson, NJ, 07513",
                        IsDepot       = false,
                        Latitude      = 40.919401,
                        Longitude     = -74.142398,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Yellow Rose Diner, 41 Route 36 North, Keyport, NJ, 07735",
                        Alias         = "Yellow Rose Diner, 41 Route 36 North, Keyport, NJ, 07735",
                        IsDepot       = false,
                        Latitude      = 40.427893,
                        Longitude     = -74.194583,
                        Time          = 0
            },

            new Address() {
                        AddressString = "Zoilas, 124 Pasaic St, Paterson, NJ, 07513",
                        Alias         = "Zoilas, 124 Pasaic St, Paterson, NJ, 07513",
                        IsDepot       = false,
                        Latitude      = 40.913417,
                        Longitude     = -74.170402,
                        Time          = 0
            },
            
                    #endregion
                };

            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "C# TEST 3",
                RT = true,
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.Today.Date),
                RouteTime = 25200,
                RouteMaxDuration = 36000,
                VehicleMaxDistanceMI = 10000,
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Parts = 5
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var dataObject = route4Me.RunAsyncOptimization(
                                        optimizationParameters,
                                        out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}
