using FocusPoint.BLL.Interfaces;
using FocusPoint.DAL.Repositories;
using FocusPoint.DAL.Repositories.Interfaces;

namespace FocusPoint.BLL.Services;

public class UserSettingsService: IUserSettingsService
{
    private readonly IUserSettingRepository _userSettingRepository;

    public UserSettingsService(IUserSettingRepository userSettingService)
    {
        _userSettingRepository = userSettingService;
    }



}