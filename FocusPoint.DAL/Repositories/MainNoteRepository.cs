using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.DAL.Repositories;

public class MainNoteRepository : IMainNoteRepository
{
    private readonly AppDbContext _context;

    public MainNoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MainNote>> GetAllAsync()
    {
        return await _context.MainNotes.ToListAsync();
    }

    public async Task<MainNote?> GetByIdAsync(Guid id)
    {
        return await _context.MainNotes.FindAsync(id);
    }

    public async Task AddAsync(MainNote mainNote)
    {
        await _context.MainNotes.AddAsync(mainNote);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MainNote mainNote)
    {
        _context.MainNotes.Update(mainNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MainNote mainNote)
    {
        _context.MainNotes.Remove(mainNote);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MainNote>> GetAllByUserIdOrdered(Guid userId)
    {
        var notes = await _context.MainNotes
            .Where(mn => mn.UserId.Equals(userId))
            .OrderBy(mn => mn.OrderIndex)
            .AsNoTracking()
            .ToListAsync();

        return notes;
    }

    public async Task UpdateManyAsync(IEnumerable<MainNote> notesToUpdate)
    {
        foreach (var mainNote in notesToUpdate)
        {
            var trackedEntity = _context.MainNotes.Local.FirstOrDefault(x => x.Id == mainNote.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }
        }

        _context.MainNotes.UpdateRange(notesToUpdate);
        await _context.SaveChangesAsync();
    }

}