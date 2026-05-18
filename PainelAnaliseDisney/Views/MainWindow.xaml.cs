using System.Windows;
using PainelAnaliseDisney.ViewModels;

namespace PainelAnaliseDisney.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Ligação crucial do padrão arquitetural MVVM:
            // Define a MainViewModel como a fonte oficial de dados e comandos desta View.
            this.DataContext = new MainViewModel();
        }
    }
}