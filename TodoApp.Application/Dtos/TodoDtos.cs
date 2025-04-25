namespace TodoApp.Application.Dtos;

public record TodoDto(string Description);
public record TodoDetailDto(Guid Id, string Description, bool IsComplete);
