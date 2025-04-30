using FocusPoint.DAL.Entities.Enums;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FocusPoint.PL.Other;

public class PriorityToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Priority priority)
        {
            return priority switch
            {
                Priority.High => Brushes.Red,
                Priority.Medium => Brushes.Orange,
                Priority.Low => Brushes.Green,
                _ => Brushes.Gray
            };
        }

        return Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}