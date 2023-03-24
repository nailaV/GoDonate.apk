using System.Net.Mail;
using System.Net;

namespace GoDonate.Helpers
{
    public class EmailHelper
    {
      
            public static void Posalji(string to, string messageSubject, string message)
            {
                String SendMailFrom = "go.donate2fa@gmail.com";
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage email = new MailMessage();
                // START
                email.From = new MailAddress(SendMailFrom);
                email.To.Add(to);
                email.CC.Add(SendMailFrom);
                email.Subject = messageSubject;
                email.Body = message;
                //END
                SmtpServer.Timeout = 5000;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "kwaokfxkmpfawbhp");
                SmtpServer.Send(email);

            }
        
    }
}
