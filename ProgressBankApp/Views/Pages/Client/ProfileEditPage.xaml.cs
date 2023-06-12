using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
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
