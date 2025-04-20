using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.DashboardServices.Base;

namespace BusinessLogicLayer.MainServices.DashboardServices
{
    public class DataFilteringService : IDataFilteringService
    {
        public (List<DailyCountDTO> Orders, List<DailyCountDTO> Sales, List<DailyCountDTO> Units)
            FilterAllData(List<DailyCountDTO> orders, List<DailyCountDTO> sales, List<DailyCountDTO> units,
                          DateTime startDate, DateTime endDate)
        {
            orders = FilterDataByDateRange(orders, startDate, endDate, x => x.Date);
            sales = FilterDataByDateRange(sales, startDate, endDate, x => x.Date);
            units = FilterDataByDateRange(units, startDate, endDate, x => x.Date);
            return (orders, sales, units);
        }

        private List<T> FilterDataByDateRange<T>(List<T> data, DateTime startDate, DateTime endDate, Func<T, DateTime> dateSelector)
        {
            return data.Where(x => dateSelector(x).Date >= startDate && dateSelector(x).Date <= endDate).ToList();
        }
    }
}
