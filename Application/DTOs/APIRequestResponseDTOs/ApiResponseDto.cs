namespace Application.DTOs.APIRequestResponseDTOs;

public class ApiResponseDto<T>
{
    public T? Data { get; set; }
    public required string Message { get; set; }
    public bool Success { get; set; } = false;
    public bool ShowMessage { get; set; } = false;
    public Dictionary<string, string>? Values { get; set; }
    public ErrorResponseDto? Error { get; set; }
}