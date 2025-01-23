namespace Application.Core.DTOs.APIRequestResponseDTOs;

public class ErrorResponseDto
{
    public required string Title { get; set; }
    public List<ErrorDescriptionDto>? ErrorDetails { get; set; }
}