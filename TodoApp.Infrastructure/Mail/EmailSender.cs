using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Models;

namespace TodoApp.Infrastructure.Mail
{
	public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
	{
		private readonly EmailSettings _emailSettings = emailSettings.Value;

		public async Task<bool> SendEmail(Email email)
		{
			using SmtpClient client = new(_emailSettings.Client);
			client.Port = _emailSettings.Port;
			client.Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.ApiKey);
			client.EnableSsl = true;

			MailMessage mailMessage = new()
			{
				From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName),
				Subject = email.Subject,
				Body = email.Body,
				IsBodyHtml = true,
			};

			mailMessage.To.Add(email.To);

			try
			{
				await client.SendMailAsync(mailMessage);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
