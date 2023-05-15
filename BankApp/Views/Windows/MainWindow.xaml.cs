using BankApp.Helpers;
using BankApp.ViewModels;
using System.Windows;

namespace BankApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Frame = frame;
            DataContext = new MainViewModel();
        }
    }
}
