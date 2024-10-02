using LiquidMixerApp.Model;
using LiquidMixerApp.Model.Inventory;
using LiquidMixerApp.Model.Mixer;
using LiquidMixerApp.Model.SpeedGenerator;
using LiquidMixerApp.Model.TimeHandler;
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
            var liquidsToMix = (liquidAToMix, liquidBToMix, liquidCToMix);

            var liquidAToAdd = new Liquid("Liquid A");
            var liquidBToAdd = new Liquid("Liquid B");
            var liquidCToAdd = new Liquid("Liquid C");
            var liquidsToAdd = (liquidAToAdd, liquidBToAdd, liquidCToAdd);

            var speedGeneratorFactory = new SpeedGeneratorFactory();
            var mixer = new BasicMixer();
            var liquidInventory = new LocalLiquidInventory();
            var timeHandler = new DelayTimeHandler();
            var liquidMixer = new LiquidMixer(mixer,liquidInventory,timeHandler);
            this.DataContext = new MainWindowViewModel(liquidsToMix, liquidsToAdd, speedGeneratorFactory, liquidMixer);
        }
    }
}