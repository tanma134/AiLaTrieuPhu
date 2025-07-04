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
using static System.Formats.Asn1.AsnWriter;

namespace ResultWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ResultWindow1 : Window
    {
        public ResultWindow1(string message, string title, int score, int total)
        {
            InitializeComponent();
            this.Title = title;
            SetupResult(score, total);
        }
        private void SetupResult(int score, int total)
        {
            txtScore.Text = $"🎯 Điểm số: {score}/15 câu đúng";
            txtPercent.Text = $"📊 Tỷ lệ chính xác: {(score * 100.0 / total):F1}%";

            string[] prizes = {
                "100.000 VNĐ", "200.000 VNĐ", "300.000 VNĐ", "500.000 VNĐ", "1.000.000 VNĐ",
                "2.000.000 VNĐ", "3.000.000 VNĐ", "6.000.000 VNĐ", "10.000.000 VNĐ", "22.000.000 VNĐ",
                "30.000.000 VNĐ", "40.000.000 VNĐ", "60.000.000 VNĐ", "85.000.000 VNĐ", "150.000.000 VNĐ"
            };
            string finalPrize = score > 0 ? prizes[score - 1] : "0 VNĐ";
            txtPrize.Text = $"💰 Giải thưởng: {finalPrize}";

            if (score == 15)
                txtAchievement.Text = "🌟 XUẤT SẮC! Bạn là TRIỆU PHÚ thực thụ!";
            else if (score >= 12)
                txtAchievement.Text = "🏆 TUYỆT VỜI! Bạn là cao thủ!";
            else if (score >= 8)
                txtAchievement.Text = "👏 KHÁ TỐT! Bạn có tiềm năng!";
            else
                txtAchievement.Text = "💪 ĐỪNG NẢN LÒNG! Hãy thử lại nhé!";
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnRetry_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}