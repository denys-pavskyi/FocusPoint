using FocusPoint.BLL.Models;
using System.ComponentModel;

namespace FocusPoint.PL.ViewModels;

public class MainViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;


    public UserDto CurrentUser { get; }

    public MainViewModel(UserDto user)
    {
        CurrentUser = user;
    }

}