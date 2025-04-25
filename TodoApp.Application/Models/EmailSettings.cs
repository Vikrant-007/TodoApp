namespace TodoApp.Application.Models
{
	public class EmailSettings
	{
		public string Client { get; set; } = default!;
		public int Port { get; set; }
		public string ApiKey { get; set; } = default!;
		public string FromAddress { get; set; } = default!;
		public string FromName { get; set; } = default!;
	}
}
