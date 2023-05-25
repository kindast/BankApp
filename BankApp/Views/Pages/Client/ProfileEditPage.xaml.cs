using BankApp.ViewModels;
using System.Windows.Controls;

namespace BankApp.Views
{
    public partial class ProfileEditPage : Page
    {
        public ProfileEditPage(int userId)
        {
            InitializeComponent();
            DataContext = new ProfileEditViewModel(userId);
        }
    }
}
