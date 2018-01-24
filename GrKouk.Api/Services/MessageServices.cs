
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace GrKouk.Api.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public async Task SendEmailAsync(string emailFrom, string emailTo,string subject, string message)
        {
            // Plug in your email service here to send an email.

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", emailFrom));
            emailMessage.To.Add(new MailboxAddress("", emailTo));
            emailMessage.Subject = subject;

            var bd = new BodyBuilder { HtmlBody = message };
            emailMessage.Body = bd.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "some.domain.com";
                await client.ConnectAsync("localhost", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
            //return Task.FromResult(0);
        }
    }
}
