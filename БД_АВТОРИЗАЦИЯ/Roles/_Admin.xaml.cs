using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using Bakery_Project;

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

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SideMenu.Visibility = SideMenu.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SideMenu.Visibility == Visibility.Visible)
            {
                Point clickPosition = e.GetPosition(this);

                if (!IsPointInsideElement(SideMenu, clickPosition) && !IsPointInsideElement(MenuButton, clickPosition))
                {
                    SideMenu.Visibility = Visibility.Hidden;
                }
            }
        }

        private bool IsPointInsideElement(FrameworkElement element, Point point)
        {
            Rect bounds = new Rect(element.TranslatePoint(new Point(0, 0), this), element.RenderSize);

            return bounds.Contains(point);
        }

        private void Button_Ingredients_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities4())
            {
                var ingredientsList = context.Ingredients.ToList();
                IngredientsDataGrid.ItemsSource = ingredientsList;
            }
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
    }
}
