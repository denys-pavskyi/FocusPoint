using FocusPoint.BLL.Models.DtoModels;
using System.ComponentModel;

namespace FocusPoint.PL.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private UserDto? _currentUser;

    public UserDto? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
        }
    }

    private UserSettingDto? _userSettings;
    public UserSettingDto? UserSettings
    {
        get => _userSettings;
        set
        {
            _userSettings = value;
            OnPropertyChanged(nameof(UserSettings));
        }
    }


    public MainViewModel(UserDto? user = null)
    {
        CurrentUser = user;
    }


    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}