using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Model;

namespace BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductImages, ProductImageDTO>().ReverseMap();
            CreateMap<ProductReview, ProductReviewDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Orders, OrdersDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<GuestInfo, GuestInfoDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<InvoiceItem, InvoiceItemDTO>().ReverseMap();
            CreateMap<MonthlyCount, MonthlyCountDTO>().ReverseMap();
            CreateMap<DailyCount, DailyCountDTO>().ReverseMap();
            CreateMap<TimeRange, TimeRangeDTO>().ReverseMap();

            
        }
    }
}

