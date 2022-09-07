using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmailService
    {
        public static void SendEmail(string email,string token, string firstName)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("phanindratestapi@gmail.com", "pvmkixdsveigvbyd");
                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(firstName);
                msgObj.IsBodyHtml = true;
                msgObj.From = new MailAddress("phanindratestapi@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = "<html><body><p><b>Hi " + $"{firstName}" + " </b>,<br/>Please click the below link for reset password.<br/>" +
                              $"{token}" +
                              "<br/><br/><br/><b>Thanks&Regards </b><br/><b>Mail Team(donot - reply to this)</b></p></body></html>";
                client.Send(msgObj);
            }
        }
    }
}