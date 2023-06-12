using ProgressBankApp.Helpers;
using ProgressBankApp.Models;

namespace ProgressBankApp.ViewModels
{
    public class DepositAgreementViewModel : ViewModel
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private Deposit _deposit;
        public Deposit Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
                OnPropertyChanged(nameof(Deposit));
            }
        }

        public DepositAgreementViewModel(User user, Deposit deposit)
        {
            User = user;
            Deposit = deposit;
        }
    }
}
