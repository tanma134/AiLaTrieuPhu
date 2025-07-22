using System.Windows;
using AdminManagement.View;
using AiLaTrieuPhu_Account.Helper;  // Bạn phải Add Reference sang project Account
using AiLaTrieuPhu_Account.Model;
using Menu_Game;


namespace AdminManagement
{
    /// <summary>
    /// Interaction logic for MenuGame.xaml
    /// </summary>
    public partial class admindb : Window
    {
        public admindb()
        {
            InitializeComponent();
        }

       
        private void acc_Click(object sender, RoutedEventArgs e)
        {
            PlayerManager aboutUs = new PlayerManager();
            aboutUs.Show();
            this.Close();
        }
      
        private void hoi_Click(object sender, RoutedEventArgs e)
        {
            AdminManagementWindow howToPlay = new AdminManagementWindow();
            howToPlay.Show();
            this.Close();
        }
        
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MenuGame menu = new MenuGame();
            menu.Show();
            this.Close();
        }

    }
}
