using FocusPoint.DAL.Entities;

namespace FocusPoint.DAL.Repositories.Interfaces;

public interface ITaskItemRepository
{
    IEnumerable<TaskItem> GetAllForUserAsync(Guid userId);
}