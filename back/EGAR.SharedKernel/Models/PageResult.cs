namespace EGAR.SharedKernel.Models;

public class PageResult<T>
{
    public IReadOnlyCollection<T> Items { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;

    public PageResult(IReadOnlyCollection<T> items, int total, int page, int pageSize)
    {
        Items = items;
        Total = total;
        Page = page;
        PageSize = pageSize;
    }

    public PageResult()
    {
        Items = new List<T>();
    }
}
