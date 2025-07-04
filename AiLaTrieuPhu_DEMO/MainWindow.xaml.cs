using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;
using ResultWindow;


namespace AiLaTrieuPhu_DEMO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int score = 0;
        private DispatcherTimer timer;
        private int timeLeft = 30;
        private bool used5050 = false;
        public MainWindow()
        {
            InitializeComponent();

            // Khởi tạo timer trước
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            LoadQuestions();      // Load file json
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayQuestion(); // Bây giờ UI đã load xong, txtTimer không còn null
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (txtTimer == null)
            {
                MessageBox.Show("txtTimer đang null!");
                return;
            }

            txtTimer.Text = $"⏰ {timeLeft}s";

            timeLeft--;
            if (timeLeft <= 0)
            {
                timer.Stop();
                MessageBox.Show("Hết thời gian!");
                Application.Current.Shutdown();
            }
        }


        private void LoadQuestions()
        {
            string json = File.ReadAllText("question.json");
            var allQuestions = JsonSerializer.Deserialize<List<Question>>(json);

            // Tách câu hỏi theo mức độ
            var easyQuestions = allQuestions.Where(q => q.Level == "easy").ToList();
            var mediumQuestions = allQuestions.Where(q => q.Level == "medium").ToList();
            var hardQuestions = allQuestions.Where(q => q.Level == "hard").ToList();

            Random rand = new Random();

            // Chọn ngẫu nhiên từng nhóm theo vị trí
            var selectedEasy = easyQuestions.OrderBy(q => rand.Next()).Take(4).ToList();     // Câu 1–4
            var selectedMedium = mediumQuestions.OrderBy(q => rand.Next()).Take(5).ToList(); // Câu 5–9
            var selectedHard = hardQuestions.OrderBy(q => rand.Next()).Take(6).ToList();     // Câu 10–15

            // Gộp lại theo thứ tự dễ → trung bình → khó
            questions = new List<Question>();
            questions.AddRange(selectedEasy);
            questions.AddRange(selectedMedium);
            questions.AddRange(selectedHard);

            currentQuestionIndex = 0;
            score = 0;
        }



        private void DisplayQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                timer.Stop();

                // Sử dụng WPF ResultWindow thay vì WinForms
                var resultWindow = new ResultWindow1("Hoàn thành!", "🎉 KẾT QUẢ CUỐI CÙNG", score, 15);
                bool? result = resultWindow.ShowDialog();

                SaveHighScore(score);
                return;
            }

            var q = questions[currentQuestionIndex];
            txtQuestion.Text = q.QuestionText;
            // ✅ Cập nhật thông tin câu hỏi và giải thưởng hiện tại
            txtQuestionNumber.Text = $"Câu hỏi: {currentQuestionIndex + 1}/15";

            // Cập nhật giải thưởng hiện tại theo thang điểm
            string[] prizes = {
        "100.000 VNĐ",    // Câu 1
        "200.000 VNĐ",    // Câu 2
        "300.000 VNĐ",    // Câu 3
        "500.000 VNĐ",    // Câu 4
        "1.000.000 VNĐ",  // Câu 5 - Mốc an toàn
        "2.000.000 VNĐ",  // Câu 6
        "3.000.000 VNĐ",  // Câu 7
        "6.000.000 VNĐ",  // Câu 8
        "10.000.000 VNĐ", // Câu 9
        "22.000.000 VNĐ", // Câu 10 - Mốc an toàn
        "30.000.000 VNĐ", // Câu 11
        "40.000.000 VNĐ", // Câu 12
        "60.000.000 VNĐ", // Câu 13
        "85.000.000 VNĐ", // Câu 14
        "150.000.000 VNĐ" // Câu 15 - Giải cao nhất
    };

            txtCurrentPrize.Text = prizes[currentQuestionIndex];

            // ✅ Highlight câu hỏi hiện tại trên thang điểm
            HighlightCurrentPrize(currentQuestionIndex + 1);

            // ✅ Reset nội dung và trạng thái của nút (khắc phục lỗi 50:50 bị lưu từ câu trước)
            btnA.Content = "A. " + q.Options[0];
            btnB.Content = "B. " + q.Options[1];
            btnC.Content = "C. " + q.Options[2];
            btnD.Content = "D. " + q.Options[3];

            btnA.IsEnabled = true;
            btnB.IsEnabled = true;
            btnC.IsEnabled = true;
            btnD.IsEnabled = true;

            // Nếu chưa dùng 50:50 thì bật nút, nếu đã dùng thì giữ disable
            btn5050.IsEnabled = !used5050;

            // Reset timer
            timeLeft = 30;
            txtTimer.Text = $"⏰ {timeLeft}s";
            timer.Start();

            panelExperts.Visibility = Visibility.Collapsed;
            expert1.Text = "";
            expert2.Text = "";
            expert3.Text = "";
        }


        private void CheckAnswer(string selected)
        {
            var correct = questions[currentQuestionIndex].CorrectAnswer;
            if (selected == correct)
            {
                score++;

                // 👉 Kiểm tra nếu đúng câu 5 (index 4) hoặc câu 10 (index 9)
                if (currentQuestionIndex == 4 || currentQuestionIndex == 9)
                {
                    string[] prizes = {
                "100.000 VNĐ", "200.000 VNĐ", "300.000 VNĐ", "500.000 VNĐ", "1.000.000 VNĐ",
                "2.000.000 VNĐ", "3.000.000 VNĐ", "6.000.000 VNĐ", "10.000.000 VNĐ", "22.000.000 VNĐ",
                "30.000.000 VNĐ", "40.000.000 VNĐ", "60.000.000 VNĐ", "85.000.000 VNĐ", "150.000.000 VNĐ"
            };

                    // Hiển thị hộp thoại xác nhận
                    MessageBoxResult result = MessageBox.Show(
                        $"🎯 Bạn đã trả lời đúng câu {currentQuestionIndex + 1}!\n" +
                        $"Bạn có muốn tiếp tục để giành giải cao hơn không?\n" +
                        $"👉 Nếu dừng lại, bạn sẽ nhận {prizes[currentQuestionIndex]}",
                        "Tiếp tục hay Dừng lại?",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );

                    if (result == MessageBoxResult.No)
                    {
                        timer.Stop();
                        var resultWindow = new ResultWindow1("Bạn đã dừng cuộc chơi!", "📦 NHẬN GIẢI", score, 15);
                        resultWindow.ShowDialog();
                        SaveHighScore(score);
                        Application.Current.Shutdown();
                        return;
                    }
                }

                currentQuestionIndex++;
                DisplayQuestion();
            }
            else
            {
                timer.Stop();

                string[] prizes = {
            "100.000 VNĐ", "200.000 VNĐ", "300.000 VNĐ", "500.000 VNĐ", "1.000.000 VNĐ",
            "2.000.000 VNĐ", "3.000.000 VNĐ", "6.000.000 VNĐ", "10.000.000 VNĐ", "22.000.000 VNĐ",
            "30.000.000 VNĐ", "40.000.000 VNĐ", "60.000.000 VNĐ", "85.000.000 VNĐ", "150.000.000 VNĐ"
        };

                int safeScore = 0;
                string safePrize = "0 VNĐ";

                // 👇 Xác định mốc an toàn gần nhất
                if (currentQuestionIndex >= 5 && currentQuestionIndex <= 9)
                {
                    safeScore = 5;
                    safePrize = prizes[4]; // Mốc câu 5
                }
                else if (currentQuestionIndex >= 10)
                {
                    safeScore = 10;
                    safePrize = prizes[9]; // Mốc câu 10
                }

                var resultWindow = new ResultWindow1("Bạn đã thua cuộc!", $"💥 GAME OVER\n🎁 Giải thưởng an toàn: {safePrize}", safeScore, 15);
                resultWindow.ShowDialog();
                SaveHighScore(safeScore);
                Application.Current.Shutdown();
            }
        }



        private void btnA_Click(object sender, RoutedEventArgs e) => CheckAnswer("A");
        private void btnB_Click(object sender, RoutedEventArgs e) => CheckAnswer("B");
        private void btnC_Click(object sender, RoutedEventArgs e) => CheckAnswer("C");
        private void btnD_Click(object sender, RoutedEventArgs e) => CheckAnswer("D");

        private void btn5050_Click(object sender, RoutedEventArgs e)
        {
            if (used5050) return; // nếu đã dùng rồi thì không làm gì cả

            used5050 = true;             // đánh dấu đã dùng
            btn5050.IsEnabled = false;   // tắt nút 50:50

            var correct = questions[currentQuestionIndex].CorrectAnswer;

            var buttons = new Dictionary<string, Button>
    {
        { "A", btnA },
        { "B", btnB },
        { "C", btnC },
        { "D", btnD }
    };

            // lấy các đáp án sai
            var wrongOptions = buttons.Keys.Where(k => k != correct).ToList();
            Random rand = new Random();
            var toRemove = wrongOptions.OrderBy(x => rand.Next()).Take(2);

            foreach (var key in toRemove)
            {
                buttons[key].IsEnabled = false;
                buttons[key].Content = ""; // ẩn nội dung
            }
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            btnPause.IsEnabled = true;
            btnResume.IsEnabled = false;
            timer.Start();
            btnA.IsEnabled = btnB.IsEnabled = btnC.IsEnabled = btnD.IsEnabled = true;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            btnPause.IsEnabled = false;
            btnResume.IsEnabled = true;
            timer.Stop();
            btnA.IsEnabled = btnB.IsEnabled = btnC.IsEnabled = btnD.IsEnabled = false;
        }
        private void SaveHighScore(int score)
        {
            int highScore = 0;
            string path = "highscore.txt";
            if (File.Exists(path))
            {
                int.TryParse(File.ReadAllText(path), out highScore);
            }

        }
        private string SimulateExpertAnswer(string correctAnswer, int questionIndex)
        {
            Random rnd = new Random();
            int chance = rnd.Next(100);

            // Câu dễ thì chuyên gia dễ đúng hơn
            int correctRate = 90 - questionIndex * 5;
            if (correctRate < 30) correctRate = 30;

            if (chance < correctRate)
                return correctAnswer;
            else
            {
                var options = new[] { "A", "B", "C", "D" };
                return options.Where(opt => opt != correctAnswer).OrderBy(x => rnd.Next()).First();
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            // Reset game state
            currentQuestionIndex = 0;
            score = 0;
            timeLeft = 30;
            used5050 = false;

            // Reset UI elements
            txtQuestionNumber.Text = "Câu hỏi: 1/15";
            txtCurrentPrize.Text = "100.000 VNĐ";
            txtTimer.Text = "30";
            txtQuestion.Text = "Chào mừng bạn đến với chương trình 'Ai là triệu phú'! Hãy sẵn sàng để thử thách kiến thức của mình.";

            // Reset answer buttons
            btnA.Content = "A. Đáp án A";
            btnB.Content = "B. Đáp án B";
            btnC.Content = "C. Đáp án C";
            btnD.Content = "D. Đáp án D";

            btnA.IsEnabled = true;
            btnB.IsEnabled = true;
            btnC.IsEnabled = true;
            btnD.IsEnabled = true;

            // Reset answer button colors to default
            btnA.ClearValue(Button.BackgroundProperty);
            btnB.ClearValue(Button.BackgroundProperty);
            btnC.ClearValue(Button.BackgroundProperty);
            btnD.ClearValue(Button.BackgroundProperty);

            // Reset help buttons
            btn5050.IsEnabled = true;
            btnPause.IsEnabled = true;
            btnResume.IsEnabled = false;

            // Reset prize ladder highlighting
            ResetPrizeLadder();
            HighlightCurrentPrize(1); // Highlight first prize

            // Hide game status
            borderStatus.Visibility = Visibility.Collapsed;

            // Stop any running timer
            if (timer != null)
            {
                timer.Stop();
            }

            // Load first question if you have a question loading method
            // LoadQuestion(currentQuestionIndex);

            // Start new game timer
            timer.Start();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            // Show confirmation dialog
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát game không?\nTiến trình hiện tại sẽ bị mất!",
                "Xác nhận thoát",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Stop timer if running
                if (timer != null)
                {
                    timer.Stop();
                }

                // Close application
                Application.Current.Shutdown();

                // Alternative: Close only this window if you have multiple windows
                // this.Close();
            }
        }

        // Helper method to reset prize ladder colors
        private void ResetPrizeLadder()
        {
            // Reset all prize items to default color
            prize1.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246)); // #FFF3F4F6
            prize2.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize3.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize4.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize5.Background = new SolidColorBrush(Color.FromRgb(251, 191, 36)); // Milestone - #FFFBBF24
            prize6.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize7.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize8.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize9.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize10.Background = new SolidColorBrush(Color.FromRgb(251, 191, 36)); // Milestone
            prize11.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize12.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize13.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize14.Background = new SolidColorBrush(Color.FromRgb(243, 244, 246));
            prize15.Background = new SolidColorBrush(Color.FromRgb(251, 191, 36)); // Final milestone
        }

        // Helper method to highlight current prize level
        private void HighlightCurrentPrize(int questionNumber)
        {
            // Reset all first
            ResetPrizeLadder();

            // Highlight current prize with blue glow
            SolidColorBrush currentBrush = new SolidColorBrush(Color.FromRgb(59, 130, 246)); // #FF3B82F6

            switch (questionNumber)
            {
                case 1: prize1.Background = currentBrush; break;
                case 2: prize2.Background = currentBrush; break;
                case 3: prize3.Background = currentBrush; break;
                case 4: prize4.Background = currentBrush; break;
                case 5: prize5.Background = currentBrush; break;
                case 6: prize6.Background = currentBrush; break;
                case 7: prize7.Background = currentBrush; break;
                case 8: prize8.Background = currentBrush; break;
                case 9: prize9.Background = currentBrush; break;
                case 10: prize10.Background = currentBrush; break;
                case 11: prize11.Background = currentBrush; break;
                case 12: prize12.Background = currentBrush; break;
                case 13: prize13.Background = currentBrush; break;
                case 14: prize14.Background = currentBrush; break;
                case 15: prize15.Background = currentBrush; break;
            }
        }
        private bool usedExperts = false;
        private void btnExperts_Click(object sender, RoutedEventArgs e)
        {
            if (usedExperts) return;

            usedExperts = true;
            btnExperts.IsEnabled = false;

            var correct = questions[currentQuestionIndex].CorrectAnswer;

            expert1.Text = $"👤 Mr.Vương: Tôi nghĩ là {SimulateExpertAnswer(correct, currentQuestionIndex)}";
            expert2.Text = $"👤 Mr.ĐA: Tôi chọn đáp án {SimulateExpertAnswer(correct, currentQuestionIndex)}";
            expert3.Text = $"👤 Mr.Huấn: Theo tôi là {SimulateExpertAnswer(correct, currentQuestionIndex)}";
            expert4.Text = $"👤 Mr.Thanh: Theo tôi là {SimulateExpertAnswer(correct, currentQuestionIndex)}";

            panelExperts.Visibility = Visibility.Visible;
        }
    }

}
