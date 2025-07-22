using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Menu_Game;
using AiLaTrieuPhu_Account.Model;
using AiLaTrieuPhu_DEMO;


namespace AdminManagement.View
{
    public partial class AdminManagementWindow : Window
    {
        private ObservableCollection<Question> questionList = new ObservableCollection<Question>();
        private string filePath = @"D:\FPT_KI5\SWT\AiLaTrieuPhu\AiLaTrieuPhu_DEMO\question.json";
        private List<Question> originalList = new List<Question>();


        public AdminManagementWindow()
        {
            InitializeComponent();
            LoadQuestions();
            cmbLevel.SelectedIndex = 0; // Default to first option
        }

        private void LoadQuestions()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var list = JsonSerializer.Deserialize<List<Question>>(json);
                    questionList = new ObservableCollection<Question>(list ?? new List<Question>());
                    originalList = list ?? new List<Question>();

                }
                else
                {
                    MessageBox.Show("Không tìm thấy file: " + filePath);
                }

                questionDataGrid.ItemsSource = questionList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load file JSON: " + ex.Message);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize(questionList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            MessageBox.Show("✅ Danh sách câu hỏi đã được lưu!");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text) || cmbCorrect.SelectedItem == null)
            {
                MessageBox.Show("⚠️ Vui lòng nhập câu hỏi và chọn đáp án đúng");
                return;
            }


            questionList.Add(new Question
            {
                QuestionText = txtQuestion.Text,
                Options = new List<string> { txtA.Text, txtB.Text, txtC.Text, txtD.Text },
                CorrectAnswer = (cmbCorrect.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Level = cmbLevel.Text // dùng ComboBox thay vì TextBox
            });

            MessageBox.Show("✅ Thêm câu hỏi thành công!");
            ClearForm();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (questionDataGrid.SelectedItem is Question q)
            {
                q.QuestionText = txtQuestion.Text;
                q.Options[0] = txtA.Text;
                q.Options[1] = txtB.Text;
                q.Options[2] = txtC.Text;
                q.Options[3] = txtD.Text;
                q.CorrectAnswer = (cmbCorrect.SelectedItem as ComboBoxItem)?.Content.ToString();
                q.Level = cmbLevel.Text;

                questionDataGrid.Items.Refresh();
                MessageBox.Show("✅ Sửa câu hỏi thành công!");
                ClearForm();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (questionDataGrid.SelectedItem is Question q)
            {
                questionList.Remove(q);
                MessageBox.Show("🗑️ Xóa câu hỏi thành công!");
                ClearForm();
            }
        }

        private void questionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (questionDataGrid.SelectedItem is Question q)
            {
                txtQuestion.Text = q.QuestionText;
                txtA.Text = q.Options[0];
                txtB.Text = q.Options[1];
                txtC.Text = q.Options[2];
                txtD.Text = q.Options[3];
                cmbCorrect.SelectedItem = cmbCorrect.Items.Cast<ComboBoxItem>()
    .FirstOrDefault(item => item.Content.ToString() == q.CorrectAnswer);

                cmbLevel.Text = q.Level;
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("⚠️ Vui lòng nhập từ khóa tìm kiếm");
                return;
            }

            var filtered = originalList.Where(q => q.QuestionText.ToLower().Contains(keyword)).ToList();
            questionList = new ObservableCollection<Question>(filtered);
            questionDataGrid.ItemsSource = questionList;
        }

        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            questionList = new ObservableCollection<Question>(originalList);
            questionDataGrid.ItemsSource = questionList;
        }


        private void ClearForm()
        {
            txtQuestion.Text = "";
            txtA.Text = "";
            txtB.Text = "";
            txtC.Text = "";
            txtD.Text = "";
            cmbCorrect.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            questionDataGrid.SelectedItem = null;
        }


        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MenuGame menu = new MenuGame();
            menu.Show();
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            admindb menu = new admindb();
            menu.Show();
            this.Close();
        }

    }
}

