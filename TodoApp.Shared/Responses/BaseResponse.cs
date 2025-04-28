namespace TodoApp.Shared.Responses
{
	public class BaseResponse<T>
	{
		public bool Status { get; set; } = true;
		public string Error { get; set; } = string.Empty;
		public int StatusCode { get; set; }
		public T Data { get; set; } = default!;

		public BaseResponse(bool status, T? data, int statusCode)
		{
			Status = status;
			Data = data!;
			StatusCode = statusCode;
		}
		public BaseResponse(bool status, string error, int statusCode)
		{
			Status = status;
			Error = error;
			StatusCode = statusCode;
		}	

		public static BaseResponse<T> Success(T data) => new(true, data, 200);
		public static BaseResponse<T> Failure(string error) => new(true, error, 204);
	}
}
