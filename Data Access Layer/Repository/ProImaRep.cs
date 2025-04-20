using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Data;
using DataAccessLayer.Repository.Base;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Model;
using System.Linq.Expressions;

namespace DataAccessLayer.Repository
{
    public class ProImaRep : MainRepository<Product>,IProImaRep
    {
        public ProImaRep(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
            _dbSet1 = _context.Set<ProductReview>();
            _dbSet3 = _context.Set<GuestInfo>();
        }
        private readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;
        private readonly DbSet<ProductReview> _dbSet1;
        private readonly DbSet<GuestInfo> _dbSet3;



        public async Task<Product?> GetProductRatingById(int productId)
        {
            return await _dbSet
                .Include(p => p.ProductReview) 
                .FirstOrDefaultAsync(p => p.ProductId == productId); 
        }
        public async Task<List<Product>> GetProductsBySearchAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _dbSet.ToListAsync();

      

            return await _dbSet
                .Where(p =>
                    p.ProductName.Contains(search) ||
                    p.CompanyName.Contains(search) ||
                    p.ShortDescription.Contains(search))
                .ToListAsync();
        }

        public async Task<List<Product>> GetTopSellingProductsAsync(int topCount)
        {
            return await _dbSet
                .OrderByDescending(p => p.Sold)
                .Take(topCount)
                .ToListAsync();
        }
        public async Task<List<ProductReview>> GetProductReviewsWithUserAsync(int ProductId)
        {
            return await _dbSet1
                .Where(p => p.ProductId == ProductId)
                .Include(p => p.guestInfo) 
                .ToListAsync();
        }

        public async Task<List<Product>> GetLowStockProductsAsync(int threshold)
        {
            return await _dbSet
                .Where(p => p.Quantity <= threshold && p.IsActive)
                .ToListAsync();
        }
        public Task<GuestInfo> FindOneAsync(Expression<Func<GuestInfo, bool>> predicate)
        {
            return _dbSet3.FirstOrDefaultAsync(predicate);
        }
     

    }
}


