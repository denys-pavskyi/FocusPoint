
namespace FocusPoint.BLL.Models;

public class SavedNoteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string Emoji { get; set; } = string.Empty;
    public int OrderIndex { get; set; }

    public Guid UserId { get; set; }
}