using MailKit.Net.Smtp;
using MimeKit;

namespace invenio.Services.Mail;

public class MailSender : IMailSender
{
    private readonly MailConfiguration _mailConfig;
    public MailSender(MailConfiguration mailConfig)
    {
        _mailConfig = mailConfig;
    }
    public async Task SendMail(Mail mail)
    {
        var emailMessage = CreateEmailMessage(mail);
        await SendAsync(emailMessage);
    }
    
    private MimeMessage CreateEmailMessage(Mail mail)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_mailConfig.From, _mailConfig.From));
        emailMessage.To.AddRange(mail.To);
        emailMessage.Subject = mail.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = mail.Content };

        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
{
    using (var client = new SmtpClient())
    {
        try
        {
            await client.ConnectAsync(_mailConfig.SmtpServer, _mailConfig.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_mailConfig.Username, _mailConfig.Password);

            await client.SendAsync(mailMessage);
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
}