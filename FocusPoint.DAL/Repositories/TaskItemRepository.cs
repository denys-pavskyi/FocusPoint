using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.DAL.Repositories;

public class TaskItemRepository: ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }


    public IEnumerable<TaskItem> GetAllForUserAsync(Guid userId)
    {
        var tasks = _context.TaskItems.Where(ti => ti.UserId.Equals(userId));

        return tasks;
    }

}