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
    }
}
