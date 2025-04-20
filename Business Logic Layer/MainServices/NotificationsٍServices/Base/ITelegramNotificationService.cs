using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.MainServices.NotificationsٍServices.Base
{
    public interface ITelegramNotificationService
    {
        Task SendMessageAsync(string htmlContent);
    }

}
