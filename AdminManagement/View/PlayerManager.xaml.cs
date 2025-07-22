using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace AdminManagement.View
{
    /// <summary>
    /// Interaction logic for PlayerManager.xaml
    /// </summary>
    public partial class PlayerManager : Window
    {
        public PlayerManager()
        {
            InitializeComponent();
        }
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AdminManagement.ViewModel.PlayerManagerViewModel viewModel)
            {
                if (viewModel.Accounts == null)
                {
                    MessageBox.Show("Danh sách tài khoản không được tải. Vui lòng kiểm tra file accounts.json.");
                }
            }
        }

     
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            admindb menu = new admindb();
            menu.Show();
            this.Close();
        }
    }

}