using Microsoft.Extensions.Configuration;
using Registration.Domein.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Application.Services.EmailService
{
    public class EmailService : IEmileService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(EmailModel model)
        {
            /*string path = @"C:\Users\User\OneDrive\Ishchi stol\FrontEnd\1-\index.html";

            using (var stream = new StreamReader(path))
            {
                model.Body = await stream.ReadToEndAsync();
            }*/

            var emailSettings = _config.GetSection("EmailSettings");
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
                Subject = model.Subject,
                Body = model.Body,
                IsBodyHtml = true,

            };
            mailMessage.To.Add(model.To);

            using var smtpClient = new SmtpClient(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]))
            {
                Port = Convert.ToInt32(emailSettings["MailPort"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
                EnableSsl = true,
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
