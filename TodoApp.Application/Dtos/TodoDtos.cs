namespace TodoApp.Application.Dtos;

public record TodoRequestDto(string Description);
public record TodoDetailDto(Guid Id, string Description, bool IsComplete);

public record TodoDetailWithUserDto(Guid Id, string Description, bool IsComplete, string UserName, string UserId);
