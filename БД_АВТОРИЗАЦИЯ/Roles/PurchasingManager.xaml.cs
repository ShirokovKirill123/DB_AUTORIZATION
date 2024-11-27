using Bakery_Project;
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

using System.Data.Entity; // Для Entity Framework 6
using Bakery_Project.Observer;

namespace БД_АВТОРИЗАЦИЯ
{
    /// <summary>
    /// Логика взаимодействия для PurchasingManager.xaml
    /// </summary>
    public partial class PurchasingManager : Page
    {
        private string currentTable;
        private PurchasingManagerObserver _purchasingManagerObserver;

        public PurchasingManager()
        {
            InitializeComponent();

            _purchasingManagerObserver = new PurchasingManagerObserver();

            this.DataContext = _purchasingManagerObserver;

            var ingredientStockNotifier = IngredientStockNotifier.Current;

            ingredientStockNotifier.AddObserver(_purchasingManagerObserver);
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
            using (var context = new BakeryEntities5())
            {
                var ingredientsList = context.Ingredients.ToList();

                DataGrid.ItemsSource = ingredientsList;
                currentTable = "Ingredients";

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Название ингредиента", Binding = new Binding("iName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Единица измерения", Binding = new Binding("unitOfMeasurement") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена за единицу(₽)", Binding = new Binding("pricePerUnit") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Доступное количество", Binding = new Binding("availableQuantity") });

                StackPanelVisibility();
            }
        }       

