using FocusPoint.BLL.Interfaces;
using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.DAL.Entities;
using FocusPoint.PL.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FocusPoint.PL.ViewModels;

public class MainNotesViewModel : INotifyPropertyChanged
{
    private readonly IMainNoteService _mainNoteService;
    private readonly Guid _currentUserId;

    public ObservableCollection<MainNoteDto> Notes { get; set; } = new();

    private MainNoteDto? _selectedNote;
    public MainNoteDto? SelectedNote
    {
        get => _selectedNote;
        set
        {
            if (_selectedNote != value)
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand LoadNotesCommand { get; }
    public ICommand AddNoteCommand { get; }
    public ICommand DeleteNoteCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand MoveLeftCommand { get; }
    public ICommand MoveRightCommand { get; }


    public MainNotesViewModel(IMainNoteService mainNoteService, Guid currentUserId)
    {
        _mainNoteService = mainNoteService;
        _currentUserId = currentUserId;

        LoadNotesCommand = new RelayCommand(async () => await LoadNotesAsync());
        AddNoteCommand = new RelayCommand(async () => await AddNoteAsync());
        DeleteNoteCommand = new RelayCommand(async () => await DeleteSelectedNoteAsync(), () => SelectedNote != null);
        SaveCommand = new RelayCommand(async () => await SaveNotesAsync());
        MoveLeftCommand = new RelayCommand(() => MoveNote(-1), () => CanMoveNote(-1));
        MoveRightCommand = new RelayCommand(() => MoveNote(1), () => CanMoveNote(1));

        _ = LoadNotesAsync();
    }


    private async Task LoadNotesAsync()
    {
        var notes = await _mainNoteService.GetAllByUserIdOrdered(_currentUserId);
        

        foreach (var mainNoteDto in notes)
        {
            mainNoteDto.AcceptChanges();
        }

        Notes = new ObservableCollection<MainNoteDto>(notes);

        SelectedNote = Notes.FirstOrDefault();
        OnPropertyChanged(nameof(Notes));
    }


    private async Task AddNoteAsync()
    {
        var newNote = new MainNoteDto
        {
            Id = Guid.NewGuid(),
            Title = "New Note",
            Content = string.Empty,
            OrderIndex = Notes.Count,
            UserId = _currentUserId
        };

        await _mainNoteService.AddAsync(newNote);
        Notes.Add(newNote);
        SelectedNote = newNote;
    }

    private async Task DeleteSelectedNoteAsync()
    {
        if (SelectedNote == null) return;

        await _mainNoteService.DeleteAsync(SelectedNote.Id);
        Notes.Remove(SelectedNote);

        await ReorderNotesAsync();
        SelectedNote = Notes.FirstOrDefault();
    }


    private async Task SaveNotesAsync()
    {
        var modifiedNotes = Notes.Where(n => n.HasChanges).ToList();
        if (modifiedNotes.Any())
        {
            await _mainNoteService.UpdateManyAsync(modifiedNotes);
            foreach (var note in modifiedNotes) note.AcceptChanges();
        }
    }

    private void MoveNote(int direction)
    {
        if (SelectedNote == null) return;

        int index = Notes.IndexOf(SelectedNote);
        int newIndex = index + direction;

        if (newIndex < 0 || newIndex >= Notes.Count) return;

        Notes.Move(index, newIndex);
        for (int i = 0; i < Notes.Count; i++)
        {
            Notes[i].OrderIndex = i;
        }

        _ = _mainNoteService.ReorderNotesAsync(Notes.Select(n => new MainNote
        {
            Id = n.Id,
            OrderIndex = n.OrderIndex,
            UserId = n.UserId
        }).ToList());
    }

    private bool CanMoveNote(int direction)
    {
        if (SelectedNote == null) return false;
        int index = Notes.IndexOf(SelectedNote);
        int newIndex = index + direction;
        return newIndex >= 0 && newIndex < Notes.Count;
    }

    private async Task ReorderNotesAsync()
    {
        for (int i = 0; i < Notes.Count; i++)
        {
            Notes[i].OrderIndex = i;
        }
        await _mainNoteService.UpdateManyAsync(Notes);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}