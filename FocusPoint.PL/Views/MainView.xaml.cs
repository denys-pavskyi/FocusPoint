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

        private readonly MainViewModel _viewModel;

        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _viewModel = mainViewModel;

            DataContext = mainViewModel;
            Loaded += MainView_Loaded;
        }


        private async void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.InitializeAsync();
        }

    }
}