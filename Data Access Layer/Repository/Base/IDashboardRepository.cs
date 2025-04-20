using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;

namespace DataAccessLayer.Repository.Base
{
   
        public interface IDashboardRepository
        {
        Task<int> CountProductAsync();
        Task<int> CountAsync(Expression<Func<Product, bool>> predicate);

        Task<List<MonthlyCount>> GetMonthlyOrderCountsAsync();
        Task<List<DailyCount>> GetDailyOrderCountsAsync();

        Task<List<MonthlyCount>> GetMonthlyOrderStatsAsync();

        Task<List<MonthlyCount>> GetMonthlyTotalSalesStatsAsync(Expression<Func<OrderItem, decimal>> selector);
        Task<List<MonthlyCount>> GetMonthlyTotalUnitySoldStatsAsync(Expression<Func<OrderItem, int>> selector);
        Task<List<DailyCount>> GetDailySalesStatsAsync(Expression<Func<OrderItem, decimal>> selector);
        Task<List<DailyCount>> GetDailyUnitsSoldStatsAsync(Expression<Func<OrderItem, int>> selector);

    }


}
