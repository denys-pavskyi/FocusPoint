using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class WorkStatisticRepository: IWorkStatisticRepository
{
    private readonly AppDbContext _context;

    public WorkStatisticRepository(AppDbContext context)
    {
        _context = context;
    }
}