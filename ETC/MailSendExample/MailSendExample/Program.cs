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
            Console.WriteLine("start");
            var lookup = new LookupClient();
            var sendDomain = "senddomain.com";
            var sender = $"noreply@{sendDomain}";
            var recDomain = "naver.com"; // <<input your mail domain>>
            var recName = "ddochea0314"; // <<input your mail account>>
            var result = lookup.Query(recDomain, QueryType.MX);

            var record = result.Answers.MxRecords().OrderBy(mx => mx.Preference).FirstOrDefault();
            var domain = record?.Exchange.Value;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    while (true)
                    {
                        client.Connect(domain, 25, MailKit.Security.SecureSocketOptions.None);
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
                        client.Send(message);

                        Console.WriteLine("send done.");
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
