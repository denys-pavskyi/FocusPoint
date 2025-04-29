using FocusPoint.DAL.Entities.Enums;

namespace FocusPoint.BLL.Models.DtoModels;

public class TaskItemDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; } = DateTime.Now;

    public Priority Priority { get; set; } = Priority.Low;

    public Guid UserId { get; set; } = Guid.Empty;
}