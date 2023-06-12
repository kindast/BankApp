using ProgressBankApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CurrentWindow = this;
            DataContext = new MainViewModel(this);
        }

        private static MainWindow CurrentWindow { get; set; }

        public static void SetTitle(string title)
        {
            if (CurrentWindow != null)
                CurrentWindow.Title = "ПрогрессБанк - " + title;
        }

        public static void Navigate(Page page)
        {
            if (CurrentWindow != null)
                CurrentWindow.frame.Navigate(page);
        }
    }
}
