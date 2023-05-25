using BankApp.Helpers;
using BankApp.Models;
using BankApp.ViewModels;
using System.Windows.Controls;

namespace BankApp.Views
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
            MainFrame.Frame.Navigate(new ProfileEditPage(((sender as Button).DataContext as User).Id));
        }
    }
}
