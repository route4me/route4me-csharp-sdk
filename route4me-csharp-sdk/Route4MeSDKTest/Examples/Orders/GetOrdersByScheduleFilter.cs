using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void GetOrdersByScheduleFilter()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateExampleOrder();

            string startDate = (DateTime.Now - (new TimeSpan(1, 0, 0, 0)))
                .ToString("yyyy-MM-dd");
            string endDate = (DateTime.Now + (new TimeSpan(31, 0, 0, 0)))
                .ToString("yyyy-MM-dd");

            var oParams = new OrderFilterParameters()
            {
                Limit = 10,
                Filter = new FilterDetails()
                {
                    Display = "all",
                    Scheduled_for_YYYYMMDD = new string[] { startDate, endDate }
                }
            };

            Order[] orders = route4Me.FilterOrders(oParams, out string errorString);

            PrintExampleOrder(orders, errorString);

            RemoveTestOrders();
        }
    }
}
