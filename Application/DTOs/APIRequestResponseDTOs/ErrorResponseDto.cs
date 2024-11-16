namespace Application.DTOs.APIRequestResponseDTOs;

public class ErrorResponseDto
{
    public required string Title { get; set; }
    public List<(string Type,string Message)>? Errors { get; set; }
}