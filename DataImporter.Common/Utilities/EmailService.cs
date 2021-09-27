using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _useSSL;
        private readonly string _from;
        private readonly EmailSettings _emailSettings;
     
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;

            _host = _emailSettings.host;
            _port = _emailSettings.port;
            _username = _emailSettings.username;
            _password = _emailSettings.password;
            _useSSL = _emailSettings.useSSL;
            _from = _emailSettings.from;
        }
        public void SendEmail(string receiver, string subject, string body, string filePath = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_from, _from));
            message.To.Add(new MailboxAddress(receiver, receiver));
            message.Subject = subject;

            if(filePath==null)
            {
                message.Body = new TextPart("html")
                {
                    Text = body,
                };
            }
            else
            {
                var builder = new BodyBuilder();
                builder.TextBody = body;
                builder.Attachments.Add(filePath);
                message.Body = builder.ToMessageBody();
            }

            using var client = new SmtpClient();
            client.Timeout = 60000;
            client.Connect(_host, _port, _useSSL);
            client.Authenticate(_username, _password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
