using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FocusPoint.BLL.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FocusPoint.BLL.Models.DtoModels;

namespace FocusPoint.PL.ViewModels;

public class TasksViewModel: INotifyPropertyChanged
{
    private readonly ITaskItemService _taskItemService;
    private readonly Guid _currentUserId;

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

    public TasksViewModel(ITaskItemService taskItemService, Guid currentUserId)
    {
        _taskItemService = taskItemService;
        _currentUserId = currentUserId;

        LoadTasks();
    }

    private void LoadTasks()
    {
        var tasks = _taskItemService.GetAllForUserAsync(_currentUserId);

        var sortedTasks = tasks
            .OrderByDescending(t => t.Priority) // High -> Mid -> Low
            .ToList();

        Tasks = new ObservableCollection<TaskItemDto>(sortedTasks);
        OnPropertyChanged(nameof(Tasks));
    }

    private void AddTask()
    {
        // TODO: Open window to add task
    }

    private void EditTask()
    {
        // TODO: Open window to edit task
    }

    private async void DeleteTask()
    {
        if (SelectedTask != null)
        {
            //await _taskService.DeleteAsync(SelectedTask.Id);
            Tasks.Remove(SelectedTask);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
}