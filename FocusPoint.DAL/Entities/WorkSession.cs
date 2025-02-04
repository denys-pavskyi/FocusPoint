using System.ComponentModel.DataAnnotations;

namespace FocusPoint.DAL.Entities;

public class WorkSession
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public User? User { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [Required]
    public TimeSpan Duration { get; set; }
}