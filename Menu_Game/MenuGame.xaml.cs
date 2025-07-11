using System.Windows;
using AiLaTrieuPhu_Account.Helper;  // Bạn phải Add Reference sang project Account
using AiLaTrieuPhu_Account.Model;
using AiLaTrieuPhu_DEMO;

namespace Menu_Game
{
    /// <summary>
    /// Interaction logic for MenuGame.xaml
    /// </summary>
    public partial class MenuGame : Window
    {
        public MenuGame()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Mở form đăng nhập (login dialog)
            var loginWin = new AiLaTrieuPhu_Account.View.LoginWindow();
            var result = loginWin.ShowDialog();

            if (result == true)
            {
                var currentUser = AccountService.CurrentAccount;
                if (currentUser != null && currentUser.Role == "Admin")
                {
                    // TODO: Thay thế bằng UI dashboard Admin thực tế của bạn
                    MessageBox.Show("Đây là tài khoản Admin. Chuyển sang dashboard admin.");
                    // new AiLaTrieuPhu_Account.View.AdminDashboardWindow().Show();
                    // this.Close();
                }
                else if (currentUser != null && currentUser.Role == "Guest")
                {
                    // Chuyển sang màn hình chơi game cho Guest
                    MainWindow menu = new MainWindow();
                    menu.Show();
                    this.Close();
                }
            }
            // Nếu đăng nhập sai hoặc user đóng login thì không làm gì, vẫn ở lại menu game.
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }

        private void HowToPlay_Click(object sender, RoutedEventArgs e)
        {
            HowToPlay howToPlay = new HowToPlay();
            howToPlay.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HuongDan_Click(object sender, RoutedEventArgs e)
        {
            // Mở hướng dẫn nếu bạn có chức năng này
        }
    }
}
