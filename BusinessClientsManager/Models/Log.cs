namespace BusinessClientsManager.Models;

public class Log
{
    public int Id { get; set; }
    public string EventType { get; set; }
    public string ObjectName { get; set; }
    public string ObjectId { get; set; }
    public DateTime EventDate { get; set; }
    public string? PrevObject { get; set; }
    public string? NextObject { get; set; }
}
