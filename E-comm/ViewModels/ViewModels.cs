using BusinessLogicLayer.DTOs;

namespace E_comm.ViewModels
{
    public class ProductDetailsViewModelCartItems
    {
        public GuestInfoDTO GuestInfoDTO { get; set; }
        public PaymentDTO PaymentDTO { get; set; }

        public decimal TotalAmount { get; set; }
        int PaymentMethod { get; set; }
    }


    public class ProductWithImagesViewModelHome
    {
        public ProductDTO Product { get; set; }
        public ProductImageDTO Images { get; set; }
    }


    public class CategoryProductViewModelHome
    {
        public List<ProductDTO> Products { get; set; }
        public List<ProductImageDTO> Images { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public string? SearchTerm { get; set; }
        public List<ProductWithImagesViewModelHome> TopSellingProducts { get; set; }
        public List<ProductWithImagesViewModelHome> SearchResults { get; set; }
    }


    public class ProductWithImagesViewModelProduct
    {
        public ProductDTO Product { get; set; }
        public ProductImageDTO Images { get; set; }
    }


    public class ProductDetailsViewModelProduct
    {
        public ProductDTO Product { get; set; }
        public List<ProductImageDTO> imageDTO { get; set; }
        public List<ProductReviewDTO> reviewDTO { get; set; }
        public List<ProductWithImagesViewModelProduct> SimilarProducts { get; set; }
        public ProductReviewDTO Review { get; set; }

    }


    public class InvoiceDetailsViewModel
    {
        public InvoiceDTO Invoice { get; set; }
        public List<ProductDetailsInvoiceItemAdmin> Items { get; set; } = new();
    }

    public class ProductDetailsInvoiceItemAdmin
    {
        public InvoiceItemDTO InvoiceItem { get; set; }
        public ProductDTO Product { get; set; }
    }


    public class OrderDetailsViewModel
    {
        public OrdersDTO Order { get; set; }
        public GuestInfoDTO GuestInfo { get; set; }  // معلومات العميل (إذا كانت موجودة)
        public PaymentDTO Payment { get; set; }  // تفاصيل الدفع
        public List<OrderItemWithProductViewModel> OrderItems { get; set; } = new();  // عناصر الطلب مع المنتج
     // إجمالي المبلغ
    }

    public class OrderItemWithProductViewModel
    {
        public OrderItemDTO OrderItem { get; set; }
        public ProductDTO Product { get; set; }  // معلومات المنتج المرتبطة بالطلب
        public decimal TotalAmount => OrderItem.Quantity * OrderItem.Price; // إجمالي المبلغ لهذا العنصر

    }


        public class ProductDetailsViewModelHomeAdmin
    {
        public ProductDTO Product { get; set; }
        public ProductImageDTO imageDTO { get; set; }
        public CategoryDTO Category { get; set; }
    }

    public class ProductImagesDetailsViewModelAdminProductImages
    {
        public ProductDTO Product { get; set; }
        public ProductImageDTO imageDTO { get; set; }
        int ProducName { get; set; }
    }

    public class DashboardFilterViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DashboardDTO DashboardData { get; set; }
        public List<ProductWithImagesViewModelHome> LowStockProduct { get; set; }
        public List<ProductWithImagesViewModelHome> TopSellingProducts { get; set; }
        public ProductImageDTO imageDTO { get; set; }
    }


}
