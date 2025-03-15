using FocusPoint.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusPoint.BLL.Models.DtoModels;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Email { get; set; }
    public TimeSpan EndOfDayTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public Guid? UserSettingId { get; set; }

    [ForeignKey(nameof(UserSettingId))]
    public UserSettingDto? UserSetting { get; set; }

    public List<Guid> TaskIds { get; set; } = new();
    public List<Guid> MainNoteIds { get; set; } = new();
    public List<Guid> WorkSessionIds { get; set; } = new();
    public List<Guid> WorkStatisticIds { get; set; } = new();
    public List<Guid> SavedNoteIds { get; set; } = new();
}