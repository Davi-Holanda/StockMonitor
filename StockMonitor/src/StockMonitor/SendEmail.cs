using System.Net;
using System.Net.Mail;

namespace StockMonitor;
public class SendEmail
{
    public static void Send(EmailSettings settings){
        Console.WriteLine("Montando email...");

        MailMessage mail = new MailMessage();
        var smtpClient = new SmtpClient(settings.host, settings.port);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(settings.email_from_user, settings.email_from_password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        mail.From = new MailAddress(settings.email_from_address);
        foreach(string email in settings.emails_to)
            mail.To.Add(email);
        mail.Subject = settings.subject;
        mail.Body = settings.body;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.Normal;

        Console.WriteLine("Enviado email...");

        smtpClient.Send(mail);

        Console.WriteLine("Email enviado");
    }

}