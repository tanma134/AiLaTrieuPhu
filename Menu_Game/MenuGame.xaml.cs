using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AiLaTrieuPhu_DEMO; // Thêm namespace chứa MainWindow

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
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close(); // Đóng cửa sổ game
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void HuongDan_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}