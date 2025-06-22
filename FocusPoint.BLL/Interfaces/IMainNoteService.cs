using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;

namespace FocusPoint.BLL.Interfaces;

public interface IMainNoteService
{
    Task<MainNoteDto?> GetByIdAsync(Guid mainNodeId);
    Task AddAsync(MainNoteDto mainNoteDto);
    Task UpdateAsync(MainNoteDto mainNoteDto);
    Task<bool> DeleteAsync(Guid mainNodeId);
    Task<List<MainNoteDto>> GetAllByUserIdOrdered(Guid userId);
    Task UpdateManyAsync(IEnumerable<MainNoteDto> notesToUpdateDtos);
    Task ReorderNotesAsync(List<MainNote> notes);
}