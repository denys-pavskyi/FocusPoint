using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.PL.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.PL.Views;
using FocusPoint.DAL.Entities;

namespace FocusPoint.PL.ViewModels;

public class AuthViewModel : INotifyPropertyChanged
{
    private readonly IMapper _mapper;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private readonly IUserService _userService;
    private readonly MainViewModel _mainViewModel;
    private readonly MainView _mainView;
    public event EventHandler? OnRequestClose;

    public ICommand LoginCommand { get; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(nameof(Username)); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }

    public AuthViewModel(IUserService userService, IMapper mapper, 
        MainViewModel mainViewModel, MainView mainView)
    {
        _userService = userService;
        _mapper = mapper;
        LoginCommand = new AsyncRelayCommand(LoginAsync);
        _mainViewModel = mainViewModel;
        _mainView = mainView;

        // Remove later
        Username = "test_user1";
        Password = "hashedpassword";

        LoginAsync();
    }

    private async Task LoginAsync()
    {
        try
        {
            var user = await _userService.GetUserByUsernameAsync(Username);
            if (user == null || !VerifyPassword(Password, user.PasswordHash))
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user?.UserSetting == null)
            {
                var newUserSetting = new UserSettingDto
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    FocusInterval = 25,
                    WorkBlocks = 4,
                    UseInternalTimer = false
                };



                await _userService.AddUserSettingsAsync(newUserSetting);


                user.UserSetting = newUserSetting;
                user.UserSettingId = newUserSetting.Id;
            }



            var userModel = _mapper.Map<UserDto>(user);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                _mainViewModel.CurrentUser = userModel;
                _mainView.DataContext = _mainViewModel;

                OnRequestClose?.Invoke(this, EventArgs.Empty);

                Application.Current.MainWindow = _mainView;
                _mainView.Show();

            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Login error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        // TODO: BCrypt as a password hashing tool
        return password == storedHash;
    }

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


}