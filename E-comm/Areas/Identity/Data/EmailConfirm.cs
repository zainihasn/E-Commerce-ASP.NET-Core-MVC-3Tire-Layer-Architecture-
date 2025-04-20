using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace E_comm.Areas.Identity.Data
{
    public class EmailConfirm : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
          
            var fmail = "YourEmail@gmail.com"; 
            var appPassword = "Your AppPassword"; 
            var theMsg = new MailMessage();
            theMsg.From = new MailAddress(fmail);
            theMsg.Subject = subject;
            theMsg.To.Add(email);
            theMsg.Body = $"<html><body>{htmlMessage}</body></html>";
            theMsg.IsBodyHtml = true; 

            
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fmail, appPassword),
                EnableSsl = true, 
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 30000
            };

            try
            {          
                await smtp.SendMailAsync(theMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }
    }
}
