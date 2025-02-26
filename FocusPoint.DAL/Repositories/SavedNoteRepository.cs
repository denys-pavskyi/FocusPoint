using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class SavedNoteRepository: ISavedNoteRepository
{
    private readonly AppDbContext _context;

    public SavedNoteRepository(AppDbContext context)
    {
        _context = context;
    }
}