using System;
using Hangfire;
using odev3.Models.Mail;
namespace odev3.API.MailService
{
    public class SendMail : ISendMail
    {
        public void SendMailToUser(EmailModel email)
        {
            BackgroundJob.Schedule(
            () => Console.WriteLine("Welcome {0} {0}", email.To, email.Comment),
            TimeSpan.FromDays(1));
        }
    }
}