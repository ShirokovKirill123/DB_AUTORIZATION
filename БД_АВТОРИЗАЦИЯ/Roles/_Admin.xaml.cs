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
using Bakery_Project;
using Bakery_Project.State;
using Bakery_Project.Observer;

namespace БД_АВТОРИЗАЦИЯ
{
    /// <summary>
    /// Логика взаимодействия для _Admin.xaml
    /// </summary>
    public partial class _Admin : Page
    {
        private string currentTable;
        private PurchasingManagerObserver _purchasingManagerObserver;

        public _Admin()
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
                DataGrid.ContextMenu = null;

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

        private void Button_ProductComposition_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ContextMenu = null;

            using (var context = new BakeryEntities5())
            {
                var productCompositionList = context.ProductСomposition
                    .Include(pc => pc.Ingredients)
                    .Include(pc => pc.Products)
                    .ToList();

                DataGrid.ItemsSource = productCompositionList;
                currentTable = "ProductComposition";

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID продукта", Binding = new Binding("product_id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID ингредиента", Binding = new Binding("ingredient_id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Продукты", Binding = new Binding("Products.pName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Ингредиенты", Binding = new Binding("Ingredients.iName") });

                DataGrid.Columns[0].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
                DataGrid.Columns[1].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
                DataGrid.Columns[2].Width = new DataGridLength(0.8, DataGridLengthUnitType.Star);
                DataGrid.Columns[3].Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                DataGrid.Columns[4].Width = new DataGridLength(2, DataGridLengthUnitType.Star);

                StackPanelVisibility();
            }           
        }

        private void Button_suppliedIngredients_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ContextMenu = null;

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
            DataGrid.ContextMenu = null;

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
            DataGrid.ContextMenu = null;

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

        private void Button_OrderedProducts_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ContextMenu = null;

