using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get schedule calendar of the contacts, orders, routes.
        /// </summary>
        public void GetScheduleCalendar()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            if (!route4Me.MemberHasCommercialCapability(
                    ActualApiKey,
                    DemoApiKey,
                    out string errorString0)
                ) return;

            TimeSpan days5 = new TimeSpan(5, 0, 0, 0);

            var calendarQuery = new ScheduleCalendarQuery()
            {
                DateFromString = (DateTime.Now - days5).ToString("yyyy-MM-dd"),
                DateToString = (DateTime.Now + days5).ToString("yyyy-MM-dd"),
                TimezoneOffsetMinutes = 4 * 60,
                ShowOrders = true,
                ShowContacts = true,
                RoutesCount = true
            };

            var scheduleCalendar = route4Me.GetScheduleCalendar(
                                    calendarQuery,
                                    out string errorString);

            Console.WriteLine(
                (scheduleCalendar?.AddressBook ?? null) == null ||
                (scheduleCalendar?.Orders ?? null) == null ||
                (scheduleCalendar?.RoutesCount ?? null) == null
                    ? "GetScheduleCalendarTest failed"
                    : "GetScheduleCalendar executed successfully"
                );
        }
    }
}
