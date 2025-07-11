using System.Windows;
using AiLaTrieuPhu_Account.ViewModel;

namespace AiLaTrieuPhu_Account.View
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel vm)
            {
                vm.Password = passwordBox.Password;
                vm.ConfirmPassword = confirmBox.Password;
            }
        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
