﻿using Serilog;

namespace TodoApp.Infrastructure.Logger
{
	public static class LogException 
	{
		public static void LogExceptions(Exception ex)
		{
			LogToFile(ex.Message);
			LogToConsole(ex.Message);
			LogToDebug(ex.Message);
		}

		private static void LogToDebug(string message) => Log.Debug(message);

		private static void LogToConsole(string message) => Log.Warning(message);

		private static void LogToFile(string message) => Log.Error(message);
	}
}
