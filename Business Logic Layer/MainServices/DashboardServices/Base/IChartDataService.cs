using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.DashboardServices.Base
{
    public interface IChartDataService
    {
        (List<string> Labels, List<int> OrdersData, List<decimal> SalesData, List<int> UnitsData)
               GenerateChartData((List<DailyCountDTO> Orders, List<DailyCountDTO> Sales, List<DailyCountDTO> Units) data,
                               DateTime startDate, DateTime endDate);
    }
}