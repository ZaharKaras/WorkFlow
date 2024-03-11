using Board.Core.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Board.Infrastructure.Email
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _config;

		public EmailService(IConfiguration configuration)
		{
			_config = configuration;
		}

		public void SendEmail(string to, string subject, string body)
		{
			var user = _config.GetSection("Email:UserName").Value;
			var password = _config.GetSection("Email:Password").Value;
			var host = _config.GetSection("Email:Host").Value;

			var message = new MimeMessage();
			message.From.Add(MailboxAddress.Parse(user));
			message.To.Add(MailboxAddress.Parse(to));
			message.Subject = subject;
			message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

			using var smtp = new SmtpClient();
			smtp.Connect(host, 587, SecureSocketOptions.StartTls);
			smtp.Authenticate(user, password);
			smtp.Send(message);
			smtp.Disconnect(true);
		}
	}
}
