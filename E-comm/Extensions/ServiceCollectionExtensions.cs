using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using BusinessLogicLayer.Service;
using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.MainServices;
using DataAccessLayer.Repository.Base;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using E_comm.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using BusinessLogicLayer.Service.CartItemServices.Base;
using BusinessLogicLayer.Service.CartItemServices;
using BusinessLogicLayer.Service.CartItemSerVice.Base;
using BusinessLogicLayer.Service.CartItemSerVice;
using BusinessLogicLayer.Service.CheckoutService.Base;
using BusinessLogicLayer.MainServices.GusetServices.Base;
using BusinessLogicLayer.MainServices.GusetServices;
using BusinessLogicLayer.MainServices.InvoiceServices.Base;
using BusinessLogicLayer.MainServices.InvoiceServices;
using BusinessLogicLayer.MainServices.NotificationsٍServices.Base;
using BusinessLogicLayer.MainServices.NotificationsٍServices;
using BusinessLogicLayer.MainServices.OrdersServices.Base;
using BusinessLogicLayer.MainServices.OrdersServices;
using BusinessLogicLayer.Service.OrdersServices.Base;
using BusinessLogicLayer.Service.OrdersServices;
using BusinessLogicLayer.Service.PaymentServices.Base;
using BusinessLogicLayer.Service.PaymentServices;
using BusinessLogicLayer.MainServices.PaymentServices.Base;
using BusinessLogicLayer.MainServices.PaymentServices;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;
using BusinessLogicLayer.MainServices.ProductsServices;
using Stripe;
using ProductService = BusinessLogicLayer.Service.ProductService;
using InvoiceService = BusinessLogicLayer.Service.InvoiceService;
using BusinessLogicLayer.MainServices.Base;
using BusinessLogicLayer.MainServices.DashboardServices.Base;
using BusinessLogicLayer.MainServices.DashboardServices;

namespace E_comm.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartCookieService, CartCookieService>();
            services.AddScoped<ICartItemEditorService, CartItemEditorService>();
            services.AddScoped<ICartManagerService, CartManagerService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<ICheckoutServices,CheckoutServices>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IGuestInfoService, GuestInfoService>();
            services.AddScoped<IGuestManagerService, GuestManagerService>();
            services.AddScoped<IInvoiceCreationService, InvoiceCreationService>();
            services.AddScoped<IInvoiceHtmlGeneratorService, InvoiceHtmlGeneratorService>();
            services.AddScoped<IInvoicePersistenceService, InvoicePersistenceService>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<ITelegramNotificationService, TelegramNotificationService>();
            services.AddScoped<IOrderCreationService, OrderCreationService>();
            services.AddScoped<IOrderPersistenceService, OrderPersistenceService>();
            services.AddScoped<IOrderProcessorService, OrderProcessorService>();
            services.AddScoped<IPaymentHandler, PaymentHandler>();
            services.AddScoped<IPaymentPersistenceService, PaymentPersistenceService>();
            services.AddScoped<IPaymentProcessorService, PaymentProcessorService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IProductManagementService, ProductManagementService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IProductStockService, ProductStockService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<INotifications, Notifications>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddTransient<IEmailSender, EmailConfirm>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IDashboardBuilderService, DashboardBuilderService>();
            services.AddScoped<IDataFilteringService, DataFilteringService>();
            services.AddScoped<ISummaryService, SummaryService>();
            services.AddScoped<IChartDataService, ChartDataService>();
            services.AddScoped<ITimeUnitProvider, TimeUnitProvider>();
            return services;
        }

        public static IServiceCollection AddProjectDb(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();
            return services;
        }

        public static IServiceCollection AddProjectIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
