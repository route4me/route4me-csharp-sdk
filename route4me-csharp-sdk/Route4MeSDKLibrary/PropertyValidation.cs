using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Route4MeSDK.DataTypes;

namespace Route4MeSDK
{
    /// <summary>
    ///     Validation of the class properties
    /// </summary>
    public static class PropertyValidation
    {
        private static readonly string[] CountryIDs =
        {
            "US", "GB", "CA", "NL", "AU", "MX", "AF", "AL", "DZ", "AS", "AD", "AO", "AI", "AQ", "AG", "AR", "AM", "AW",
            "AT", "AZ", "BS", "BH", "BD", "BB",
            "BY", "BE", "BZ", "BJ", "BM", "BT", "BO", "BA", "BW", "BV", "BR", "BN", "BG", "BF", "BI", "KH", "CM", "CV",
            "KY", "TD", "CL", "CN", "CX", "CO",
            "KM", "CG", "CK", "CR", "HR", "CU", "CY", "CZ", "CD", "DK", "DJ", "DM", "DO", "TP", "EC", "EG", "SV", "GQ",
            "ER", "EE", "ET", "FK", "FO", "FJ",
            "FI", "FR", "GF", "PF", "GA", "GM", "GE", "DE", "GH", "GI", "GR", "GL", "GD", "GP", "GU", "GT", "GN", "GW",
            "GY", "HT", "HN", "HK", "HU", "IS",
            "IN", "ID", "IR", "IQ", "IE", "IL", "IT", "JM", "JP", "JO", "KZ", "KE", "KI", "KR", "KW", "KG", "LA", "LV",
            "LB", "LS", "LR", "LY", "LI", "LT",
            "LU", "MO", "MG", "MW", "MY", "MV", "ML", "MT", "MH", "MQ", "MR", "MU", "YT", "FM", "MD", "MC", "MN", "MS",
            "MA", "MZ", "MM", "NA", "NR", "NP",
            "AN", "NC", "NZ", "NI", "NE", "NG", "NU", "NF", "KP", "NO", "OM", "PK", "PW", "PA", "PG", "PY", "PE", "PH",
            "PN", "PL", "PT", "PR", "QA", "RE",
            "RO", "RU", "RW", "KN", "LC", "WS", "SM", "SA", "SN", "SC", "SL", "SG", "SK", "SI", "SB", "SO", "ZA", "ES",
            "LK", "SH", "PM", "SD", "SR", "SZ",
            "SE", "CH", "SY", "TW", "TJ", "TZ", "TH", "TG", "TK", "TO", "TT", "TN", "TR", "TM", "TC", "TV", "UG", "UA",
            "AE", "UM", "UY", "UZ", "VU", "VA",
            "VE", "VN", "VG", "VI", "WF", "YE", "YU", "ZM", "ZW"
        };

        private static readonly string[] UsaStateIDs =
        {
            "AK", "AL", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA",
            "KS", "KY", "LA", "ME", "MH", "MD", "MA",
            "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PW", "PA",
            "PR", "RI", "SC", "SD", "TN", "TX",
            "UT", "VT", "VI", "VA", "WA", "WV", "WI", "WY", "AE",
            "CA", "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" // Canadian
        };

        private static readonly string[] AddressStopTypes =
        {
            "DELIVERY", "PICKUP", "BREAK", "MEETUP", "SERVICE", "VISIT", "DRIVEBY"
        };

        public static ValidationResult ValidateMonthlyNthN(int N)
        {
            int[] nList = {1, 2, 3, 4, 5, -1};
            // Perform validation logic here and set isValid to true or false.

            if (Array.IndexOf(nList, N) >= 0)
                return ValidationResult.Success;
            return new ValidationResult(
                "The selected option is not available for this type of the schedule.");
        }

        public static ValidationResult ValidateScheduleMode(string sMode)
        {
            string[] sList = {"daily", "weekly", "monthly", "annually"};
            // Perform validation logic here and set isValid to true or false.

            if (Array.IndexOf(sList, sMode) >= 0)
                return ValidationResult.Success;
            return new ValidationResult(
                "The selected option is not available for this type of the schedule.");
        }

