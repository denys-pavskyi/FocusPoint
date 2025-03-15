using FocusPoint.DAL.Entities;

namespace FocusPoint.DAL.Repositories.Interfaces;

public interface IUserSettingRepository
{
    Task<UserSetting?> GetUserSettingsByUserIdAsync(Guid userId);
    Task AddUserSettingsAsync(UserSetting userSettings);
    Task UpdateSettingsAsync(UserSetting userSettings);
}