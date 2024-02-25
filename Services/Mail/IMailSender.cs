namespace invenio.Services.Mail;

public interface IMailSender
{
        Task SendMail(Mail mail);
}