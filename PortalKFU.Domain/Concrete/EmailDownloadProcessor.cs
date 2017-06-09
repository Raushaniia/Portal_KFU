using System.Net;
using System.Net.Mail;
using System.Text;
using PortalKFU.Domain.Abstract;
using PortalKFU.Domain.Entities;

namespace PortalKFU.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "rusha005@mail.ru";
        public string MailFromAddress = "TheGreateAdel.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "localhost";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\";
    }

    public class EmailDownloadProcessor : IDownloadProcessor
    {
        private EmailSettings emailSettings;

        public EmailDownloadProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessDownload(Library library, DocumentDetails documentInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var line in library.Lines)
                {
                    var subtotal = line.Event.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}",
                        line.Quantity, line.Event.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", library.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(documentInfo.Name)
                    .AppendLine(documentInfo.Line1)
                    .AppendLine(documentInfo.Line2 ?? "")
                    .AppendLine(documentInfo.Line3 ?? "")
                    .AppendLine(documentInfo.Type)
                    .AppendLine(documentInfo.Comment)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}",
                        documentInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ отправлен!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}