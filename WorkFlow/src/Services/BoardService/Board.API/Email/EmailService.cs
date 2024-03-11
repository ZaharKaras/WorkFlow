using Board.API.Email.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Board.API.Email
{
	public class EmailService : IEmailService
	{
		public void SendEmail(string body)
		{
			var message = new MimeMessage();
			message.From.Add(MailboxAddress.Parse("lorenzo52@ethereal.email"));
			message.To.Add(MailboxAddress.Parse("lorenzo52@ethereal.email"));
			message.Subject = "Test email sender.";
			message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

			using var smtp = new SmtpClient();
			smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
			smtp.Authenticate("lorenzo52@ethereal.email", "xkrAxt7yQ5rY7B4m1q");
			smtp.Send(message);
			smtp.Disconnect(true);
		}
	}
}
