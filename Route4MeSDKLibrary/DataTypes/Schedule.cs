using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// A trip schedule to a location
    /// </summary>
    [DataContract]
    public sealed class Schedule
    {
        public Schedule(string sMode, bool blNth)
        {
            switch (sMode)
            {
                case "daily":
                    this.daily = new schedule_daily();
                    this.mode = "daily";
                    break;
                case "weekly":
                    this.weekly = new schedule_weekly();
                    this.mode = "weekly";
                    //this.weekly.weekdays = new int[]{};
                    break;
                case "monthly":
                    this.monthly = new schedule_monthly();
                    this.mode = "monthly";
                    if (blNth) this.monthly.nth = new schedule_monthly_nth();
                    break;
                case "annually":
                    this.annually = new schedule_annually();
                    this.mode = "annually";
                    if (blNth) this.annually.nth = new schedule_monthly_nth();
                    //this.annually.months = new int[] { }; 
                    break;
            }
            
        }

        public Schedule()
        {

        }

        [DataMember(Name = "enabled")]
        public bool enabled { get; set; }

        [DataMember(Name = "mode"), CustomValidation(typeof(PropertyValidation), "ValidateScheduleMode")]
        public string mode { get; set; }

        [DataMember(Name = "daily", EmitDefaultValue = false, IsRequired = false)]
        public schedule_daily daily { get; set; }

        [DataMember(Name = "weekly", EmitDefaultValue = false, IsRequired = false)]
        public schedule_weekly weekly { get; set; }

        [DataMember(Name = "monthly", EmitDefaultValue = false, IsRequired = false)]
        public schedule_monthly monthly { get; set; }

        [DataMember(Name = "annually", EmitDefaultValue = false, IsRequired = false)]
        public schedule_annually annually { get; set; }

        public bool ValidateScheduleMode(object ScheduleMode)
        {
            if (ScheduleMode == null) return false;
            if (Array.IndexOf(new string[]{"daily","weekly","monthly","annually"},ScheduleMode.ToString())>=0) return true;
            return false;
        }

        public bool ValidateScheduleEnabled(object ScheduleEnabled)
        {
            bool blValid = false;
            if (bool.TryParse(ScheduleEnabled.ToString(), out blValid)) return true; else return false;
        }

        public bool ValidateScheduleUseNth(object ScheduleUseNth)
        {
            bool blValid = false;
            if (bool.TryParse(ScheduleUseNth.ToString(), out blValid)) return true; else return false;
        }

        public bool ValidateScheduleEvery(object ScheduleEvery)
        {
            int iEvery = -1;
            if (int.TryParse(ScheduleEvery.ToString(), out iEvery)) return true; else return false;
        }

        public bool ValidateScheduleWeekdays(object Weekdays)
        {
            if (Weekdays == null) return false;

            bool blValid = true;

            string[] arWeekdays = Weekdays.ToString().Split(',');

            foreach (string weekday in arWeekdays)
            {
                int iWeekday = -1;
                if (!int.TryParse(weekday, out iWeekday)) { blValid = false; break; }

                iWeekday = Convert.ToInt32(weekday);
                if (iWeekday > 7 || iWeekday < 1) { blValid = false; break; }
            }

            return blValid;
        }

        public bool ValidateScheduleMonthDays(object ScheduleMonthDays)
        {
            if (ScheduleMonthDays == null) return false;

            bool blValid = true;

            string[] arMonthdays = ScheduleMonthDays.ToString().Split(',');

            foreach (string monthday in arMonthdays)
            {
                int iMonthday = -1;
                if (!int.TryParse(monthday, out iMonthday)) { blValid = false; break; }

                iMonthday = Convert.ToInt32(monthday);
                if (iMonthday > 31 || iMonthday < 1) { blValid = false; break; }
            }

            return blValid;
        }

        public bool ValidateScheduleYearMonths(object ScheduleYearMonths)
        {
            if (ScheduleYearMonths == null) return false;

            bool blValid = true;

            string[] arYearMonth = ScheduleYearMonths.ToString().Split(',');

            foreach (string yearmonth in arYearMonth)
            {
                int iYearmonth = -1;
                if (!int.TryParse(yearmonth, out iYearmonth)) { blValid = false; break; }

                iYearmonth = Convert.ToInt32(yearmonth);
                if (iYearmonth > 12 || iYearmonth < 1) { blValid = false; break; }
            }

            return blValid;
        }

        public bool ValidateScheduleMonthlyMode(object ScheduleMonthlyMode)
        {
            if (ScheduleMonthlyMode == null) return false;
            if (Array.IndexOf(new string[] { "dates", "nth" }, ScheduleMonthlyMode.ToString()) >= 0) return true;
            return false;
        }

        public bool ValidateScheduleNthN(object ScheduleNthN)
        {
            int iN = -1;
            if (!int.TryParse(ScheduleNthN.ToString(), out iN)) return false;
            iN = Convert.ToInt32(ScheduleNthN);

            if (Array.IndexOf(new int[] { 1, 2, 3, 4, 5, -1 }, iN) < 0) return false;

            return true;
        }

        public bool ValidateScheduleNthWhat(object ScheduleNthWhat)
        {
            int iN = -1;
            if (!int.TryParse(ScheduleNthWhat.ToString(), out iN)) return false;
            iN = Convert.ToInt32(ScheduleNthWhat);

            if (Array.IndexOf(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, iN) < 0) return false;

            return true;
        }

    }

    [DataContract]
    public class schedule_daily
    {
        public schedule_daily(int _every=1)
        {
            every = _every;
        }

        [DataMember(Name = "every")]
        public int every { get; set; }
    }

    [DataContract]
    public class schedule_weekly
    {
        public schedule_weekly(int _every=1, int[] _weekdays=null)
        {
            every = _every;
            if (_weekdays != null) weekdays = _weekdays;
        }

        [DataMember(Name = "every")]
        public int every { get; set; }

        [DataMember(Name = "weekdays", EmitDefaultValue = false), Range(1, 7, ErrorMessage = "Weekday must be between 1 and 7")]
        public int[] weekdays { get; set; }
    }

    [DataContract]
    public class schedule_monthly_nth
    {
        public schedule_monthly_nth(int _n=1, int _what=1)
        {
            n = _n;
            what = _what;
        }

        [DataMember(Name = "n", EmitDefaultValue = false), CustomValidation(typeof(PropertyValidation), "ValidateMonthlyNthN")]
        public int n { get; set; }

        [DataMember(Name = "what", EmitDefaultValue = false), Range(1, 10, ErrorMessage = "Wrong value for the What Time parameter")]
        public int what { get; set; }
    }

    [DataContract]
    public class schedule_monthly
    {
        public schedule_monthly(int _every=1, string _mode="dates", int[] _dates = null, Dictionary<int,int> _nth=null)
        {
            every = _every;
            mode = _mode;
            if (_dates != null) dates = _dates;
            if (_nth != null)
            {
                int _n = -1;
                int _what = -1;
                foreach (KeyValuePair<int, int> kv1 in _nth)
                {
                    _n = kv1.Key;
                    _what = kv1.Value;
                }
                if (_n != -1 && _what != -1)
                {
                    this.nth = new schedule_monthly_nth(_n, _what);
                }
            }
        }

        [DataMember(Name = "every")]
        public int every { get; set; }

        [DataMember(Name = "mode"), CustomValidation(typeof(PropertyValidation), "ValidateScheduleMonthlyMode")]
        public string mode { get; set; }

        [DataMember(Name = "dates", EmitDefaultValue = false), Range(1, 31, ErrorMessage = "Month day must be between 1 and 31")]
        public int[] dates { get; set; }

        [DataMember(Name = "nth", EmitDefaultValue = false)]
        public schedule_monthly_nth nth { get; set; }
    }

    [DataContract]
    public class schedule_annually
    {
        [DataMember(Name = "every")]
        public int every { get; set; }

        [DataMember(Name = "use_nth")]
        public bool use_nth { get; set; }

        [DataMember(Name = "months", EmitDefaultValue = false), Range(1, 12, ErrorMessage = "Month number must be between 1 and 12")]
        public int[] months { get; set; }

        [DataMember(Name = "nth", EmitDefaultValue = false)]
        public schedule_monthly_nth nth { get; set; }
    }
}