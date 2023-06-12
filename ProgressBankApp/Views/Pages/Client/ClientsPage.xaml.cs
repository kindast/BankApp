using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            DataContext = new ClientsViewModel();
        }

        private void btnEditProfile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.Navigate(new ProfileEditPage(((sender as Button).DataContext as User).Id));
        }
    }
}
