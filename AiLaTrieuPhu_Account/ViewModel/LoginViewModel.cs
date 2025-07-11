using AiLaTrieuPhu_Account.Helper;
using AiLaTrieuPhu_Account.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AiLaTrieuPhu_DEMO;
using AiLaTrieuPhu_DEMO.Helper; // Nếu MainWindow ở đây
// using YourAdminNamespace; // Nếu AdminDashboardWindow ở namespace khác

namespace AiLaTrieuPhu_Account.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            var acc = AccountService.Login(Username, Password);
            if (acc != null)
            {
                var win = parameter as Window;
                win?.Hide();

                // HIỆN THÔNG BÁO CHÀO MỪNG NGƯỜI DÙNG
                MessageBox.Show($"Chào mừng, {acc.Username}!", "Đăng nhập thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                if (acc.Role == "Admin")
                {
                    // Mở giao diện admin
                    // var adminWin = new AdminDashboardWindow();
                    // adminWin.Show();
                    MessageBox.Show("Chuyển đến Admin Dashboard (bạn hãy mở AdminDashboardWindow ở đây)");
                }
                else
                {
                    var gameWin = new AiLaTrieuPhu_DEMO.MainWindow();
                    gameWin.Show();
                }

                win?.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }


        public void Login(Window window)
        {
            ExecuteLogin(window);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
