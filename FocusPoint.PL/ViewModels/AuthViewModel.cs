using AutoMapper;
using FocusPoint.BLL.Interfaces;
using FocusPoint.PL.Commands;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using FocusPoint.BLL.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FocusPoint.PL.ViewModels;

public class AuthViewModel : INotifyPropertyChanged
{
    private readonly IMapper _mapper;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private readonly IUserService _userService;
    private readonly MainViewModel _mainViewModel;
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
        MainViewModel mainViewModel)
    {
        _userService = userService;
        _mapper = mapper;
        LoginCommand = new AsyncRelayCommand(LoginAsync);
        _mainViewModel = mainViewModel;

        // Remove later
        Username = "test_user1";
        Password = "hashedpassword";
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

            var userModel = _mapper.Map<UserDto>(user);
            
            Application.Current.Dispatcher.Invoke(() =>
            {
                _mainViewModel.CurrentUser = userModel;
                var mainWindow = new MainWindow(_mainViewModel);

                mainWindow.Show();
                Application.Current.MainWindow?.Close();
                Application.Current.MainWindow = mainWindow;
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