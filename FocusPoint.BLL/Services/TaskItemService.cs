using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.BLL.Services;

public class TaskItemService: ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public TaskItemService(ITaskItemRepository taskItemRepository, IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskItemDto>> GetAllForUserAsync(Guid userId, bool? isComplete = null)
    {
        var tasks = await _taskItemRepository.GetAllForUserAsync(userId, isComplete);

        return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
    }

    public async Task AddAsync(TaskItemDto newTask)
    {
        var newTaskMapped = _mapper.Map<TaskItem>(newTask);

        await _taskItemRepository.AddAsync(newTaskMapped);
    }

    public async Task UpdateAsync(TaskItemDto updatedTask)
    {
        var updatedTaskMapped = _mapper.Map<TaskItem>(updatedTask);

        await _taskItemRepository.UpdateAsync(updatedTaskMapped);
    }

    public async Task<bool> RemoveByIdAsync(Guid taskId)
    {
        var task = await _taskItemRepository.GetByIdAsync(taskId);

        if (task is null)
        {
            return false;
        }

        await _taskItemRepository.RemoveAsync(task);
        return true;
    }
}