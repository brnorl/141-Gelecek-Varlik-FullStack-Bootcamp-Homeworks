using odev3.Models.Mail;

namespace odev3.API.MailService
{
    public interface ISendMail
    {
        public void SendMailToUser(EmailModel model);
    }
}