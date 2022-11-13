using Microsoft.EntityFrameworkCore;
using Stroitel.Context;
using Stroitel.Models;
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

namespace Stroitel.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthStartupWindow.xaml
    /// </summary>
    public partial class AuthStartupWindow : Window
    {
        private MainWindow _mainWindow;

        public List<User> Users { get; set; }
        public AuthStartupWindow()
        {
            InitializeComponent();
            using (AppDbContext context = new())
            {
                Users = context.Users
                    .Include(u => u.UserRoleNavigation)
                    .ToList();
            }
        }

        private void GetAuth(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text;
            string password = PasswordPb.Password;
            try
            {
                var user = Users.Single(u => u.UserLogin == login && u.UserPassword == password);
                if (user.UserRoleNavigation.RoleName == "Клиент")
                {
                    _mainWindow = new(user.UserId);
                    _mainWindow.Show();
                    Close();
                }
            }
            catch 
            {
                ErrorTb.Visibility = Visibility.Visible;
            }
            
        }

        private void GuestAuth(object sender, RoutedEventArgs e)
        {
            _mainWindow = new();
            _mainWindow.Show();
            Close();
        }
    }
}
