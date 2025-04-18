using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FocusPoint.DAL.Repositories;

public class WorkSessionRepository: IWorkSessionRepository
{
    private readonly AppDbContext _context;

    public WorkSessionRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<WorkSession>> GetAllForDateAsync(DateTime date)
    {

        var startOfDay = date.Date.ToUniversalTime();
        var endOfDay = startOfDay.AddDays(1).ToUniversalTime();

        var workSessions = await _context.WorkSessions
            .Where(ws =>
                ws.StartTime < endOfDay &&
                (ws.EndTime == null || ws.EndTime > startOfDay)
            )
            .ToListAsync();

        return workSessions;


    }

    public async Task AddAsync(WorkSession workSession)
    {
        await _context.WorkSessions.AddAsync(workSession);
        await _context.SaveChangesAsync();
    }


}