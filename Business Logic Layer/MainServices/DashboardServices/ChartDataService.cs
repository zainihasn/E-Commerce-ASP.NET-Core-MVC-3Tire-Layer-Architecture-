using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.DashboardServices.Base;

namespace BusinessLogicLayer.MainServices.DashboardServices
{
    public class ChartDataService : IChartDataService
    {

        private readonly ITimeUnitProvider _timeUnitProvider;

       
        public ChartDataService(ITimeUnitProvider timeUnitProvider)
        {
            _timeUnitProvider = timeUnitProvider;
        }

        public (List<string> Labels, List<int> OrdersData, List<decimal> SalesData, List<int> UnitsData)
            GenerateChartData((List<DailyCountDTO> Orders, List<DailyCountDTO> Sales, List<DailyCountDTO> Units) data,
                                DateTime startDate, DateTime endDate)
        {
       
            string dateFormat = _timeUnitProvider.GetUnitLabel() == "شهري" ? "MM/yyyy" : "dd/MM";

            var dates = GetDistinctDates(data.Orders, data.Sales, data.Units);
            var orderDict = GetOrderDictionary(data.Orders);
            var salesDict = GetSalesDictionary(data.Sales);
            var unitsDict = GetUnitsDictionary(data.Units);

            List<string> labels = new();
            List<int> ordersData = new();
            List<decimal> salesData = new();
            List<int> unitsData = new();

            foreach (var date in dates)
            {
                labels.Add(date.ToString(dateFormat, new CultureInfo("ar")));
                ordersData.Add(orderDict.TryGetValue(date, out var c) ? c : 0);
                salesData.Add(salesDict.TryGetValue(date, out var s) ? s : 0);
                unitsData.Add(unitsDict.TryGetValue(date, out var u) ? u : 0);
            }

            return (labels, ordersData, salesData, unitsData);
        }



        private List<DateTime> GetDistinctDates(List<DailyCountDTO> dailyOrders, List<DailyCountDTO> dailySales, List<DailyCountDTO> dailyUnits)
        {
            return dailyOrders.Select(x => x.Date.Date)
                .Union(dailySales.Select(x => x.Date.Date))
                .Union(dailyUnits.Select(x => x.Date.Date))
                .Distinct()
                .OrderBy(d => d)
                .ToList();
        }

        private Dictionary<DateTime, int> GetOrderDictionary(List<DailyCountDTO> dailyOrders)
        {
            return dailyOrders.ToDictionary(x => x.Date.Date, x => x.Count);
        }

        private Dictionary<DateTime, decimal> GetSalesDictionary(List<DailyCountDTO> dailySales)
        {
            return dailySales.ToDictionary(x => x.Date.Date, x => x.Total);
        }

        private Dictionary<DateTime, int> GetUnitsDictionary(List<DailyCountDTO> dailyUnits)
        {
            return dailyUnits.ToDictionary(x => x.Date.Date, x => x.TotalUnits);
        }

        private string GetDateFormat(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).TotalDays <= 31 ? "dd/MM" : "MM/yyyy";
        }
    }
}
