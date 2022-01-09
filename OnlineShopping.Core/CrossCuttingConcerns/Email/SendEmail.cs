using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Core.CrossCuttingConcerns.Email
{
    public static class SendEmail
    {

        public static void Send(string to, string subject, string body)
        {
            MailMessage message = new("danego62@gmail.com", to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            SmtpClient gmailClient = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("danego62@gmail.com", "emorfon61")
            };
            gmailClient.Send(message);
        }

    }
}
