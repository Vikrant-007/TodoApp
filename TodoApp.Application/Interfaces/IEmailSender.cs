using TodoApp.Application.Models;

namespace TodoApp.Application.Interfaces
{
	public interface IEmailSender
	{
		Task<bool> SendEmail(Email email);
	}
}
