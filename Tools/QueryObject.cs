namespace Tools;

public class QueryObject
{
    public string? Search { get; set; } = null;
    public int? PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = 20;
}