using System.Windows;
using FocusPoint.BLL.Models;
using FocusPoint.DAL.Configurations;
using FocusPoint.PL.ViewModels;

namespace FocusPoint.PL
{

    public partial class MainWindow : Window
    {
        public MainWindow(UserDto userModel)
        {
            InitializeComponent();
            DataContext = new MainViewModel(userModel);
        }
    }
}