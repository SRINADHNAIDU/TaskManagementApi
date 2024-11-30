namespace TaskManagementApi.Models;

public class PagedResponse<T>
{
    public IEnumerable<T>? Data { get; set; }
    public int? Count { get; set; }
}
