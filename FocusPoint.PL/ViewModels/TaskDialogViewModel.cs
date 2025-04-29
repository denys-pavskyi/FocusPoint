using FocusPoint.BLL.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusPoint.BLL.Interfaces;
using System.Windows.Input;
using FocusPoint.PL.Commands;

namespace FocusPoint.PL.ViewModels
{
    public class TaskDialogViewModel : INotifyPropertyChanged
    {
        private readonly ITaskItemService _taskItemService;
        private TaskItemDto _task;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<TaskItemDto>? OnSave;
        public ICommand SaveCommand { get; }

        public TaskItemDto Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged(nameof(Task));
            }
        }

        public string ButtonText => Task.Id == Guid.Empty ? "Add" : "Save";

        public TaskDialogViewModel(ITaskItemService taskItemService, TaskItemDto task = null!)
        {
            _taskItemService = taskItemService;
            Task = task ?? new TaskItemDto();
            SaveCommand = new RelayCommand(SaveTask);
        }

        public async void SaveTask()
        {
            if (Task.Id == Guid.Empty)
            {
                await _taskItemService.AddAsync(Task);
            }
            else
            {
                await _taskItemService.UpdateAsync(Task);
            }

            OnSave?.Invoke(this, Task);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
