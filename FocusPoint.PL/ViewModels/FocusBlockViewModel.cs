using System.ComponentModel;
using System.Windows.Media;

namespace FocusPoint.PL.ViewModels;

public class FocusBlockViewModel : INotifyPropertyChanged
{
    public int BlockNumber { get; set; }
    public int MinutesWorked { get; set; }
    public int FocusInterval { get; set; }

    public SolidColorBrush BlockColor => GetBlockColor();
    public string ProgressText => GetProgressText();

    private SolidColorBrush GetBlockColor()
    {
        if (MinutesWorked >= FocusInterval) return Brushes.Green;   // Finished
        if (MinutesWorked > 0) return Brushes.Orange;              // Started
        return Brushes.Red;                                        // Not started yet
    }

    private string GetProgressText()
    {
        if (FocusInterval == 0) return "0%";
        int progressPercentage = (int)((float)MinutesWorked / FocusInterval * 100);
        return $"{progressPercentage}%";
    }

    public void UpdateMinutesWorked(int minutes)
    {
        MinutesWorked = minutes;
        OnPropertyChanged(nameof(MinutesWorked));
        OnPropertyChanged(nameof(BlockColor));
        OnPropertyChanged(nameof(ProgressText));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}