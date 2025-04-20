using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.NotificationsٍServices.Base;

namespace BusinessLogicLayer.MainServices.NotificationsٍServices
{
    public class EmailNotificationService : IEmailNotificationService
    {
        public async Task SendOrderEmailToAdmin(string htmlContent)
        {
            var adminEmail = "Admin@gmail.com";
            var subject = $"طلب جديد من ضيف - رقم: /**/";
            var tempFilePath = Path.Combine(Path.GetTempPath(), $"invoice_{Guid.NewGuid()}.html");
            await File.WriteAllTextAsync(tempFilePath, htmlContent, Encoding.UTF8);

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("YourEmail@gmail.com", "YourAppPassword");//you must use AppPassword,
                                                                                                   //DoNot use yor gmail PassWord,
                                                                                                   //go to your gmail and add new AppPassword
                                                                                                   //and use ti here
                smtp.EnableSsl = true;

                var mail = new MailMessage
                {
                    From = new MailAddress("YourEmail@gmail.com"),
                    Subject = subject,
                    Body = "تم إنشاء طلب جديد. مرفق تفاصيل الفاتورة بصيغة HTML.",
                    IsBodyHtml = false
                };

                mail.To.Add(adminEmail);
                mail.Attachments.Add(new Attachment(tempFilePath, "text/html") { Name = "invoice.html" });

                await smtp.SendMailAsync(mail);
            }

            File.Delete(tempFilePath);
        }
    }

}
