using System.Net;
using System.Net.Mail;

namespace AiLaTrieuPhu_Account.Helper
{
    public static class EmailHelper
    {
        // Thay đổi tài khoản này sang Gmail của bạn
        public static readonly string SenderEmail = "tainhce181569@fpt.edu.vn";
        public static readonly string AppPassword = "rzcy dele mubb taao";

        public static bool SendOtp(string toEmail, string otpCode, out string error)
        {
            error = "";
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(SenderEmail, AppPassword),
                    EnableSsl = true
                };

                var mail = new MailMessage(
                    from: SenderEmail,
                    to: toEmail,
                    subject: "Mã OTP xác thực đổi mật khẩu",
                    body: $"Mã OTP của bạn là: {otpCode}\n(Hiệu lực trong 3 phút)");

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
