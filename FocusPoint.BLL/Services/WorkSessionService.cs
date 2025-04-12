using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.BLL.Services;

public class WorkSessionService : IWorkSessionService
{

    private readonly IWorkSessionRepository _workSessionRepository;
    private readonly IMapper _mapper;

    public WorkSessionService(IWorkSessionRepository workSessionRepository, IMapper mapper)
    {
        _workSessionRepository = workSessionRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(WorkSessionDto workSession)
    {
        var ws = _mapper.Map<WorkSession>(workSession);
        await _workSessionRepository.AddAsync(ws);
    }

    public async Task<IEnumerable<WorkSessionDto>> GetAllForDateAsync(DateTime date)
    {
        var workSessions = await _workSessionRepository.GetAllForDateAsync(date);

        return _mapper.Map<IEnumerable<WorkSessionDto>>(workSessions);
    }
}