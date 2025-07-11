using AiLaTrieuPhu_Account.Helper;
using AiLaTrieuPhu_DEMO.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace AiLaTrieuPhu_Account.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register(object parameter)
        {
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng!");
                return;
            }
            if (Password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải ít nhất 6 ký tự!");
                return;
            }
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }

            var success = AccountService.Register(Username, Password, Email, "Guest");
            if (success)
            {
                MessageBox.Show("Đăng ký thành công! Đăng nhập lại để chơi game.");
                (parameter as Window)?.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc email đã tồn tại!");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
