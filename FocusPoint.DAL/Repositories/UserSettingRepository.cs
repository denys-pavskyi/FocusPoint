using FocusPoint.DAL.Configurations;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.DAL.Repositories;

public class UserSettingRepository: IUserSettingRepository
{
    private readonly AppDbContext _context;

    public UserSettingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task UpdateSettingsAsync(UserSetting userSettings)
    {
        _context.UserSettings.Update(userSettings);
        await _context.SaveChangesAsync();
    }

    public async Task<UserSetting?> GetUserSettingsByUserIdAsync(Guid userId)
    {
        var userSettingsByUserId = await _context.UserSettings.FirstOrDefaultAsync(us => us.UserId.Equals(userId));

        return userSettingsByUserId;
    }

    public async Task AddUserSettingsAsync(UserSetting userSettings)
    {
        await _context.UserSettings.AddAsync(userSettings);

        await _context.SaveChangesAsync();
    }
}