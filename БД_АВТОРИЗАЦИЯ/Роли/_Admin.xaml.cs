using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace БД_АВТОРИЗАЦИЯ
{
    /// <summary>
    /// Логика взаимодействия для _Admin.xaml
    /// </summary>
    public partial class _Admin : Page
    {
        public _Admin()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ___Без_имени_.Visibility = Visibility.Visible;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 1 clicked!");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 2 clicked!");
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 3 clicked!");
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 4 clicked!");
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 5 clicked!");
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 6 clicked!");
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button 7 clicked!");
        }

        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            ___Без_имени_.Visibility = Visibility.Hidden;
        }

        private void ___Без_имени__MouseEnter(object sender, MouseEventArgs e)
        {
            ___Без_имени_.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave_2(object sender, MouseEventArgs e)
        {
            ___Без_имени_.Visibility = Visibility.Hidden;
        }
    }
}
