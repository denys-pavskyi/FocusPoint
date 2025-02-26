using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class TaskItemRepository: ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }
}