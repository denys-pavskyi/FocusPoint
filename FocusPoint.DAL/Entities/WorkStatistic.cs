using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusPoint.DAL.Entities;

public class WorkStatistic
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public int TotalWorkMinutes { get; set; }

    [Required]
    public int CompletedTasks { get; set; }
}