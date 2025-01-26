namespace Application.Core.DTOs.PaginationDTOs;

public class PaginationDto<T>
{
    public T? RequestData { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}