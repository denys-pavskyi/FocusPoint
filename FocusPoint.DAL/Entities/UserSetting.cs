using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocusPoint.DAL.Entities;

public class UserSetting
{
    public Guid Id { get; set; }

    public int FocusInterval { get; set; } // Size of focus interval
    public int WorkBlocks { get; set; } // Number of focus blocks
    public bool UseInternalTimer { get; set; } // If user wants to use build-in timer

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

}