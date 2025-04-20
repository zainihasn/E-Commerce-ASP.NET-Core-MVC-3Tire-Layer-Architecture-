using DataAccessLayer.Data;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace DataAccessLayer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Product = new MainRepository<Product>(context);
            ProImaRep = new ProImaRep(context);
            Category = new MainRepository<Category>(context);
            ProductReview = new MainRepository<ProductReview>(context);
            ProductImages= new MainRepository<ProductImages>(context);
            Orders = new MainRepository<Orders>(context);
            GuestInfo = new MainRepository<GuestInfo>(context);
            Payment = new MainRepository<Payment>(context);
            CartItem = new MainRepository<CartItem>(context);
            OrderItem = new MainRepository<OrderItem>(context);
            Invoice = new MainRepository<Invoice> (context);
            InvoiceItem= new MainRepository<InvoiceItem>(context);
            Dashboard = new DashboardRepository(context);

        }
        private readonly AppDbContext _context;
        private bool disposed = false;

        public IRepository<Product> Product { get; }
        public IRepository<Category> Category { get; }
        public IRepository<Orders> Orders { get; }
        public IRepository<OrderItem> OrderItem { get; }
        public IRepository<GuestInfo> GuestInfo { get; }
        public IProImaRep ProImaRep { get; }
        public IDashboardRepository Dashboard { get; }

        public IRepository<CartItem> CartItem { get; }
        public IRepository<ProductImages> ProductImages { get; }
        public IRepository<ProductReview> ProductReview { get;  }
        public IRepository<Payment> Payment { get; }
        public IRepository<Invoice> Invoice { get; }
        public IRepository<InvoiceItem> InvoiceItem { get; }
        public int CommitChanges()
        {
            return _context.SaveChanges();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {                 
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }
                disposed = true;
            }
        }

        ~UnitOfWork()
        {
 
            Dispose(false);
        }
    }
}
