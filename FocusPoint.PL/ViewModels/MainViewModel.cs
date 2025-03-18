using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FocusPoint.PL.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    public event PropertyChangedEventHandler? PropertyChanged;
    private UserDto? _currentUser;
    public ICommand SaveSettingsCommand { get; }

    public UserDto CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    public MainViewModel(IUserService userService, UserDto user = null)
    {
        CurrentUser = user;
        _userService = userService;
        SaveSettingsCommand = new RelayCommand(async () => await SaveSettingsAsync());
    }


    private async Task SaveSettingsAsync()
    {
        try
        {
            await _userService.UpdateSettingsAsync(CurrentUser.UserSetting);
            // TODO Logic for interface update
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving settings: {ex.Message}");
        }
    }



    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}