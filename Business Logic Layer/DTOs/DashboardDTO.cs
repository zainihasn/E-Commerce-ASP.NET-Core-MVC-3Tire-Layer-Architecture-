using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class DashboardDTO
    {
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalUnitySold { get; set; }
        public int AvailableProducts { get; set; }
        public int TotalProducts { get; set; }
        public List<string> Labels { get; set; } = new();
        public List<int> OrdersData { get; set; } = new();
        public List<decimal> SalesData { get; set; } = new();
    }


    public class TimeRangeDTO
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public static TimeRangeDTO Last7Days()
        {
            var today = DateTime.UtcNow.Date;
            return new TimeRangeDTO { From = today.AddDays(-30), To = today };
        }

        public static TimeRangeDTO Last30Days()
        {
            var today = DateTime.UtcNow.Date;
            return new TimeRangeDTO { From = today.AddDays(-9), To = today };
        }

        public static TimeRangeDTO LastYear()
        {
            var today = DateTime.UtcNow.Date;
            return new TimeRangeDTO { From = today.AddYears(-1), To = today };
        }

        public static TimeRangeDTO Custom(DateTime from, DateTime to)
        {
            return new TimeRangeDTO { From = from.Date, To = to.Date };
        }
    }








    public class MonthlyCountDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
        public int TotalUnits { get; set; }

    }
    public class DailyCountDTO
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }         
        public decimal Total { get; set; }     
        public int TotalUnits { get; set; }    
    }

}
