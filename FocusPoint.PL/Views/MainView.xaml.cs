using System.Windows;
using System.Windows.Controls;
using FocusPoint.BLL.Models;
using FocusPoint.DAL.Configurations;
using FocusPoint.PL.ViewModels;
using FocusPoint.PL.Views;

namespace FocusPoint.PL
{

    public partial class MainView : Window
    {
        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

    }
}