using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Tools.SearchAgents
{
    public class MailService
    {
        public static void SendMessage(string receiver, string content)
        {
            var fromAddress = new MailAddress("fromserviceworkua@gmail.com", "From Work.At.Ua");
            var toAddress = new MailAddress(receiver, receiver);
            string fromPassword = "vova2411";
            string subject = "Search result from work.at.ua";
            string body = content;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
