﻿namespace TodoApp.Application.Exceptions
{
	public class BadRequestException(string message) : ApplicationException(message)
	{
	}
}
