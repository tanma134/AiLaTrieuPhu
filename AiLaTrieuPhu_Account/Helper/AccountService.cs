using AiLaTrieuPhu_Account.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace AiLaTrieuPhu_Account.Helper
{
    public static class AccountService
    {
        // Đường dẫn tới accounts.json nằm ở thư mục project

        private static readonly string FilePath = Path.Combine(
     Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName,
     "AiLaTrieuPhu_Account", "accounts.json");




        public static Account CurrentAccount { get; set; }

        // Load all accounts
        public static List<Account> LoadAccounts()
        {
            if (!File.Exists(FilePath))
                return new List<Account>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
        }

        // Save all accounts
        private static void SaveAccounts(List<Account> accounts)
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(accounts, new JsonSerializerOptions { WriteIndented = true }));
        }

        // Login by username & password
        public static Account Login(string username, string password)
        {
            var accounts = LoadAccounts();
            var acc = accounts.FirstOrDefault(a =>
                a.Username.Trim().Equals(username.Trim(), StringComparison.OrdinalIgnoreCase) &&
                a.Password == password);
           

            if (acc != null) CurrentAccount = acc;
            return acc;

        }

        // Register new account (with email, check duplicate username/email)
        public static bool Register(string username, string password, string email, string role = "Guest")
        {
            try
            {
                var accounts = LoadAccounts();
                if (accounts.Any(a => a.Username == username || a.Email == email))
                    return false;

                var newAcc = new Account
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    Role = role
                };
                accounts.Add(newAcc);

                SaveAccounts(accounts);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Find by email (for forgot password)
        public static Account FindByEmail(string email)
        {
            var accounts = LoadAccounts();
            return accounts.FirstOrDefault(a => a.Email == email);
        }

        // Change password by email (forgot password)
        public static bool ChangePassword(string email, string newPassword)
        {
            var accounts = LoadAccounts();
            var acc = accounts.FirstOrDefault(a => a.Email == email);
            if (acc == null) return false;
            acc.Password = newPassword;
            SaveAccounts(accounts);
            return true;
        }

        // Debug method
        public static string GetFilePath() => FilePath;
    }
}
