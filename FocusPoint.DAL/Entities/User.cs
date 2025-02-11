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

    [Required] public TimeSpan EndOfDayTime { get; set; } = new(0, 0, 0);

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }


    //Relationship with TaskItem many(TaskItem) - to - one(User)
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();


    //Relationship with MainNote many(MainNote) - to - one(User)
    public ICollection<MainNote> MainNotes { get; set; } = new List<MainNote>();


    //Relationship with WorkSession many(WorkSession) - to - one(User)
    public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();

    //Relationship with WorkStatistic many(WorkStatistic) - to - one(User)
    public ICollection<WorkStatistic> WorkStatistics { get; set; } = new List<WorkStatistic>();


    //Relationship with SavedNote many(SavedNote) - to - one(User)
    public ICollection<SavedNote> SavedNotes { get; set; } = new List<SavedNote>();
}