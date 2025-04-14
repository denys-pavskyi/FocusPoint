using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.PL.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FocusPoint.PL.ViewModels
{
    public class AddWorkSessionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DateTime _startDate = DateTime.Now.Date;
        private string _startTimeString = DateTime.Now.ToString("HH:mm");
        private int _duration;

        private readonly Guid _userId;
        private readonly Action<WorkSessionDto> _onConfirm;
        private readonly Action _closeWindow;
        public ICommand ConfirmCommand { get; }

        public AddWorkSessionViewModel(Guid userId, Action<WorkSessionDto> onConfirm, Action closeWindow)
        {
            _userId = userId;
            _onConfirm = onConfirm;
            _closeWindow = closeWindow;
            ConfirmCommand = new RelayCommand(Confirm);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        public string StartTimeString
        {
            get => _startTimeString;
            set { _startTimeString = value; OnPropertyChanged(nameof(StartTimeString)); }
        }

        public int Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        private void Confirm()
        {
            if (!TimeSpan.TryParse(StartTimeString, out var time))
            {
                MessageBox.Show("Wrong time format");
                return;
            }

            var startTime = StartDate.Date + time;
            var session = new WorkSessionDto
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                StartTime = startTime.ToUniversalTime(),
                Duration = Duration,
                EndTime = startTime.AddMinutes(Duration).ToUniversalTime()
            };

            _onConfirm(session);
            _closeWindow();
        }


        private void OnPropertyChanged(string name)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
}
