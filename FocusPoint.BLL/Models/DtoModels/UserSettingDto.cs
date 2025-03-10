namespace FocusPoint.BLL.Models.DtoModels;

public class UserSettingDto
{
    public Guid Id { get; set; }

    public int FocusInterval { get; set; } // Size of focus interval
    public int WorkBlocks { get; set; } // Number of focus blocks
    public bool UseInternalTimer { get; set; } // If user wants to use build-in timer
    public Guid UserId { get; set; }
}