using System.Windows;
using FocusPoint.BLL.Models;
using FocusPoint.DAL.Configurations;
using FocusPoint.PL.ViewModels;

namespace FocusPoint.PL
{

    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}