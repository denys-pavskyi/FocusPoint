using FocusPoint.DAL.Entities;

namespace FocusPoint.DAL.Repositories.Interfaces;

public interface IWorkSessionRepository
{
    Task<IEnumerable<WorkSession>> GetAllForDateAsync(DateTime date);
    Task AddAsync(WorkSession workSession);
}