using Login.Domain.Model;
using Login.Infrrastructure.Interface;
using System.Net;
using System.Net.Mail;

namespace Login.Infrrastructure
{
    public class RecoverPasswordInfrastructure : IRecoverPasswordInfrastructure
    {
        public void SendNewPasswordToEmail(UserLoginDomain user, EmailConfigurationDomain credencials)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(credencials.EmailCredencial);
            mail.To.Add(user.Email);
            mail.IsBodyHtml = true;
            mail.Subject = "Recuperação de Senha";
            mail.Body = @"<h2>Foi solicitado uma recuperação de senha! Favor alterar a senha no primeiro acesso!</h2> <br/> 
                           Nome:  " + user.FirstName + " " + user.LastName + "<br/>" +
                         " Email : " + user.Email + " <br/>" +
                         " Nova Senha : " + user.UserPassword + "<br/> <br/> <br/>" +
                         "<h3>Mensagem Automatica, não respoder esse Email!</h3> <br/>";

            SmtpClient client = new SmtpClient(credencials.SmtpClient);
            client.Port = credencials.Port;
            client.EnableSsl = credencials.EnableSsl;
            client.UseDefaultCredentials = credencials.UseDefaultCredentials;
            client.Credentials = new NetworkCredential(credencials.EmailCredencial, credencials.PasswordCredencial);

            client.Send(mail);
        }
    }
}
