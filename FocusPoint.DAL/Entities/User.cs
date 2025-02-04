using System.ComponentModel.DataAnnotations;

namespace FocusPoint.DAL.Entities;

public class User
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required]
    public TimeSpan EndOfDayTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }


    //Relationship with TaskItem many(TaskItem) - to - one(User)
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}