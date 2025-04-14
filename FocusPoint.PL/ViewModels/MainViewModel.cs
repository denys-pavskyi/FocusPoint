using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using FocusPoint.PL.Views;
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
    private readonly IWorkSessionService _workSessionService;
    public event PropertyChangedEventHandler? PropertyChanged;
    private UserDto? _currentUser;


    // For time block
    private ObservableCollection<FocusBlockViewModel> _focusBlocks;
    public ObservableCollection<FocusBlockViewModel> FocusBlocks
    {
        get => _focusBlocks;
        set
        {
            _focusBlocks = value;
            OnPropertyChanged(nameof(FocusBlocks));
        }
    }

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

    public ICommand ToggleTimerCommand => new RelayCommand(ToggleTimer);
    public ICommand AddManualTimeCommand => new RelayCommand(OpenAddManualTimeDialog);


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

    public MainViewModel(IUserService userService, IWorkSessionService workSession, UserDto user = null)
    {

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += TimerTick;

        CurrentUser = user;
        _userService = userService;
        _workSessionService = workSession;
        SaveSettingsCommand = new RelayCommand(async () => await SaveSettingsAsync());

        LoadFocusBlocks(5, 90);

    }


    private async Task SaveSettingsAsync()
    {
        try
        {
            await _userService.UpdateSettingsAsync(CurrentUser.UserSetting!);
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
            FocusBlocks.Add(new FocusBlockViewModel
            {
                BlockNumber = i,
                FocusInterval = focusInterval
            });
        }

        OnPropertyChanged(nameof(FocusBlocks));
    }

    public void UpdateBlockProgress(int blockNumber, int minutesWorked)
    {
        var block = FocusBlocks.FirstOrDefault(b => b.BlockNumber == blockNumber);
        if (block != null)
        {
            block.UpdateMinutesWorked(minutesWorked);
        }
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



    private void OpenAddManualTimeDialog()
    {
        if (_currentUser == null) return;

        var window = new AddWorkSessionView(_currentUser.Id, async session =>
        {
            await _workSessionService.AddAsync(session);
            // Оновити список сесій, якщо потрібно
        });

        window.ShowDialog();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}