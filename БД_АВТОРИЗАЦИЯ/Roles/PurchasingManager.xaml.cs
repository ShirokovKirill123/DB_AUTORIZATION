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
    /// <summary>
    /// Логика взаимодействия для PurchasingManager.xaml
    /// </summary>
    public partial class PurchasingManager : Page
    {
        public PurchasingManager()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ___Без_имени_.Visibility = Visibility.Visible;
        }

        private void Button_Ingredients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ProductComposition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_suppliedIngredients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Suppliers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Products_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_OrderedProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Orders_Click(object sender, RoutedEventArgs e)
        {

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
