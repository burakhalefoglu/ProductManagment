using System;
using System.Linq;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;

namespace Core.Utilities.Mail
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();

            try
            {
                Console.WriteLine("📤 Mail hazırlığı başlıyor...");

                if (emailMessage.FromAddresses == null || !emailMessage.FromAddresses.Any())
                    throw new Exception("Gönderen adres boş!");

                if (emailMessage.ToAddresses == null || !emailMessage.ToAddresses.Any())
                    throw new Exception("Alıcı adres boş!");

                // FROM
                message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                // TO
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                // REPLY-TO
                message.ReplyTo.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                // SUBJECT
                message.Subject = string.IsNullOrWhiteSpace(emailMessage.Subject) ? "Işık Medya - Bilgilendirme" : emailMessage.Subject;

                // MESSAGE-ID
                message.MessageId = MimeUtils.GenerateMessageId("isik.media");
                var textPart = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Content,
                    ContentTransferEncoding = ContentEncoding.QuotedPrintable
                };
                // Charset'i doğrudan ContentType üzerinden güncelle
                textPart.ContentType.Charset = "UTF-8";
                message.Body = textPart;

                using var emailClient = new SmtpClient();

                Console.WriteLine("🔌 SMTP bağlantısı deneniyor...");
                emailClient.LocalDomain = "isik.media";

                emailClient.Connect(
                    _configuration["EmailConfiguration:SmtpServer"],
                    Convert.ToInt32(_configuration["EmailConfiguration:SmtpPort"]),
                    SecureSocketOptions.StartTls
                );
                Console.WriteLine("✅ SMTP bağlantısı kuruldu.");

                emailClient.Authenticate(
                    _configuration["EmailConfiguration:Username"],
                    _configuration["EmailConfiguration:Password"]
                );
                Console.WriteLine("🔐 SMTP kimlik doğrulama başarılı.");

                emailClient.Send(message);
                Console.WriteLine("✅ Mail gönderildi.");

                emailClient.Disconnect(true);
                Console.WriteLine("🔌 SMTP bağlantısı kapatıldı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔴 SMTP hatası: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("↪️ İç Hata: " + ex.InnerException.Message);

                throw; // hata üst katmana çıksın
            }
        }
    }
}
