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
    int _minutesWorked;


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

    private DispatcherTimer _timer;
    private bool _isTimerRunning;
    private DateTime _startTime;

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


    private SettingsViewModel _settingsViewModel;
    public SettingsViewModel SettingsViewModel
    {
        get => _settingsViewModel;
        set
        {
            _settingsViewModel = value;
            OnPropertyChanged(nameof(SettingsViewModel));
        }
    }

    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set
        {
            if (_selectedTabIndex != value)
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
                OnTabChanged();
            }
        }
    }

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

    }

    public async Task InitializeAsync()
    {
        if (_currentUser.UserSetting != null)
        {
            LoadFocusBlocks(_currentUser.UserSetting.WorkBlocks, _currentUser.UserSetting.FocusInterval);

            _minutesWorked = await CalculateWorkedMinutesForToday();
            UpdateFocusBlocks(_minutesWorked);

            SettingsViewModel = new SettingsViewModel(CurrentUser, _userService);

        }
    }

    public void RefreshFocusBlocks()
    {
        if (CurrentUser?.UserSetting is not null)
        {
            LoadFocusBlocks(CurrentUser.UserSetting.WorkBlocks, CurrentUser.UserSetting.FocusInterval);
            UpdateFocusBlocks(_minutesWorked);
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

    private async void ToggleTimer()
    {
        if (_isTimerRunning)
        {
            _timer?.Stop();
            _isTimerRunning = false;

            var elapsedMinutes = (int)_elapsedTime.TotalMinutes;
            if (elapsedMinutes > 0 && _currentUser != null)
            {
                var session = new WorkSessionDto
                {
                    Id = Guid.NewGuid(),
                    UserId = _currentUser.Id,
                    StartTime = _startTime,
                    EndTime = _startTime.AddMinutes(elapsedMinutes),
                    Duration = elapsedMinutes
                };

                await _workSessionService.AddAsync(session);
                _minutesWorked += session.Duration;
                UpdateFocusBlocks(_minutesWorked);
            }


        }
        else
        {
            _startTime = DateTime.UtcNow;
            _timer?.Start();
            _isTimerRunning = true;
        }

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

            _minutesWorked = await CalculateWorkedMinutesForToday();

            UpdateFocusBlocks(_minutesWorked);

        });

        window.ShowDialog();
    }


    private void UpdateFocusBlocks(int minutesWorked)
    {
        if (FocusBlocks == null || FocusBlocks.Count == 0)
            return;

        foreach (var block in FocusBlocks)
        {
            if (minutesWorked <= 0)
            {
                block.MinutesWorked = 0;
            }
            else if (minutesWorked >= block.FocusInterval)
            {
                block.MinutesWorked = block.FocusInterval;
                minutesWorked -= block.FocusInterval;
            }
            else
            {
                block.MinutesWorked = minutesWorked;
                minutesWorked = 0;
            }
        }
    }


    private async Task<int> CalculateWorkedMinutesForToday()
    {
        var sessions = await _workSessionService.GetAllForDateAsync(DateTime.Today);

        if (_currentUser == null || _currentUser.EndOfDayTime == null)
            throw new InvalidOperationException("User or EndOfDayTime is not set.");

        var endOfDay = _currentUser.EndOfDayTime;
        var today = DateTime.Today.ToUniversalTime();


        var dayStart = today.Add(endOfDay);
        if (DateTime.Now.TimeOfDay < endOfDay)
            dayStart = dayStart.AddDays(-1);

        var dayEnd = dayStart.AddDays(1);

        int totalMinutes = 0;

        foreach (var session in sessions)
        {
            var sessionStart = session.StartTime;
            var sessionEnd = session.EndTime ?? DateTime.Now;

            var overlapStart = sessionStart > dayStart ? sessionStart : dayStart;
            var overlapEnd = sessionEnd < dayEnd ? sessionEnd : dayEnd;

            if (overlapEnd > overlapStart)
            {
                var minutes = (int)(overlapEnd - overlapStart).TotalMinutes;
                totalMinutes += minutes;
            }
        }

        return totalMinutes;
    }


    private void OnTabChanged()
    {
        if (SelectedTabIndex == 0)
        {
            RefreshFocusBlocks();
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}