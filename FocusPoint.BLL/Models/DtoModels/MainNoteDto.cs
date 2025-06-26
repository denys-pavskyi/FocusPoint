using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FocusPoint.BLL.Models.DtoModels;

public class MainNoteDto : INotifyPropertyChanged
{
    private string _title = string.Empty;
    private string _content = string.Empty;
    private string _emoji = string.Empty;
    private int _orderIndex;

    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;
                HasChanges = true;
                OnPropertyChanged(nameof(Title));
            }
        }
    }

    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                HasChanges = true;
                OnPropertyChanged(nameof(Content));
            }
        }
    }

    public string Emoji
    {
        get => _emoji;
        set
        {
            if (_emoji != value)
            {
                _emoji = value;
                HasChanges = true;
                OnPropertyChanged(nameof(Emoji));
            }
        }
    }

    public int OrderIndex
    {
        get => _orderIndex;
        set
        {
            if (_orderIndex != value)
            {
                _orderIndex = value;
                HasChanges = true;
                OnPropertyChanged(nameof(OrderIndex));
            }
        }
    }

    [JsonIgnore]
    public bool HasChanges { get; private set; } = false;

    public void AcceptChanges()
    {
        HasChanges = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}