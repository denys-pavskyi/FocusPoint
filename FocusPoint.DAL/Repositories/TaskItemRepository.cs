using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.DAL.Repositories;

public class TaskItemRepository: ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<TaskItem>> GetAllForUserAsync(Guid userId, bool? isComplete = null)
    {
        IQueryable<TaskItem> tasks;

        if (isComplete.HasValue)
        {
            tasks = _context.TaskItems.Where(ti => ti.UserId.Equals(userId) && ti.IsCompleted == isComplete);

        }
        else
        {
            tasks = _context.TaskItems.Where(ti => ti.UserId.Equals(userId));
        }

        return await tasks.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(TaskItem newTask)
    {
        newTask.DueDate = newTask.DueDate.ToUniversalTime();
        newTask.CreatedAt = newTask.CreatedAt.ToUniversalTime();

        await _context.TaskItems.AddAsync(newTask);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(TaskItem updatedTask)
    {
        var task = await _context.TaskItems.FindAsync(updatedTask.Id);

        if (task == null)
        {
            throw new InvalidOperationException("Task not found");
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;
        task.DueDate = updatedTask.DueDate.ToUniversalTime();
        task.Priority = updatedTask.Priority;

        _context.TaskItems.Update(task);

        await _context.SaveChangesAsync();
    }


    public async Task<TaskItem?> GetByIdAsync(Guid taskId)
    {
        var task = await _context.TaskItems.FirstOrDefaultAsync(ti => ti.Id.Equals(taskId));
        return task;
    }

    public async Task RemoveAsync(TaskItem task)
    {
        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
    }

}