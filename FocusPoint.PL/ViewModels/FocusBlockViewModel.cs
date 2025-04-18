using System.ComponentModel;
using System.Windows.Media;

namespace FocusPoint.PL.ViewModels;

public class FocusBlockViewModel : INotifyPropertyChanged
{
    public int BlockNumber { get; set; }
    private int _minutesWorked;
    public int MinutesWorked
    {
        get => _minutesWorked;
        set
        {
            if (_minutesWorked != value)
            {
                _minutesWorked = value;
                OnPropertyChanged(nameof(MinutesWorked));
                OnPropertyChanged(nameof(ProgressPercentage));
                OnPropertyChanged(nameof(BlockColor));
            }
        }
    }
    public int FocusInterval { get; set; }
    public double ProgressPercentage => (FocusInterval > 0) ? (double)MinutesWorked / FocusInterval * 100 : 0;


    public SolidColorBrush BlockColor => GetBlockColor();

    private SolidColorBrush GetBlockColor()
    {
        if (MinutesWorked >= FocusInterval) return Brushes.Green;   // Finished
        if (MinutesWorked > 0) return Brushes.Orange;              // Started
        return Brushes.Red;                                        // Not started yet
    }

    public void UpdateMinutesWorked(int minutes)
    {
        MinutesWorked = minutes;
        OnPropertyChanged(nameof(MinutesWorked));
        OnPropertyChanged(nameof(BlockColor));
        OnPropertyChanged(nameof(ProgressPercentage));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}