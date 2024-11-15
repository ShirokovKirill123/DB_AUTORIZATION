using Bakery_Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace БД_АВТОРИЗАЦИЯ
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
            else
            {

                Bakery_Project.Authorization authpage = new Bakery_Project.Authorization();
                this.Content = authpage;                
            }
        }
    }
}
