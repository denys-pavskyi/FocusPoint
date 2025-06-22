using FocusPoint.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.DAL.Repositories.Interfaces;

public interface IMainNoteRepository
{
    Task<IEnumerable<MainNote>> GetAllAsync();
    Task<MainNote?> GetByIdAsync(Guid id);
    Task AddAsync(MainNote mainNote);
    Task UpdateAsync(MainNote mainNote);
    Task DeleteAsync(Guid id);
    Task<List<MainNote>> GetAllByUserIdOrdered(Guid userId);
    Task UpdateManyAsync(IEnumerable<MainNote> notesToUpdate);

}