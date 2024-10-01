using LiquidMixerApp.Model;
using LiquidMixerApp.Model.SpeedGenerator;
using LiquidMixerApp.ViewModel;
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

namespace LiquidMixerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            var liquidAToMix = new Liquid("Liquid A");
            var liquidBToMix = new Liquid("Liquid B");
            var liquidCToMix = new Liquid("Liquid C");
            var liquidAToAdd = new Liquid("Liquid A");
            var liquidBToAdd = new Liquid("Liquid B");
            var liquidCToAdd = new Liquid("Liquid C");
            var speedGeneratorFactory = new SpeedGeneratorFactory();
            this.DataContext = new MainWindowViewModel(liquidAToMix, liquidBToMix, liquidCToMix, liquidAToAdd, liquidBToAdd, liquidCToAdd, speedGeneratorFactory);
        }
    }
}