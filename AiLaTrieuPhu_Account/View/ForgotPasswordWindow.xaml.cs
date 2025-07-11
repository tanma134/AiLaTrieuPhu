using System;
using System.Windows;
using AiLaTrieuPhu_Account.Helper;

namespace AiLaTrieuPhu_Account.View
{
    public partial class ForgotPasswordWindow : Window
    {
        private string currentOtp = "";
        private DateTime otpTime;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        // Gửi mã OTP
        private void SendOtp_Click(object sender, RoutedEventArgs e)
        {
            var email = emailBox.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Nhập email trước!");
                return;
            }
            var acc = AccountService.FindByEmail(email);
            if (acc == null)
            {
                MessageBox.Show("Email chưa đăng ký!");
                return;
            }
            // Sinh OTP
            var rand = new Random();
            currentOtp = rand.Next(100000, 999999).ToString();
            otpTime = DateTime.Now;
            if (EmailHelper.SendOtp(email, currentOtp, out string err))
            {
                MessageBox.Show("OTP đã gửi! Kiểm tra email trong 3 phút.");
            }
            else
            {
                MessageBox.Show("Lỗi gửi email: " + err);
            }
        }

        // Đặt lại mật khẩu
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            var email = emailBox.Text.Trim();
            var otp = otpBox.Text.Trim();
            var newPass = newPassBox.Password;
            var confirm = confirmBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Điền đủ tất cả các trường!");
                return;
            }
            if (currentOtp == "" || (DateTime.Now - otpTime).TotalMinutes > 3)
            {
                MessageBox.Show("OTP chưa gửi hoặc đã hết hạn!");
                return;
            }
            if (otp != currentOtp)
            {
                MessageBox.Show("Mã OTP không đúng!");
                return;
            }
            if (newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới tối thiểu 6 ký tự!");
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }
            var ok = AccountService.ChangePassword(email, newPass);
            if (ok)
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản hoặc lỗi ghi file!");
            }
        }
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}
