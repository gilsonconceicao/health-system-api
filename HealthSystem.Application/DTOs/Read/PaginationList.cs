namespace HealthSystem.Application.DTOs.Read;
#nullable disable
public class PaginationList<T>
{
    public T Data {get; set;}
    public int TotalItems {get; set;}
}