        public static ValidationResult ValidateScheduleMonthlyMode(string sMode)
        {
            string[] sList = {"dates", "nth"};
            // Perform validation logic here and set isValid to true or false.

            if (Array.IndexOf(sList, sMode) >= 0)
                return ValidationResult.Success;
            return new ValidationResult(
                "The selected option is not available for this type of the schedule.");
        }

        public static ValidationResult ValidateEpochTime(object value)
        {
            var iTime = 0;

            if (int.TryParse(value.ToString(), out iTime) && value.GetType().ToString() != "System.String")
                return ValidationResult.Success;
            return new ValidationResult(
                "The property time can not have the value " + value);
        }

        public static ValidationResult ValidateOverrideAddresses(object value)
        {
            try
            {
                var oaddr = (OverrideAddresses) value;
                return ValidationResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Validation of the override_addresses's value failed. " + ex.Message);
                return new ValidationResult(
                    "The property override_addresses can not have the value " + value);
            }
        }

        public static ValidationResult ValidateLatitude(object value)
        {
            if (value == null) return null;

            try
            {
                if (double.TryParse(value.ToString(), out var __))
                {
                    var lat = Convert.ToDouble(value);

                    return lat >= -90 && lat <= 90
                        ? ValidationResult.Success
                        : new ValidationResult("The latitude value is wrong: " + (value?.ToString() ?? "null"));
                }

                return new ValidationResult(
                    "The latitude value is wrong: " + (value?.ToString() ?? "null"));
            }
            catch (Exception ex)
            {
                return new ValidationResult(
                    "The latitude value is wrong: " + (value?.ToString() ?? "null") + $". Exception: {ex.Message}");
            }
        }

        public static ValidationResult ValidateLongitude(object value)
        {
            if (value == null) return null;

            try
            {
                if (double.TryParse(value.ToString(), out var __))
                {
                    var lng = Convert.ToDouble(value);

                    return lng >= -180 && lng <= 180
                        ? ValidationResult.Success
                        : new ValidationResult("The longitude value is wrong: " + (value?.ToString() ?? "null"));
                }

                return new ValidationResult(
                    "The longitude value is wrong: " + (value?.ToString() ?? "null"));
            }
            catch (Exception ex)
            {
                return new ValidationResult(
                    "The longitude value is wrong: " + (value?.ToString() ?? "null") + $". Exception: {ex.Message}");
            }
        }

        public static ValidationResult ValidateTimeWindow(object value)
        {
            if (value == null) return null;

            try
            {
                if (long.TryParse(value.ToString(), out var __))
                {
                    var tw = Convert.ToInt64(value);

                    return tw >= 0 && tw <= 86400
                        ? ValidationResult.Success
                        : new ValidationResult("The time window value is wrong: " + (value?.ToString() ?? "null"));
                }

                return new ValidationResult(
                    "The time window value is wrong: " + (value?.ToString() ?? "null"));
            }
            catch (Exception ex)
            {
                return new ValidationResult(
                    "The time window value is wrong: " + (value?.ToString() ?? "null") + $". Exception: {ex.Message}");
            }
        }

        public static ValidationResult ValidateCsvTimeWindow(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            var pattern = @"^[0-1]{0,1}[0-9]{1}\:[0-5]{1}[0-9]{1}$";

            var regex = new Regex(pattern);
            var match = regex.Match(value.ToString());

            return match.Success
                ? ValidationResult.Success
                : new ValidationResult("The CSV time window value is wrong: " + value);
        }

        public static ValidationResult ValidateServiceTime(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (!long.TryParse(value.ToString(), out var __))
                return new ValidationResult("The csv service time must be integer: " + value);

            var servtime = Convert.ToInt32(value);

            return servtime >= 0 && servtime < 28800
                ? ValidationResult.Success
                : new ValidationResult("The CSV service time value is wrong: " + value);
        }

        public static ValidationResult ValidateCsvServiceTime(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(int))
                return new ValidationResult("The csv service time must be integer: " + value);

            var servtime = Convert.ToInt32(value);

