using FocusPoint.BLL.Models.DtoModels;
using FocusPoint.PL.ViewModels;
using System;
using System.Collections.Generic;
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

namespace FocusPoint.PL.Views
{
    /// <summary>
    /// Interaction logic for AddWorkSessionView.xaml
    /// </summary>
    public partial class AddWorkSessionView : Window
    {
        public AddWorkSessionView(Guid userId, Action<WorkSessionDto> onConfirm)
        {
            InitializeComponent();

            var viewModel = new AddWorkSessionViewModel(userId, onConfirm, Close);
            DataContext = viewModel;
        }
    }
}
