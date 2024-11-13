using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
using БД_АВТОРИЗАЦИЯ.Хранитель_Memento_;

namespace БД_АВТОРИЗАЦИЯ
{    
    public partial class Авторизация : Page
    {

        // Создание объектов для паттерна Memento
        private Caretaker _caretaker = new Caretaker();
        private Originator _originator = new Originator();

        private const string MementoFileName = "memento.json";

        public Авторизация()
        {
            InitializeComponent();
            Manager.MainFrame = SecondFrame;
            RestoreLoginFromMemento();            
        }

        private void RestoreLoginFromMemento()
        {
            if (File.Exists(MementoFileName))
            {
                string json = File.ReadAllText(MementoFileName);
                _caretaker.Memento = JsonConvert.DeserializeObject<Memento>(json);

                if (_caretaker.Memento != null)
                {
                    WatermarkText.Text = _caretaker.Memento.Username;
                }
            }
        }

        private void SaveLoginToMemento()
        {
            string json = JsonConvert.SerializeObject(_caretaker.Memento);
            File.WriteAllText(MementoFileName, json);
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginTextBox.Text))
            {
                WatermarkText.Visibility = Visibility.Collapsed;
            }
            else
            {
                WatermarkText.Visibility = Visibility.Visible;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new BakeryEntities2())
            {
                var user = context.Roles.FirstOrDefault(u => u.role_email == username && u.role_password == password);

                if (user != null)
                {
                    _originator.Username = username;
                    _originator.Role = user.title_of_role;
                    _caretaker.Memento = _originator.CreateMemento(); // Сохранение состояния в Memento                    

                    SaveLoginToMemento();

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