            using (var context = new BakeryEntities5())
            {
                var orderedProductsList = context.OrderedProducts
                    .Include(op => op.Products)
                    .ToList();

                foreach (var orderedProduct in orderedProductsList)
                {
                    decimal unitPrice = orderedProduct.Products?.pricePerUnit ?? 0;
                    orderedProduct.price = unitPrice * orderedProduct.quantityOfProducts;
                }

                DataGrid.ItemsSource = orderedProductsList;
                currentTable = "OrderedProducts";

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID заказа", Binding = new Binding("order_id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID продукта", Binding = new Binding("product_id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Продукты", Binding = new Binding("Products.pName") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество продукта", Binding = new Binding("quantityOfProducts") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена(₽)", Binding = new Binding("price") });

                StackPanelVisibility();
            }
        }

        //реализация паттерна State
        private void Button_Orders_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new BakeryEntities5())
            {
                var ordersList = context.Orders.ToList();

                foreach (var order in ordersList)
                {
                    var orderContext = new OrderContext(order);                  
                }

                DataGrid.ItemsSource = ordersList;
                currentTable = "Orders";

                DataGrid.ContextMenu = null;
                var contextMenu = new ContextMenu();

                var menuItemCompleted = new MenuItem { Header = "Отметить как выполненный" };
                menuItemCompleted.Click += MarkOrderAsCompleted;
                var menuItemCanceled = new MenuItem { Header = "Отменить заказ" };
                menuItemCanceled.Click += MarkOrderAsCanceled;

                contextMenu.Items.Add(menuItemCompleted);
                contextMenu.Items.Add(menuItemCanceled);

                DataGrid.ContextMenu = contextMenu;

                DataGrid.Columns.Clear();
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата заказа", Binding = new Binding("orderDate") });
                DataGrid.Columns.Add(new DataGridTextColumn { Header = "Состояние", Binding = new Binding("condition") });

                StackPanelVisibility();
            }
        }

        private void MarkOrderAsCompleted(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Orders)DataGrid.SelectedItem;
            var orderContext = new OrderContext(selectedOrder);

            orderContext.MarkAsCompleted();
            using (var context = new BakeryEntities5())
            {
                context.Entry(selectedOrder).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            Button_Orders_Click(sender, e);
        }

        private void MarkOrderAsCanceled(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Orders)DataGrid.SelectedItem;
            var orderContext = new OrderContext(selectedOrder);

            orderContext.MarkAsCanceled();

            using (var context = new BakeryEntities5())
            {
                context.Entry(selectedOrder).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            Button_Orders_Click(sender, e);
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

                    case "Orders":
                        var newOrder = new Orders
                        {
                            orderDate = DateTime.Now, 
                            condition = "Новый"
                        };
                        context.Orders.Add(newOrder);
                        break;

                    case "OrderedProducts":
                        var newOrderedProduct = new OrderedProducts
                        {
                            order_id = 1, 
                            product_id = 1, 
                            quantityOfProducts = 0,  
                            price = 0m 
                        };
                        context.OrderedProducts.Add(newOrderedProduct);
                        break;

                    case "ProductComposition":
                        var newProductComposition = new ProductСomposition
                        {
                            product_id = 1, 
                            ingredient_id = 1 
                        };
                        context.ProductСomposition.Add(newProductComposition);
                        context.SaveChanges();
                        RefreshDataGrid(); 
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
                else if (currentTable == "ProductComposition")
                {
                    var productCompositionFromGrid = DataGrid.ItemsSource as List<ProductСomposition>;

                    if (productCompositionFromGrid != null)
                    {
                        foreach (var productComposition in productCompositionFromGrid)
                        {
                            if (productComposition.id == 0)   
                            {
                                context.ProductСomposition.Add(productComposition);
                            }
                            else 
                            {
                                var existingProductComposition = context.ProductСomposition.Find(productComposition.id);
                                if (existingProductComposition != null)
                                {
                                    context.Entry(existingProductComposition).CurrentValues.SetValues(productComposition);
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
                else if (currentTable == "OrderedProducts")
                {
                    var orderedProductsFromGrid = DataGrid.ItemsSource as List<OrderedProducts>;

                    if (orderedProductsFromGrid != null)
                    {
                        foreach (var orderedProduct in orderedProductsFromGrid)
                        {
                            if (orderedProduct.id == 0) 
                            {
                                context.OrderedProducts.Add(orderedProduct);
                            }
                            else 
                            {
                                var existingOrderedProduct = context.OrderedProducts.Find(orderedProduct.id);
                                if (existingOrderedProduct != null)
                                {
                                    context.Entry(existingOrderedProduct).CurrentValues.SetValues(orderedProduct);
                                }
                            }
                        }

                        context.SaveChanges();
                    }
                }
                else if (currentTable == "Orders")
                {
                    var ordersFromGrid = DataGrid.ItemsSource as List<Orders>;

                    if (ordersFromGrid != null)
                    {
                        foreach (var order in ordersFromGrid)
                        {
                            if (order.id == 0) 
                            {
                                context.Orders.Add(order);
                            }
                            else 
                            {
                                var existingOrder = context.Orders.Find(order.id);
                                if (existingOrder != null)
                                {
                                    context.Entry(existingOrder).CurrentValues.SetValues(order);
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

                    case "Orders":
                        itemToDelete = context.Orders.FirstOrDefault(o => o.id == deleteID);
                        break;

                    case "OrderedProducts":
                        itemToDelete = context.OrderedProducts.FirstOrDefault(op => op.id == deleteID);
                        break;

                    case "ProductComposition":
                        itemToDelete = context.ProductСomposition.FirstOrDefault(pc => pc.id == deleteID);
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
                case "Orders":
                    Button_Orders_Click(null, null);
                    break;
                case "Suppliers":
                    Button_Suppliers_Click(null, null);
                    break;
                case "OrderedProducts":
                    Button_OrderedProducts_Click(null, null);
                    break;
                case "ProductComposition":
                    Button_ProductComposition_Click(null, null);
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
                    if (item is ProductСomposition productComposition)
                    {
                        matchesId = productComposition.product_id == id; 
                    }
                    else if (item is suppliedIngredients suppliedIngredient)
                    {
                        matchesId = suppliedIngredient.ingredient_id == id; 
                    }
                    else if (item is OrderedProducts orderedProduct)
                    {
                        matchesId = orderedProduct.order_id == id; 
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
                    else if (item is Orders order)
                    {
                        matchesId = order.id == id;
                    }
                }

                bool matchesName = true;
                if (!string.IsNullOrWhiteSpace(nameFilter))
                {
                    matchesName = false;

                    // Фильтрация по имени для связей в таблицах
                    if (item is ProductСomposition productComposition)
                    {
                        bool matchesProductName = true;
                        if (productComposition.Products != null && productComposition.Products.pName != null)
                        {
                            matchesProductName = productComposition.Products.pName.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                        }

                        bool matchesIngredientName = true;
                        if (productComposition.Ingredients != null && productComposition.Ingredients.iName != null)
                        {
                            matchesIngredientName = productComposition.Ingredients.iName.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                        }

                        matchesName = matchesProductName || matchesIngredientName;
                    }
                    else if (item is OrderedProducts orderedProduct)
                    {
                        if (orderedProduct.Products != null && orderedProduct.Products.pName != null)
                        {
                            matchesName = orderedProduct.Products.pName.IndexOf(nameFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                    }
                    else if (item is suppliedIngredients suppliedIngredient)
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

        private void TBNotification_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TBNotification.Text == "")
            {
                border.Visibility = Visibility.Hidden;
                TBNotification.Visibility = Visibility.Hidden;
            }
            else
            {
                border.Visibility = Visibility.Visible;
                TBNotification.Visibility = Visibility.Visible;
            }
        }
    }
}
