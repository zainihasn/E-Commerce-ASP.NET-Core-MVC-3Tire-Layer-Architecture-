using BusinessLogicLayer.Service.Base;
using BusinessLogicLayer.MainServices.NotificationsٍServices.Base;



namespace BusinessLogicLayer.Service
{
    public class Notifications : INotifications
    {
        private readonly ITelegramNotificationService _telegramService;
        private readonly IEmailNotificationService _emailService;

        public Notifications(ITelegramNotificationService telegramService, IEmailNotificationService emailService)
        {
            _telegramService = telegramService;
            _emailService = emailService;
        }


        public async Task SendMessageAsync(string htmlContent) => await _telegramService.SendMessageAsync(htmlContent);     
        public async Task SendOrderEmailToAdmin(string htmlContent) => await _emailService.SendOrderEmailToAdmin(htmlContent);
        
    }

}
