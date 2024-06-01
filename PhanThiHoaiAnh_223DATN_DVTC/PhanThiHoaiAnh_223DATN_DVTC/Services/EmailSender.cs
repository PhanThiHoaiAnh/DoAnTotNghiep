using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PhanThiHoaiAnh_223DATN_DVTC.Models;
using System.Net.Mail;
using System.Net;

namespace PhanThiHoaiAnh_223DATN_DVTC.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                // Configure the SMTP client settings
                client.Host = "smtp.gmail.com";//smtp.gmail.com
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("dvtc.vanphan@gmail.com", "pmousvlsfuwrwual");

                // Create the email message
                var mailMessage = new MailMessage("dvtc.vanphan@gmail.com", email, subject, message);

                // Send the email
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