        private void Button_suppliedIngredients_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities5())
            {
                var suppliedIngredientsList = context.suppliedIngredients
                    .Include(si => si.Ingredients)
                    .Include(si => si.Suppliers)
                    .ToList();

                foreach (var suppliedIngredient in suppliedIngredientsList)
                {
                    var ingredient = suppliedIngredient.Ingredients;
                    if (ingredient != null)
                    {
                        var ingredientQuantity = suppliedIngredient.ingredient_quntity;
                        suppliedIngredient.price = ingredient.pricePerUnit * ingredientQuantity;
                    }
                }

                DataGrid.ItemsSource = suppliedIngredientsList;
                currentTable = "suppliedIngredients";

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID поставщика", Binding = new Binding("supplier_id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID ингредиента", Binding = new Binding("ingredient_id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Поставщики", Binding = new Binding("Suppliers.sName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Ингредиенты", Binding = new Binding("Ingredients.iName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество ингредиента", Binding = new Binding("ingredient_quntity") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена(₽)", Binding = new Binding("price") });

                DataGrid.Columns[4].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
                DataGrid.Columns[5].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);

                StackPanelVisibility();
            }
        }

        private void Button_Suppliers_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities5())
            {
                var suppliersList = context.Suppliers.ToList();

                DataGrid.ItemsSource = suppliersList;
                currentTable = "Suppliers";

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Название поставщика", Binding = new Binding("sName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Контактная информация", Binding = new Binding("contactInformation") });

                StackPanelVisibility();
            }
        }

        private void Button_Products_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities5())
            {
                var productsList = context.Products.ToList();

                DataGrid.ItemsSource = productsList;
                currentTable = "Products";

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Название продукта", Binding = new Binding("pName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Тип продукта", Binding = new Binding("ptype") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена за единицу", Binding = new Binding("pricePerUnit") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Объем выпуска", Binding = new Binding("outputVolume") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Срок хранения", Binding = new Binding("shelflife") });

                DataGrid.Columns[1].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
                DataGrid.Columns[2].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
                DataGrid.Columns[5].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);

                StackPanelVisibility();
            }
        }       

        private void StackPanelVisibility()
        {
            if (DataGrid.Items.Count == 0)
            {
                StackPanel2.Visibility = Visibility.Hidden;
            }
            else
            {
                StackPanel2.Visibility = Visibility.Visible;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities5())
            {
                switch (currentTable)
                {
                    case "Products":
                        var newProduct = new Products
                        {
                            pName = "",
                            ptype = "",
                            pricePerUnit = 0m,
                            outputVolume = 0,
                            shelflife = DateTime.Now
                        };
                        context.Products.Add(newProduct);
                        break;

                    case "Ingredients":
                        var newIngredient = new Ingredients
                        {
                            iName = "",
                            unitOfMeasurement = "",
                            pricePerUnit = 0m,
                            availableQuantity = 0
                        };
                        context.Ingredients.Add(newIngredient);
                        break;

                    case "Suppliers":
                        var newSupplier = new Suppliers
                        {
                            sName = "",
                            contactInformation = ""
                        };
                        context.Suppliers.Add(newSupplier);
                        break;                                      

                    case "suppliedIngredients":
                        var newSuppliedIngredient = new suppliedIngredients
                        {
                            supplier_id = 1,
                            ingredient_id = 1,
                            ingredient_quntity = 0,
                            price = 0m
                        };
                        context.suppliedIngredients.Add(newSuppliedIngredient);
                        break;

                    default:
                        MessageBox.Show("Неизвестная таблица для добавления.");
                        return;
                }

                context.SaveChanges();
                RefreshDataGrid();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities5())
            {
                if (currentTable == "Ingredients")
                {
                    var ingredientsFromGrid = DataGrid.ItemsSource as List<Ingredients>;

                    if (ingredientsFromGrid != null)
                    {
                        foreach (var ingredient in ingredientsFromGrid)
                        {
                            if (ingredient.id == 0)
                            {
                                context.Ingredients.Add(ingredient);
                            }
                            else
                            {
                                var existingIngredient = context.Ingredients.Find(ingredient.id);
                                if (existingIngredient != null)
                                {
                                    context.Entry(existingIngredient).CurrentValues.SetValues(ingredient);
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }
                else if (currentTable == "Products")
                {
                    var productsFromGrid = DataGrid.ItemsSource as List<Products>;

                    if (productsFromGrid != null)
                    {
                        foreach (var product in productsFromGrid)
                        {
                            if (product.id == 0)
                            {
                                context.Products.Add(product);
                            }
                            else
                            {
                                var existingProduct = context.Products.Find(product.id);
                                if (existingProduct != null)
                                {
                                    context.Entry(existingProduct).CurrentValues.SetValues(product);
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }
                else if (currentTable == "Suppliers")
                {
                    var suppliersFromGrid = DataGrid.ItemsSource as List<Suppliers>;

                    if (suppliersFromGrid != null)
                    {
                        foreach (var supplier in suppliersFromGrid)
                        {
                            if (supplier.id == 0)
                            {
                                context.Suppliers.Add(supplier);
                            }
                            else
                            {
                                var existingSupplier = context.Suppliers.Find(supplier.id);
                                if (existingSupplier != null)
                                {
                                    context.Entry(existingSupplier).CurrentValues.SetValues(supplier);
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }               
                else if (currentTable == "suppliedIngredients")
                {
                    var suppliedIngredientsFromGrid = DataGrid.ItemsSource as List<suppliedIngredients>;

                    if (suppliedIngredientsFromGrid != null)
                    {
                        foreach (var suppliedIngredient in suppliedIngredientsFromGrid)
                        {
                            if (suppliedIngredient.id == 0)
                            {
                                context.suppliedIngredients.Add(suppliedIngredient);
                            }
                            else
                            {
                                var existingSuppliedIngredient = context.suppliedIngredients.Find(suppliedIngredient.id);
                                if (existingSuppliedIngredient != null)
                                {
                                    context.Entry(existingSuppliedIngredient).CurrentValues.SetValues(suppliedIngredient);
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }                                
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            string inputID = TextBoxID.Text;

            if (string.IsNullOrEmpty(inputID))
            {
                MessageBox.Show("Введите ID удаляемого элемента");
                return;
            }

            int deleteID = int.Parse(inputID);

            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент с ID {deleteID}?", "Подтверждение удаления", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            using (var context = new BakeryEntities5())
            {
                dynamic itemToDelete = null;

                switch (currentTable)
                {
                    case "Products":
                        itemToDelete = context.Products.FirstOrDefault(p => p.id == deleteID);
                        break;

                    case "Ingredients":
                        itemToDelete = context.Ingredients.FirstOrDefault(i => i.id == deleteID);
                        break;

                    case "Suppliers":
                        itemToDelete = context.Suppliers.FirstOrDefault(s => s.id == deleteID);
                        break;                  

                    case "suppliedIngredients":
                        itemToDelete = context.suppliedIngredients.FirstOrDefault(si => si.id == deleteID);
                        break;

                    default:
                        MessageBox.Show("Неизвестная таблица.");
                        return;
                }

                if (itemToDelete == null)
                {
                    MessageBox.Show($"Запись с ID {deleteID} не найдена.");
                    return;
                }

                context.Set(itemToDelete.GetType()).Remove(itemToDelete);

                context.SaveChanges();

                MessageBox.Show($"Запись с ID {deleteID} была успешно удалена.");
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            switch (currentTable)
            {
                case "Products":
                    Button_Products_Click(null, null);
                    break;
                case "Ingredients":
                    Button_Ingredients_Click(null, null);
                    break;              
                case "Suppliers":
                    Button_Suppliers_Click(null, null);
                    break;              
                case "suppliedIngredients":
                    Button_suppliedIngredients_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = DataGrid.ItemsSource as IEnumerable<object>;

            string idFilter = TextBoxID.Text;
            string nameFilter = TextBoxName.Text;

            if (string.IsNullOrWhiteSpace(idFilter) && string.IsNullOrWhiteSpace(nameFilter))
            {
                MessageBox.Show("Невозможно выполнить фильтрацию: фильтр не задан.");
            }

            var filteredList = itemsSource.Where(item =>
            {
                bool matchesId = true;
                if (!string.IsNullOrWhiteSpace(idFilter) && int.TryParse(idFilter, out int id))
                {       
                    if (item is suppliedIngredients suppliedIngredient)
                    {
                        matchesId = suppliedIngredient.ingredient_id == id;
                    }     
                    else if (item is Ingredients ingredient)
                    {
                        matchesId = ingredient.id == id;
                    }
                    else if (item is Suppliers supplier)
                    {
                        matchesId = supplier.id == id;
                    }
                    else if (item is Products product)
                    {
                        matchesId = product.id == id;
                    }                    
                }

                bool matchesName = true;
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    matchesName = false;

                    // Фильтрация по имени для связей в таблицах                                       
                    if (item is suppliedIngredients suppliedIngredient)
                    {
                        bool matchesIngredientName = true;
                        if (suppliedIngredient.Ingredients != null && suppliedIngredient.Ingredients.iName != null)
                        {
                            matchesIngredientName = suppliedIngredient.Ingredients.iName.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                        }

                        bool matchesSupplierName = true;
                        if (suppliedIngredient.Suppliers != null && suppliedIngredient.Suppliers.sName != null)
                        {
                            matchesSupplierName = suppliedIngredient.Suppliers.sName.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                        }

                        matchesName = matchesIngredientName || matchesSupplierName;
                    }

                    // Фильтрация по обычным строковым свойствам
                    if (!matchesName)
                    {
                        var nameProperties = new[] { "iName", "pName", "sName" };
                        matchesName = nameProperties.Any(propName =>
                        {
                            var property = item.GetType().GetProperty(propName);
                            if (property != null)
                            {
                                var value = property.GetValue(item);
                                if (value is string stringValue)
                                {
                                    return stringValue.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                                }
                            }
                            return false;
                        });
                    }
                }

                return matchesId && matchesName;
            }).ToList();

            if (filteredList.Count == 0)
            {
                MessageBox.Show("Фильтрация не дала результатов. Попробуйте изменить фильтр");
                return;
            }

            DataGrid.ItemsSource = filteredList;
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
            var window = (MainWindow)mainWindow;
            window.MainFrame.Navigate(new ReportsPage());
        }
    }
}
