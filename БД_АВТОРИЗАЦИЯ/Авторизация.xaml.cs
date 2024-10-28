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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace БД_АВТОРИЗАЦИЯ
{
    
    public partial class Авторизация : Page
    {
        public Авторизация()
        {
            InitializeComponent();
            Manager.MainFrame = SecondFrame;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new BakeryEntities())
            {
                var user = context.Roles.FirstOrDefault(u => u.role_email == username && u.role_password == password);

                if (user != null)
                {

                    MainWindow mainWindow = new MainWindow();

                    if (user.title_of_role == "Admin")
                    {
                        mainWindow.MainFrame.Navigate(new _Admin());
                    }

                    else if (user.title_of_role == "Purchasing Manager")
                    {
                        mainWindow.MainFrame.Navigate(new BakeryWorker());
                    }
                    else if (user.title_of_role == "Bakery Worker")
                    {
                        mainWindow.MainFrame.Navigate(new PurchasingManager());
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная роль пользователя");
                        return;
                    }

                    mainWindow.Show();
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль");
                }
            }
        }
    }
}
