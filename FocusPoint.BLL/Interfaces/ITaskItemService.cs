using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;

namespace FocusPoint.BLL.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemDto>> GetAllForUserAsync(Guid userId, bool? isComplete = null);
    Task AddAsync(TaskItemDto newTask);
    Task UpdateAsync(TaskItemDto updatedTask);
    Task<bool> RemoveByIdAsync(Guid taskId);
}