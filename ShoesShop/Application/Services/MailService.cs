using System.Net;
using System.Net.Mail;
using ShoesShop.Application.Interfaces.Services;

namespace ShoesShop.Application.Services
{
    public class MailService : IEmailService
    {

        private readonly IConfiguration _config;
        public MailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendMailAsync(string address, string subject, string body)
        {
            var smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress("noreply@shoesshop.com", "ShoesShop Support"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mail.To.Add(address);

            await smtpClient.SendMailAsync(mail);
        }
    }
}
