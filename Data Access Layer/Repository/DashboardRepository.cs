using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Orders> _dbSet;
        private readonly DbSet<Product> _dbSet2;
        private readonly DbSet<OrderItem> _dbSet1;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Orders>();
            _dbSet2 = _context.Set<Product>();
            _dbSet1 = _context.Set<OrderItem>();
        }

        public async Task<int> CountProductAsync()
        {
            return await _dbSet2.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _dbSet2.Where(predicate).CountAsync();
        }

        public async Task<List<MonthlyCount>> GetMonthlyOrderCountsAsync()
        {
            return await _dbSet
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new MonthlyCount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();
        }

       

        public async Task<List<MonthlyCount>> GetMonthlyOrderStatsAsync()
        {
            return await _dbSet
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new MonthlyCount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();
        }

        public async Task<List<MonthlyCount>> GetMonthlyTotalSalesStatsAsync(Expression<Func<OrderItem, decimal>> selector)
        {
            return await _dbSet1
        .GroupBy(oi => new { oi.Order.OrderDate.Year, oi.Order.OrderDate.Month })
        .Select(g => new MonthlyCount
        {
            Year = g.Key.Year,
            Month = g.Key.Month,
            // هنا استخدمنا Select للتعامل مع Group
            Total = g.Sum(oi => oi.Quantity * (decimal)oi.Price)  // نطبق Sum بعد تحديد الحقول
        })
        .ToListAsync();
        }
        

        public async Task<List<MonthlyCount>> GetMonthlyTotalUnitySoldStatsAsync(Expression<Func<OrderItem, int>> selector)
        {
            return await _dbSet1
                .Where(oi => oi.Order != null)
                .GroupBy(oi => new { oi.Order.OrderDate.Year, oi.Order.OrderDate.Month })
                .Select(g => new MonthlyCount
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalUnits = g.Sum(oi => oi.Quantity )
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();
        }

    
        public async Task<List<DailyCount>> GetDailyOrderCountsAsync()
        {
            return await _dbSet
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new DailyCount
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

      
        public async Task<List<DailyCount>> GetDailySalesStatsAsync(Expression<Func<OrderItem, decimal>> selector)
        {
            return await _dbSet1
                .Where(oi => oi.Order != null)
                .GroupBy(oi => oi.Order.OrderDate.Date)
                .Select(g => new DailyCount
                {
                    Date = g.Key,
                    Total = g.Sum(oi => oi.Quantity * (decimal)oi.Price)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }


        public async Task<List<DailyCount>> GetDailyUnitsSoldStatsAsync(Expression<Func<OrderItem, int>> selector)
        {
            return await _dbSet1
                .Where(oi => oi.Order != null)
                .GroupBy(oi => oi.Order.OrderDate.Date)
                .Select(g => new DailyCount
                {
                    Date = g.Key,
                    TotalUnits = g.Sum(oi => oi.Quantity)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }






    }
}