            return servtime >= 0 && servtime < 480
                ? ValidationResult.Success
                : new ValidationResult("The CSV service time value is wrong: " + value);
        }

        public static ValidationResult ValidateCountryId(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The country ID must be a string: " + value);

            var v = value.ToString().ToUpper();

            return CountryIDs.Contains(v)
                ? ValidationResult.Success
                : new ValidationResult("The country ID value is wrong: " + value);
        }

        public static ValidationResult ValidationStateId(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The state ID must be a string: " + value);

            var v = value.ToString().ToUpper();

            return UsaStateIDs.Contains(v)
                ? ValidationResult.Success
                : new ValidationResult("The state ID value is not USA state ID: " + value);
        }

        public static ValidationResult ValidateEmail(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The email must be a string: " + value);

            var regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                  + "@"
                                  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            var match = regex.Match(value.ToString());

            return match.Success
                ? ValidationResult.Success
                : new ValidationResult("The email is wrong: " + value);
        }

        public static ValidationResult ValidatePhone(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            var pattern =
                @"^(\+\s{0,1}){0,1}(\(\d{1,3}\)|\d{1,3}){1}[ |-]{1}(\d{2,4}[ |.|-]{1})(\d{2,5}[ |.|-]{0,1}){1}(\d{2,5}[ |.|-]{0,1}){0,1}(\d{2,5}[ |.|-]{0,1}){0,1}$";

            if (value.GetType() != typeof(string))
                return new ValidationResult("The phone number must be a string: " + value);

            var regex = new Regex(pattern);
            var match = regex.Match(value.ToString());

            return match.Success
                ? ValidationResult.Success
                : new ValidationResult("The phone number is wrong: " + value);
        }

        public static ValidationResult ValidateZipCode(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The zip code must be a string: " + value);

            var regex = new Regex(@"^\d{4,5}(-\d{4})?$");
            var match = regex.Match(value.ToString());

            return match.Success
                ? ValidationResult.Success
                : new ValidationResult("The zip code is wrong: " + value);
        }

        public static ValidationResult ValidateAddressStopType(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The address stop type must be a string: " + value);

            var v = value.ToString().ToUpper();

            return AddressStopTypes.Contains(v)
                ? ValidationResult.Success
                : new ValidationResult("The address stop type value is wrong: " + value);
        }

        public static ValidationResult ValidateFirstName(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The first name must be a string: " + value);

            return value.ToString().Length <= 25
                ? ValidationResult.Success
                : new ValidationResult("The first name length should be < 26: " + value);
        }

        public static ValidationResult ValidateLastName(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The last name must be a string: " + value);

            return value.ToString().Length <= 35
                ? ValidationResult.Success
                : new ValidationResult("The last name length should be < 26: " + value);
        }

        public static ValidationResult ValidateColorValue(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            if (value.GetType() != typeof(string))
                return new ValidationResult("The color must be a string: " + value);

            var pattern = @"^[a-fA-F0-9]{6}$";

            var regex = new Regex(pattern);
            var match = regex.Match(value.ToString());

            return match.Success
                ? ValidationResult.Success
                : new ValidationResult("The color value is wrong: " + value);
        }

        public static ValidationResult ValidateCustomData(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            try
            {
                var val = R4MeUtils.ReadObjectNew<Dictionary<string, string>>(value.ToString());

                return val != null
                    ? ValidationResult.Success
                    : new ValidationResult("The custom data is wrong: " + value);
            }
            catch (Exception ex)
            {
                return new ValidationResult("The custom data is wrong: " + value + $". Exception: {ex.Message}");
            }
        }

        public static ValidationResult ValidateIsNumber(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            return double.TryParse(value.ToString(), out var __)
                ? ValidationResult.Success
                : new ValidationResult("The value is not number: " + value);
        }

        public static ValidationResult ValidateIsBoolean(object value)
        {
            if ((value?.ToString().Length ?? 0) < 1) return null;

            return bool.TryParse(value.ToString(), out var __)
                ? ValidationResult.Success
                : new ValidationResult("The value is not boolean: " + value);
        }
    }
}