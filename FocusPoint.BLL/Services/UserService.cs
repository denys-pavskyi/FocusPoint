using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusPoint.BLL.Services;

public class UserService: IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly IUserSettingRepository _userSettingRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userService, IUserSettingRepository userSettingRepository, IMapper mapper)
    {
        _userRepository = userService;
        _userSettingRepository = userSettingRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        return _mapper.Map<UserDto>(user);
    }


    public async Task UpdateSettingsAsync(UserSettingDto userSettingsDto)
    {
        var userSettings = _mapper.Map<UserSetting>(userSettingsDto);
        await _userSettingRepository.UpdateSettingsAsync(userSettings);
    }

    public async Task<UserSettingDto?> GetUserSettingsByUserIdAsync(Guid userId)
    {
        var userSettingsByUserId = await _userSettingRepository.GetUserSettingsByUserIdAsync(userId);

        return _mapper.Map<UserSettingDto>(userSettingsByUserId);
    }

    public async Task AddUserSettingsAsync(UserSettingDto userSettingsDto)
    {
        var userSetting = _mapper.Map<UserSetting>(userSettingsDto);
        await _userSettingRepository.AddUserSettingsAsync(userSetting);
    }


}