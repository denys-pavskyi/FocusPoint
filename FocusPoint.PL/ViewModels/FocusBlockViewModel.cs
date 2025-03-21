using System.ComponentModel;
using System.Windows.Media;

namespace FocusPoint.PL.ViewModels;

public class FocusBlockViewModel : INotifyPropertyChanged
{
    private int _currentMinutes;

    public int CurrentMinutes
    {
        get => _currentMinutes;
        set
        {
            _currentMinutes = value;
            OnPropertyChanged(nameof(CurrentMinutes));
            OnPropertyChanged(nameof(SessionProgressText));
            OnPropertyChanged(nameof(SessionStatusColor));
        }
    }

    public string SessionProgressText => $"{CurrentMinutes}/120 хв";

    public SolidColorBrush SessionStatusColor
    {
        get
        {
            if (CurrentMinutes == 0) return new SolidColorBrush(Colors.Red);
            if (CurrentMinutes < 120) return new SolidColorBrush(Colors.Yellow);
            return new SolidColorBrush(Colors.Green);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}