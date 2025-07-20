using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using AiLaTrieuPhu_Account.Helper;
using AiLaTrieuPhu_Account.Model;
using AiLaTrieuPhu_DEMO;

namespace Menu_Game
{

    public partial class MenuGame : Window
    {
        public MenuGame()
        {
            InitializeComponent();
            PlayMenuMusicOnce();
        }

        private void PlayMenuMusicOnce()
        {
            if (MenuMusicController.MenuMusicPlayer == null)
            {
                try
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Music", "menu.mp3");
                    if (File.Exists(path))
                    {
                        MediaPlayer player = new MediaPlayer();
                        player.Open(new Uri(path, UriKind.Absolute));
                        player.Volume = 0.1;

                        player.Play();

                        MenuMusicController.MenuMusicPlayer = player;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy file menu_theme.mp3 trong thư mục Music!", "Lỗi âm thanh", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi phát nhạc menu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            var loginWin = new AiLaTrieuPhu_Account.View.LoginWindow();
            var result = loginWin.ShowDialog();

            if (result == true)
            {
                var currentUser = AccountService.CurrentAccount;

                MenuMusicController.StopMenuMusic();

                if (currentUser != null && currentUser.Role == "Admin")
                {
                    MessageBox.Show("Đây là tài khoản Admin. Chuyển sang dashboard admin.");
                    this.Close();
                }
                else if (currentUser != null && currentUser.Role == "Guest")
                {
                    MainWindow menu = new MainWindow();
                    menu.Show();
                    this.Close();
                }
            }
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            MenuMusicController.StopMenuMusic();
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Close();
        }

        private void HowToPlay_Click(object sender, RoutedEventArgs e)
        {
            MenuMusicController.StopMenuMusic();
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
         
        }
    }

   
    public static class MenuMusicController
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public static MediaPlayer MenuMusicPlayer;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public static void StopMenuMusic()
        {
            if (MenuMusicPlayer != null)
            {
                MenuMusicPlayer.Stop();
                MenuMusicPlayer.Close();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                MenuMusicPlayer = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            }
        }
    }
}
