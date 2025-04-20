using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using static DataAccessLayer.Model.Invoice;

namespace DataAccessLayer.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Product { get; }
        IRepository<Category> Category { get; }
        IRepository<Orders> Orders { get; }
        IRepository<OrderItem> OrderItem { get; }
        IRepository<CartItem> CartItem { get; }
        IRepository<ProductImages> ProductImages { get; }
        IRepository <ProductReview> ProductReview { get; }
        IRepository<Payment> Payment { get; }
        IRepository<GuestInfo> GuestInfo { get; }
        IProImaRep ProImaRep { get; }
        IRepository<Invoice> Invoice { get; }
        IRepository<InvoiceItem> InvoiceItem { get; }
        IDashboardRepository Dashboard { get; }
        int CommitChanges();

    }
}
