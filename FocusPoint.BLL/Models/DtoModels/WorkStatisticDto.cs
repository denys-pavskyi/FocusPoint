namespace FocusPoint.BLL.Models.DtoModels;

public class WorkStatisticDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }

    public int TotalWorkMinutes { get; set; }
    public int CompletedTasks { get; set; }
}