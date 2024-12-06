namespace Application.DTOs.APIRequestResponseDTOs;

public class ApiResponseDto<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = "Something went wrong";
    public bool Success { get; set; } = false;
    public bool ShowMessage { get; set; } = true;
    public Dictionary<string, string>? Values { get; set; }
    public ErrorResponseDto? Error { get; set; }
}