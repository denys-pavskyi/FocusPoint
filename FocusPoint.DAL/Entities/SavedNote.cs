using System.ComponentModel.DataAnnotations;

namespace FocusPoint.DAL.Entities;

public class SavedNote
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(600)]
    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public Guid UserId { get; set; }

    public User? User { get; set; }
}