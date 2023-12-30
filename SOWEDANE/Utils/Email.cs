using System.Net;
using System.Net.Mail;

namespace SOWEDANE.Utils
{
    public class Email
    {
        //private static readonly IConfiguration configuration;
       

        static void send(IConfiguration configuration, string subject, string emailBody, string to)
        {

            string senderEmail = configuration.GetValue<String>("AppSettings:noreplyEmail") ;
            string senderPassword = configuration.GetValue<String>("AppSettings:noreplyPassword");
            string smptHost = configuration.GetValue<String>("AppSettings:smptHost");
            int smtpPort = configuration.GetValue<int>("AppSettings:smtpPort");


            // Create a MailMessage object
            MailMessage mail = new MailMessage(senderEmail, to);

            // Subject and body of the email
            mail.Subject = subject;
            mail.Body = emailBody;

            // Create a SmtpClient
            SmtpClient smtpClient = new SmtpClient(smptHost);
            smtpClient.Port = smtpPort;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            // Send the email
            smtpClient.Send(mail);

        }
    }
}
