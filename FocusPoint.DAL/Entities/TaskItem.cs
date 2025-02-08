using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FocusPoint.DAL.Entities.Enums;

namespace FocusPoint.DAL.Entities;

public class TaskItem
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }

    public Priority Priority { get; set; } = Priority.Low;

    [Required]
    public required Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; } = null!;
}