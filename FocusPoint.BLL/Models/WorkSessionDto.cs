
namespace FocusPoint.BLL.Models;

public class WorkSessionDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int Duration { get; set; }
}