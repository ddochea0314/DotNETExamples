using DnsClient;
using MimeKit;
using System;
using System.Linq;

namespace MailSendExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var lookup = new LookupClient();
            var sendDomain = "senddomain.com";
            var sender = $"noreply@{sendDomain}";
            var recDomain = "gmail.com"; // <<input your mail domain>>
            var recName = "<<input your mail account>>"; // <<input your mail account>>
            var result = lookup.Query(recDomain, QueryType.MX);

            var record = result.Answers.MxRecords().FirstOrDefault();
            var domain = record?.Exchange.Value;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", sender));
            message.To.Add(new MailboxAddress(recName, $"{recName}@{recDomain}"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.LocalDomain = sendDomain;
                client.ServerCertificateValidationCallback = (s, c, h, e) => {
                    return true;
                };
                try
                {
                    client.Connect(domain, 0, MailKit.Security.SecureSocketOptions.Auto);
                }
                catch (Exception ex)
                {

                }
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
