namespace TodoApp.Application.Responses
{
	public class BaseCommandResponse
	{
		public Guid Id { get; set; }
		public bool Success { get; set; } = true;
		public string Message { get; set; } = string.Empty;
		public List<string>? Errors { get; set; }
	}
}
