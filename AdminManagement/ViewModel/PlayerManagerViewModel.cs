using AiLaTrieuPhu_Account.Model;
using AiLaTrieuPhu_Account.Helper;
using AdminManagement.Helper;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AdminManagement.ViewModel
{
    public class PlayerManagerViewModel : BaseViewModel
    {
        private ObservableCollection<Account> _accounts;
        public ObservableCollection<Account> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }

        public ICommand LockCommand { get; }
        public ICommand DeleteCommand { get; }

        public PlayerManagerViewModel()
        {
            Accounts = new ObservableCollection<Account>(AccountService.LoadAccounts());
            LockCommand = new RelayCommand(OnLockExecute);
            DeleteCommand = new RelayCommand(OnDeleteExecute);
        }

        private void OnLockExecute(object parameter)
        {
            if (parameter is Account account)
            {
                if (AccountService.ToggleLockAccount(account.Username))
                {
                    MessageBox.Show($"Tài khoản {account.Username} đã được {(account.IsLocked ? "mở khóa" : "khóa")}.");
                    Accounts = new ObservableCollection<Account>(AccountService.LoadAccounts());
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản.");
                }
            }
        }

        private void OnDeleteExecute(object parameter)
        {
            if (parameter is Account account)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản {account.Username}?", "Xác nhận xóa", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (AccountService.DeleteAccount(account.Username))
                    {
                        MessageBox.Show($"Tài khoản {account.Username} đã được xóa.");
                        Accounts = new ObservableCollection<Account>(AccountService.LoadAccounts());
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản.");
                    }
                }
            }
        }
    }

    public class BaseViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}