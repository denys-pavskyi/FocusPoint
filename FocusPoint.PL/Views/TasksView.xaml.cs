using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FocusPoint.PL.ViewModels;

namespace FocusPoint.PL.Views
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : UserControl
    {

        private TasksViewModel ViewModel => (TasksViewModel)DataContext;
        private bool _showCompleted = false;

        public TasksView()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is TasksViewModel vm && vm.EditTaskCommand.CanExecute(null))
            {
                vm.EditTaskCommand.Execute(null);
            }
        }

        private async void OnActiveTasksSelected(object sender, RoutedEventArgs e)
        {
            await ViewModel.SetFilterAsync(false);
        }

        private async void OnCompletedTasksSelected(object sender, RoutedEventArgs e)
        {
            await ViewModel.SetFilterAsync(true);
        }

    }
}
