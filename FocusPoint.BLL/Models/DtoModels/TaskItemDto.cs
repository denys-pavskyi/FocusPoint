using FocusPoint.DAL.Entities.Enums;

namespace FocusPoint.BLL.Models.DtoModels;

public class TaskItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }

    public Priority Priority { get; set; } = Priority.Low;

    public required Guid UserId { get; set; }
}