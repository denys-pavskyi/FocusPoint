using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.PL.Commands;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FocusPoint.PL.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{

    private readonly IUserService _userService;

    public SettingsViewModel(UserDto currentUser, IUserService userService)
    {
        CurrentUser = currentUser;
        _userService = userService;
        SaveSettingsCommand = new RelayCommand(async () => await SaveSettingsAsync());
    }

    public UserDto CurrentUser { get; set; }

    public ICommand SaveSettingsCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task SaveSettingsAsync()
    {
        try
        {
            await _userService.UpdateSettingsAsync(CurrentUser.UserSetting!);
            //LoadFocusBlocks(CurrentUser.UserSetting!.WorkBlocks, CurrentUser.UserSetting.FocusInterval);
            //UpdateFocusBlocks(_minutesWorked);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving settings: {ex.Message}");
        }
    }


}