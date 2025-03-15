using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;

namespace FocusPoint.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByUsernameAsync(string username);
    Task UpdateSettingsAsync(UserSettingDto userSettingsDto);
    Task<UserSettingDto?> GetUserSettingsByUserIdAsync(Guid userId);
    Task AddUserSettingsAsync(UserSettingDto userSettingsDto);
}