using FocusPoint.BLL.Models.DtoModels;
using System.ComponentModel;

namespace FocusPoint.PL.ViewModels;

public class MainViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;


    public UserDto? CurrentUser { get; set; }

    public MainViewModel(UserDto? user = null)
    {
        CurrentUser = user;
    }

}