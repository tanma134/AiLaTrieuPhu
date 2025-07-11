using System.Windows;
using AiLaTrieuPhu_Account.ViewModel;

namespace AiLaTrieuPhu_Account.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                // Gán password từ PasswordBox vào ViewModel
                vm.Password = passwordBox.Password;

                // Kiểm tra thông tin đầu vào
                if (!string.IsNullOrEmpty(vm.Username) && !string.IsNullOrEmpty(vm.Password))
                {
                    vm.Login(this);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var regWin = new RegisterWindow();
            regWin.ShowDialog();
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            var forgotWin = new ForgotPasswordWindow();
            forgotWin.ShowDialog();
        }
    }
}