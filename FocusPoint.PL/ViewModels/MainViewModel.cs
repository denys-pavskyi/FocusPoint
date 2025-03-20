using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace FocusPoint.PL.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    public event PropertyChangedEventHandler? PropertyChanged;
    private UserDto? _currentUser;


    // For time block
    private int _workBlocks = 3;
    private int _focusInterval = 120;
    private ObservableCollection<FocusBlockViewModel> _focusBlocks;


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

    public int WorkBlocks
    {
        get => _workBlocks;
        set
        {
            _workBlocks = value;
            OnPropertyChanged(nameof(WorkBlocks));
            UpdateFocusBlocks();
        }
    }

    public ObservableCollection<FocusBlockViewModel> FocusBlocks
    {
        get => _focusBlocks;
        set
        {
            _focusBlocks = value;
            OnPropertyChanged(nameof(FocusBlocks));
        }
    }

    public MainViewModel(IUserService userService, UserDto user = null)
    {
        CurrentUser = user;
        _userService = userService;
        SaveSettingsCommand = new RelayCommand(async () => await SaveSettingsAsync());
        UpdateFocusBlocks();
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

    private void UpdateFocusBlocks()
    {
        FocusBlocks = new ObservableCollection<FocusBlockViewModel>();

        for (int i = 0; i < WorkBlocks; i++)
        {
            FocusBlocks.Add(new FocusBlockViewModel());
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}