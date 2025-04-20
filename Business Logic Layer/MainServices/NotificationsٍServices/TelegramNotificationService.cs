using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.NotificationsٍServices.Base;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace BusinessLogicLayer.MainServices.NotificationsٍServices
{
    public class TelegramNotificationService : ITelegramNotificationService
    {
        private readonly string _botToken = "7847324428:AAGkI8NYhErudCQRc1FGddkIvo2NrmRUnAQ";
        private readonly string _chatId = "-1002525256561";

        public async Task SendMessageAsync(string htmlContent)
        {
            var botClient = new TelegramBotClient(_botToken);
            var tempFilePath = Path.Combine(Path.GetTempPath(), $"invoice_{Guid.NewGuid()}.html");
            await File.WriteAllTextAsync(tempFilePath, htmlContent, Encoding.UTF8);

            using (var stream = File.OpenRead(tempFilePath))
            {
                var inputFile = new InputFileStream(stream, "invoice.html");

                await botClient.SendDocumentAsync(
                    chatId: _chatId,
                    document: inputFile,
                    caption: "📄 فاتورة HTML",
                    parseMode: ParseMode.Html
                );
            }

            File.Delete(tempFilePath);
        }
    }

}
