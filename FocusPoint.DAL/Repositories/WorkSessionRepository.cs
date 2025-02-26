using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class WorkSessionRepository: IWorkSessionRepository
{
    private readonly AppDbContext _context;

    public WorkSessionRepository(AppDbContext context)
    {
        _context = context;
    }
}