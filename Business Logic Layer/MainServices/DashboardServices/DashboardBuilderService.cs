using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.DashboardServices.Base;

namespace BusinessLogicLayer.MainServices.DashboardServices
{
    public class DashboardBuilderService : IDashboardBuilderService
    {

        public DashboardDTO BuildDashboardDTO(
            (List<string> Labels, List<int> OrdersData, List<decimal> SalesData, List<int> UnitsData) chartData,
            (int AvailableProducts, int TotalProducts, int TotalUsers) summary)
        {
            return new DashboardDTO
            {
                Labels = chartData.Labels,
                OrdersData = chartData.OrdersData,
                SalesData = chartData.SalesData,
                TotalUnitySold = chartData.UnitsData.Sum(),
                TotalOrders = chartData.OrdersData.Sum(),
                TotalSales = chartData.SalesData.Sum(),
                AvailableProducts = summary.AvailableProducts,
                TotalProducts = summary.TotalProducts,
                TotalUsers = summary.TotalUsers
            };
        }
    }
}
