using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FocusPoint.BLL.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.PL.Views;

namespace FocusPoint.PL.ViewModels;

public class TasksViewModel: INotifyPropertyChanged
{
    private readonly ITaskItemService _taskItemService;
    private readonly Guid _currentUserId;
    private readonly bool? _isCompletedFilter;

    public ObservableCollection<TaskItemDto> Tasks { get; set; } = new();

    private TaskItemDto? _selectedTask;
    public TaskItemDto? SelectedTask
    {
        get => _selectedTask;
        set
        {
            _selectedTask = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddTaskCommand => new RelayCommand(AddTask);
    public ICommand EditTaskCommand => new RelayCommand(EditTask, () => SelectedTask != null);
    public ICommand DeleteTaskCommand => new RelayCommand(DeleteTask, () => SelectedTask != null);
    public ICommand RefreshTasksCommand => new RelayCommand(async () => await LoadTasksAsync());
    public ICommand MarkCompletedCommand => new RelayCommand<TaskItemDto>(async (task) =>
    {
        if (task == null) return;

        task.IsCompleted = true;
        await _taskItemService.UpdateAsync(task);
        Tasks.Remove(task);
    });


    public TasksViewModel(ITaskItemService taskItemService, Guid currentUserId, bool? isCompletedFilter = false)
    {
        _taskItemService = taskItemService;
        _currentUserId = currentUserId;
        _isCompletedFilter = isCompletedFilter;

        LoadTasksAsync();
    }

    private async Task LoadTasksAsync()
    {
        var tasks = await _taskItemService.GetAllForUserAsync(_currentUserId, _isCompletedFilter);

        var sortedTasks = tasks
            .OrderByDescending(t => t.Priority) // High -> Mid -> Low
            .ToList();

        Tasks = new ObservableCollection<TaskItemDto>(sortedTasks);
        OnPropertyChanged(nameof(Tasks));
    }

    private void AddTask()
    {
        var newTask = new TaskItemDto
        {
            UserId = _currentUserId
        };
        var viewModel = new TaskDialogViewModel(_taskItemService, newTask);
        var dialog = new TaskDialogView(viewModel);

        viewModel.OnSave += async (sender, task) =>
        {
            Tasks.Add(task);
        };

        dialog.ShowDialog();
    }

    private void EditTask()
    {
        if (SelectedTask == null) return;

        var viewModel = new TaskDialogViewModel(_taskItemService, SelectedTask);
        var dialog = new TaskDialogView(viewModel);

        viewModel.OnSave += async (sender, task) =>
        {
            var index = Tasks.IndexOf(SelectedTask);
            Tasks[index] = task;
        };

        dialog.ShowDialog();
    }

    private async void DeleteTask()
    {
        if (SelectedTask != null)
        {
            var success = await _taskItemService.RemoveByIdAsync(SelectedTask.Id);
            if (success)
            {
                Tasks.Remove(SelectedTask);
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
}