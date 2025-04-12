using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;

namespace FocusPoint.BLL.Interfaces;

public interface IWorkSessionService
{
    Task<IEnumerable<WorkSessionDto>> GetAllForDateAsync(DateTime date);
    Task AddAsync(WorkSessionDto workSession);
}