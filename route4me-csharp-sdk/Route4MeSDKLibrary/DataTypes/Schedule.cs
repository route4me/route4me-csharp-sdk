using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     A trip schedule to a location
    /// </summary>
    [DataContract]
    public sealed class Schedule
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Schedule" /> class.
        /// </summary>
        /// <param name="scheduleMode">
        ///     The schedule mode.
        ///     <para>
        ///         Available values:
        ///         <value>daily</value>
        ///         ,
        ///         <value>weekly</value>
        ///         ,
        ///         <value>monthly</value>
        ///         ,
        ///         <value>annually</value>
        ///     </para>
        /// </param>
        /// <param name="blNth">if set to <c>true</c> the schedule_monthly_nth type object will be initialized</param>
        public Schedule(string scheduleMode, bool blNth)
        {
            switch (scheduleMode)
            {
                case "daily":
                    Daily = new ScheduleDaily();
                    Mode = "daily";
                    break;
                case "weekly":
                    Weekly = new ScheduleWeekly();
                    Mode = "weekly";
                    break;
                case "monthly":
                    Monthly = new ScheduleMonthly();
                    Mode = "monthly";
                    if (blNth) Monthly.Nth = new ScheduleMonthlyNth();
                    break;
                case "annually":
                    Annually = new ScheduleAnnually();
                    Mode = "annually";
                    if (blNth) Annually.Nth = new ScheduleMonthlyNth();
                    break;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Schedule" /> class.
        /// </summary>
        public Schedule()
        {
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="Schedule" /> is enabled.
        /// </summary>
        /// <value>
        ///     If <c>true</c>, the schedule is enabled; otherwise - not enabled</c>.
        /// </value>
        [DataMember(Name = "enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        ///     When schedule will be started from.
        /// </summary>
        [DataMember(Name = "from")]
        [CustomValidation(typeof(PropertyValidation), "ValidateScheduleFrom")]
        public string From { get; set; }

        /// <summary>
        ///     Schedule mode
        ///     <para>
        ///         Available values:
        ///         <value>daily</value>
        ///         ,
        ///         <value>weekly</value>
        ///         ,
        ///         <value>monthly</value>
        ///         ,
        ///         <value>annually</value>
        ///     </para>
        /// </summary>
        [DataMember(Name = "mode")]
        [CustomValidation(typeof(PropertyValidation), "ValidateScheduleMode")]
        public string Mode { get; set; }

        /// <summary>
        ///     If schedule mode is daily, specifies daily schedule data.
        ///     See <see cref="ScheduleDaily" />
        /// </summary>
        [DataMember(Name = "daily", EmitDefaultValue = false, IsRequired = false)]
        public ScheduleDaily Daily { get; set; }

        /// <summary>
        ///     If schedule mode is weekly, specifies weekly schedule data.
        ///     See <see cref="ScheduleWeekly" />
        /// </summary>
        [DataMember(Name = "weekly", EmitDefaultValue = false, IsRequired = false)]
        public ScheduleWeekly Weekly { get; set; }

        /// <summary>
        ///     If schedule mode is monthly, specifies monthly schedule data.
        ///     See <see cref="ScheduleMonthly" />
        /// </summary>
        [DataMember(Name = "monthly", EmitDefaultValue = false, IsRequired = false)]
        public ScheduleMonthly Monthly { get; set; }

        /// <summary>
        ///     If schedule mode is annually, specifies annually schedule data.
        ///     See <see cref="ScheduleAnnually" />
        /// </summary>
        [DataMember(Name = "annually", EmitDefaultValue = false, IsRequired = false)]
        public ScheduleAnnually Annually { get; set; }

        /// <summary>
        ///     Validates the parameter 'mode'. See <see cref="Schedule.Mode" />
        /// </summary>
        /// <param name="ScheduleMode">Schedule mode</param>
        /// <returns>True, if the parameter 'mode' was validated successfully.</returns>
        public bool ValidateScheduleMode(object ScheduleMode)
        {
            return ScheduleMode == null
                ? false
                : Array.IndexOf(new[] {"daily", "weekly", "monthly", "annually"}, ScheduleMode.ToString()) >= 0
                    ? true
                    : false;
        }

        /// <summary>
        ///     Validates the parameter 'enable'. See <see cref="Schedule.Enabled" />
        /// </summary>
        /// <param name="ScheduleEnabled">Boolean value for the parameter 'enable'</param>
        /// <returns>True, if the parameter 'enable' was validated successfully.</returns>
        public bool ValidateScheduleEnabled(object ScheduleEnabled)
        {
            var blValid = false;
            return bool.TryParse(ScheduleEnabled.ToString(), out blValid) ? true : false;
        }

        /// <summary>
        ///     Validates the parameter 'from'. See <see cref="Schedule.From" />
        /// </summary>
        /// <param name="ScheduleFrom">Date value for the parameter 'from'</param>
        /// <returns>True, if the parameter 'from' was validated successfully.</returns>
        public bool ValidateScheduleFrom(object ScheduleFrom)
        {
            var dtOut = DateTime.MinValue;
            return DateTime.TryParseExact(ScheduleFrom.ToString(), "yyyy-MM-dd", new CultureInfo("fr-FR"),
                DateTimeStyles.None, out dtOut)
                ? true
                : false;
        }

        /// <summary>
        ///     Validates the parameter 'use_nth'. See <see cref="ScheduleAnnually.UseNth" />
        /// </summary>
        /// <param name="ScheduleUseNth">Boolean value for the parameter 'use_nth'</param>
        /// <returns>True, if the parameter 'use_nth' was validated successfully.</returns>
        public bool ValidateScheduleUseNth(object ScheduleUseNth)
        {
            var blValid = false;
            return bool.TryParse(ScheduleUseNth.ToString(), out blValid) ? true : false;
        }

        /// <summary>
        ///     Validates the parameter 'every'. See <see cref="ScheduleDaily.Every" />
        /// </summary>
        /// <param name="ScheduleEvery">Integer value for the parameter 'every'</param>
        /// <returns>True, if the parameter 'every' was validated successfully.</returns>
        public bool ValidateScheduleEvery(object ScheduleEvery)
        {
            var iEvery = -1;
            return int.TryParse(ScheduleEvery.ToString(), out iEvery) ? true : false;
        }

        /// <summary>
        ///     Validates the parameter 'weekdays'. See <see cref="ScheduleWeekly.Weekdays" />
        /// </summary>
        /// <param name="Weekdays">Comma-delimited weekday numbers for the parameter 'weekdays'</param>
        /// <returns>True, if the parameter 'weekdays' was validated successfully.</returns>
        public bool ValidateScheduleWeekdays(object Weekdays)
        {
            if (Weekdays == null) return false;

            var blValid = true;

            var arWeekdays = Weekdays.ToString().Split(',');

            foreach (var weekday in arWeekdays)
            {
                var iWeekday = -1;
                if (!int.TryParse(weekday, out iWeekday))
                {
                    blValid = false;
                    break;
                }

                iWeekday = Convert.ToInt32(weekday);
                if (iWeekday > 7 || iWeekday < 1)
                {
                    blValid = false;
                    break;
                }
            }

            return blValid;
        }

        /// <summary>
        ///     Validates the parameter 'dates'. See <see cref="ScheduleMonthly.Dates" />
        /// </summary>
        /// <param name="ScheduleMonthDays">Comma-delimited month days for the parameter 'dates'</param>
        /// <returns>True, if the parameter 'dates' was validated successfully.</returns>
        public bool ValidateScheduleMonthDays(object ScheduleMonthDays)
        {
            if (ScheduleMonthDays == null) return false;

            var blValid = true;

            var arMonthdays = ScheduleMonthDays.ToString().Split(',');

            foreach (var monthday in arMonthdays)
            {
                var iMonthday = -1;
                if (!int.TryParse(monthday, out iMonthday))
                {
                    blValid = false;
                    break;
                }

                iMonthday = Convert.ToInt32(monthday);
                if (iMonthday > 31 || iMonthday < 1)
                {
                    blValid = false;
                    break;
                }
            }

            return blValid;
        }

        /// <summary>
        ///     Validates the parameter 'months'. See <see cref="ScheduleAnnually.Months" />
        /// </summary>
        /// <param name="ScheduleYearMonths">Comma-delimited month numbers for the parameter 'months'</param>
        /// <returns>True, if the parameter 'months' was validated successfully.</returns>
        public bool ValidateScheduleYearMonths(object ScheduleYearMonths)
        {
            if (ScheduleYearMonths == null) return false;

            var blValid = true;

            var arYearMonth = ScheduleYearMonths.ToString().Split(',');

            foreach (var yearmonth in arYearMonth)
            {
                var iYearmonth = -1;
                if (!int.TryParse(yearmonth, out iYearmonth))
                {
                    blValid = false;
                    break;
                }

                iYearmonth = Convert.ToInt32(yearmonth);
                if (iYearmonth > 12 || iYearmonth < 1)
                {
                    blValid = false;
                    break;
                }
            }

            return blValid;
        }

        /// <summary>
        ///     Validates the parameter 'mode' of the class ScheduleMonthly.
        ///     See <see cref="ScheduleMonthly.Mode" />
        /// </summary>
        /// <param name="ScheduleMonthlyMode"></param>
        /// <returns>True, if the parameter 'mode' was validated successfully.</returns>
        public bool ValidateScheduleMonthlyMode(object ScheduleMonthlyMode)
        {
            return ScheduleMonthlyMode == null
                ? false
                : Array.IndexOf(new[] {"dates", "nth"}, ScheduleMonthlyMode.ToString()) >= 0
                    ? true
                    : false;
        }

        /// <summary>
        ///     Validates the parameter 'n' in. See <see cref="ScheduleMonthly.Nth.N" />.
        /// </summary>
        /// <param name="ScheduleNthN">Integer value for the parameter 'n'</param>
        /// <returns>True, if the parameter 'n' was validated successfully.</returns>
        public bool ValidateScheduleNthN(object ScheduleNthN)
        {
            var iN = -10;
            if (!int.TryParse(ScheduleNthN.ToString(), out iN)) return false;

            iN = Convert.ToInt32(ScheduleNthN);

            return Array.IndexOf(new[] {1, 2, 3, 4, 5, -1}, iN) < 0 ? false : true;
        }

        /// <summary>
        ///     Validates the parameter 'what'. See <see cref="ScheduleMonthly.Nth.What" />.
        /// </summary>
        /// <param name="ScheduleNthWhat">Integer value for the parameter 'what'</param>
        /// <returns>True, if the parameter 'what' was validated successfully.</returns>
        public bool ValidateScheduleNthWhat(object ScheduleNthWhat)
        {
            var iN = -1;
            if (!int.TryParse(ScheduleNthWhat.ToString(), out iN)) return false;

            iN = Convert.ToInt32(ScheduleNthWhat);

            return Array.IndexOf(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, iN) < 0 ? false : true;
        }
    }

    /// <summary>
    ///     Subclass of the class Schedule. See <see cref="Schedule.Daily" />
    /// </summary>
    [DataContract]
    public class ScheduleDaily
    {
        /// <summary>Initializes a new instance of the <see cref="ScheduleDaily" /> class.</summary>
        /// <param name="_every">Repeat every next '_every' days</param>
        public ScheduleDaily(int _every = 1)
        {
            Every = _every;
        }

        /// <summary>Initializes a new instance of the <see cref="ScheduleDaily" /> class.</summary>
        public ScheduleDaily()
        {
        }

        /// <summary>
        ///     Repeat every next 'Every' days
        /// </summary>
        [DataMember(Name = "every")]
        public int Every { get; set; }
    }

    /// <summary>
    ///     Subclass of the class Schedule. See <see cref="Schedule.Weekly" />
    /// </summary>
    [DataContract]
    public class ScheduleWeekly
    {
        /// <summary>Initializes a new instance of the <see cref="ScheduleWeekly" /> class.</summary>
        /// <param name="_every">Repeat every week next '_every' days.</param>
        /// <param name="_weekdays">An array of the weekday numbers.</param>
        public ScheduleWeekly(int _every = 1, int[] _weekdays = null)
        {
            Every = _every;
            if (_weekdays != null) Weekdays = _weekdays;
        }

        // <summary>Initializes a new instance of the <see cref="ScheduleWeekly"/> class.</summary>
        public ScheduleWeekly()
        {
        }

        /// <summary>
        ///     Repeat every week next 'Every' days.
        /// </summary>
        [DataMember(Name = "every")]
        public int Every { get; set; }

        /// <summary>
        ///     An array of the weekday numbers.
        /// </summary>
        [DataMember(Name = "weekdays", EmitDefaultValue = false)]
        [Range(1, 7, ErrorMessage = "Weekday must be between 1 and 7")]
        public int[] Weekdays { get; set; }
    }

    /// <summary>
    ///     Subclass of the class Schedule. See <see cref="Schedule.Monthly.Nth" />
    /// </summary>
    [DataContract]
    public class ScheduleMonthlyNth
    {
        /// <summary>Initializes a new instance of the <see cref="ScheduleMonthlyNth" /> class.</summary>
        /// <param name="_n">
        ///     Which day of the time period, 1: 1st, 2: 2nd, 3: 3rd, 4: 4th, 5: 5th, -1: Last/param>
        ///     <param name="_what">
        ///         What time. 1: Monday, 2: Tuesday, 3: Wednesday, 4: Thursday,
        ///         5: Friday, 6: Saturday, 7: Sunday, 8: Day, 9: Working Day, 10: Weekend
        ///     </param>
        public ScheduleMonthlyNth(int _n = 1, int _what = 1)
        {
            N = _n;
            What = _what;
        }

        public ScheduleMonthlyNth()
        {
        }

        /// <summary>
        ///     Which day of the time period, 1: 1st, 2: 2nd, 3: 3rd, 4: 4th, 5: 5th, -1: Last
        /// </summary>
        [DataMember(Name = "n", EmitDefaultValue = false)]
        [CustomValidation(typeof(PropertyValidation), "ValidateMonthlyNthN")]
        public int N { get; set; }

        /// <summary>
        ///     What time. 1: Monday, 2: Tuesday, 3: Wednesday, 4: Thursday, 5: Friday,
        ///     6: Saturday, 7: Sunday, 8: Day, 9: Working Day, 10: Weekend
        /// </summary>
        [DataMember(Name = "what", EmitDefaultValue = false)]
        [Range(1, 10, ErrorMessage = "Wrong value for the What Time parameter")]
        public int What { get; set; }
    }

    /// <summary>
    ///     Subclass of the class Schedule. See <see cref="Schedule.Monthly" />
    /// </summary>
    [DataContract]
    public class ScheduleMonthly
    {
        /// <summary>Initializes a new instance of the <see cref="ScheduleMonthly" /> class.</summary>
        /// <param name="_every">Repeat every month next '_every' days</param>
        /// <param name="_mode">Monthly schedule mode.</param>
        /// <param name="_dates">An array of month days for monthly schedule if the mode 'dates' was chosen</param>
        /// <param name="_nth">Monthly schedule option if the mode 'nth' was chosen</param>
        public ScheduleMonthly(int _every = 1, string _mode = "dates", int[] _dates = null,
            Dictionary<int, int> _nth = null)
        {
            Every = _every;
            Mode = _mode;

            if (_dates != null) Dates = _dates;

            if (_nth != null)
            {
                var _n = -1;
                var _what = -1;

                _nth.ForEach(kv1 =>
                {
                    _n = kv1.Key;
                    _what = kv1.Value;
                });

                if (_n != -1 && _what != -1) Nth = new ScheduleMonthlyNth(_n, _what);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="ScheduleMonthly" /> class.</summary>
        public ScheduleMonthly()
        {
        }

        /// <summary>
        ///     Repeat every month next 'Every' days
        /// </summary>
        [DataMember(Name = "every")]
        public int Every { get; set; }

        /// <summary>
        ///     Monthly schedule mode
        /// </summary>
        [DataMember(Name = "mode")]
        [CustomValidation(typeof(PropertyValidation), "ValidateScheduleMonthlyMode")]
        public string Mode { get; set; }

        /// <summary>
        ///     An array of month days for monthly schedule if the mode 'dates' was chosen.
        /// </summary>
        [DataMember(Name = "dates", EmitDefaultValue = false)]
        [Range(1, 31, ErrorMessage = "Month day must be between 1 and 31")]
        public int[] Dates { get; set; }

        /// <summary>
        ///     Monthly schedule option if the mode 'nth' was chosen
        /// </summary>
        [DataMember(Name = "nth", EmitDefaultValue = false)]
        public ScheduleMonthlyNth Nth { get; set; }
    }

    /// <summary>
    ///     Subclass of the class Schedule. See <see cref="Schedule.Annually" />
    /// </summary>
    [DataContract]
    public class ScheduleAnnually
    {
        /// <summary>
        ///     Repeat every year next 'Every' months.
        /// </summary>
        [DataMember(Name = "every")]
        public int Every { get; set; }

        /// <summary>
        ///     If true, use NTH mode.
        /// </summary>
        [DataMember(Name = "use_nth")]
        public bool UseNth { get; set; }

        /// <summary>
        ///     An array of the month numbers.
        /// </summary>
        [DataMember(Name = "months", EmitDefaultValue = false)]
        [Range(1, 12, ErrorMessage = "Month number must be between 1 and 12")]
        public int[] Months { get; set; }

        /// <summary>
        ///     Annualy schedule option if 'UseNth' is true
        /// </summary>
        [DataMember(Name = "nth", EmitDefaultValue = false)]
        public ScheduleMonthlyNth Nth { get; set; }
    }
}