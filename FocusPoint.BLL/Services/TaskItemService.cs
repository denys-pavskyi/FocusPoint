using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
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

    public IEnumerable<TaskItemDto> GetAllForUserAsync(Guid userId)
    {
        var tasks = _taskItemRepository.GetAllForUserAsync(userId);

        return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
    }


}