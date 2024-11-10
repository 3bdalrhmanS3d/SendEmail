using System;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace testEmail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string toEmail = "";
            string fromEmail = "";
            string fromPassword = "";  

            try
            {
                Console.WriteLine("Attempting to send email...");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", fromEmail));
                message.To.Add(new MailboxAddress("Recipient Name", toEmail));
                message.Subject = "Test Email using MailKit";

                message.Body = new TextPart("plain")
                {
                    Text = "This is a test email sent using MailKit to verify SMTP setup."
                };

                using (var client = new SmtpClient())
                {
                    try
                    {
                        client.Connect("smtp.mail.yahoo.com", 587, SecureSocketOptions.StartTls);
                    }
                    catch
                    {
                        client.Connect("smtp.mail.yahoo.com", 465, SecureSocketOptions.SslOnConnect);
                    }

                    client.Authenticate(fromEmail, fromPassword);

                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught while sending email: {0}", ex.ToString());
            }
        }
    }
}
