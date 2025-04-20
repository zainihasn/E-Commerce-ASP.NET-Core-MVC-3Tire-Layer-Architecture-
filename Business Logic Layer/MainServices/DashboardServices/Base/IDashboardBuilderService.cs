using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.DashboardServices.Base
{
    public interface IDashboardBuilderService
    {
        public DashboardDTO BuildDashboardDTO(
     (List<string> Labels, List<int> OrdersData, List<decimal> SalesData, List<int> UnitsData) chartData,
     (int AvailableProducts, int TotalProducts, int TotalUsers) summary);
    }
}
