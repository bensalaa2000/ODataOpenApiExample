namespace Axess.Shared;

public class ApiResult<T>
{
	public bool Success { get; set; }

	public T Data { get; set; }

	public ApiResult(bool success, T data)
	{
		Data = data;
		Success = success;
	}
}