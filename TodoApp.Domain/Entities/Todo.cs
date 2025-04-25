namespace TodoApp.Domain.Entities;

public class Todo
{
	public Guid Id { get; set; }
	public string Description { get; set; } = default!;
	public bool IsComplete { get; set; } = false;
	public virtual string UserId { get; set; } = default!;
	public virtual ApplicationUser User { get; set; } = default!;

}
