namespace backend.Models;

public class EventItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public bool IsPaid { get; set; }
    public int Capacity { get; set; }
}
