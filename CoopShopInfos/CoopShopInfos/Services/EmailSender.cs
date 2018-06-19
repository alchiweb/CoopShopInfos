using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace CoopShopInfos.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            //From Address    
            var fromAddress = "marina.philippe@outlook.com";
            var fromAdressTitle = "CoopShopInfos";
            //To Address    
            var toAddress = email;
            var toAdressTitle = "Microsoft ASP.NET Core";
            var messageSubject = subject;
            var bodyContent = message;

            //Smtp Server    
            string SmtpServer = "smtp.live.com";
            //Smtp Port Number    
            int SmtpPortNumber = 587;
            var mimeMessage = new MimeMessage();

            try
            {
                mimeMessage.From.Add(new MailboxAddress
                (fromAdressTitle,
                    fromAddress
                ));
                mimeMessage.To.Add(new MailboxAddress
                (toAdressTitle,
                    toAddress
                ));
                mimeMessage.Subject = messageSubject; //Subject  
                mimeMessage.Body = new TextPart("plain")
                {
                    Text = bodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        "marina.philippe@outlook.com",
                        "@an7*ep5/ma3"
                    );
                    await client.SendAsync(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    Console.ReadLine();
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
