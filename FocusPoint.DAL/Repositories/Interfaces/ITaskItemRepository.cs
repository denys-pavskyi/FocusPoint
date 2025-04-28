using FocusPoint.DAL.Entities;

namespace FocusPoint.DAL.Repositories.Interfaces;

public interface ITaskItemRepository
{
    IEnumerable<TaskItem> GetAllForUserAsync(Guid userId);
    Task AddAsync(TaskItem newTask);
    Task UpdateAsync(TaskItem updatedTask);
    Task<TaskItem?> GetByIdAsync(Guid taskId);
    Task RemoveAsync(TaskItem task);
}