using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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



    // Timer

    private DispatcherTimer? _timer;
    private DateTime _startTime;
    private bool _isTimerRunning;

    public string TimerButtonText => _isTimerRunning ? "Stop" : "Start";
    public Brush TimerButtonColor => _isTimerRunning ? Brushes.Red : Brushes.Green;

    private TimeSpan _elapsedTime;
    public TimeSpan ElapsedTime
    {
        get => _elapsedTime;
        set { _elapsedTime = value; 
            OnPropertyChanged(nameof(ElapsedTime)); }
    }

    public ICommand ToggleTimerCommand { get; }
    public ICommand AddManualTimeCommand { get; }


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

        ToggleTimerCommand = new RelayCommand(ToggleTimer);
        AddManualTimeCommand = new RelayCommand(AddManualTime);

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += TimerTick;

        CurrentUser = user;
        _userService = userService;
        SaveSettingsCommand = new RelayCommand(async () => await SaveSettingsAsync());

        LoadFocusBlocks(5, 90);

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

    private void LoadFocusBlocks(int workBlocks, int focusInterval)
    {
        FocusBlocks = new ObservableCollection<FocusBlockViewModel>();
        for (int i = 1; i <= workBlocks; i++)
        {
            FocusBlocks.Add(new FocusBlockViewModel { BlockNumber = i, FocusInterval = focusInterval });
        }
        OnPropertyChanged(nameof(FocusBlocks));
    }

    private void ToggleTimer()
    {
        if (_isTimerRunning)
        {
            _timer?.Stop();
        }
        else
        {
            _startTime = DateTime.UtcNow;
            _timer?.Start();
        }

        _isTimerRunning = !_isTimerRunning;
        OnPropertyChanged(nameof(TimerButtonText));
        OnPropertyChanged(nameof(TimerButtonColor));
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        ElapsedTime = DateTime.UtcNow - _startTime;
    }

    private void AddManualTime()
    {
        // Відкрити діалог або збільшити ElapsedTime вручну
    }


    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}