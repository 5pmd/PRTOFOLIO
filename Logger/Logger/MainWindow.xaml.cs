using Logger.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        private LoggerViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new LoggerViewModel();
            DataContext = _viewModel;
            // Example activity logging
            _viewModel.LogActivity("Application started.");

            SomeActivityMethod();

            for (int i = 0; i < 20; i++)
            {
                _viewModel.LogActivity($"Application started. {i}");
            }
        }

        private void SomeActivityMethod()
        {
            // Log some activities
            _viewModel.LogActivity("Some activity performed.");
        }
    }
}