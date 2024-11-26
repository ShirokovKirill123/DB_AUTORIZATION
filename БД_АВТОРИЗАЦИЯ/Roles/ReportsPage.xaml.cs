using Bakery_Project;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Data.Entity;   // Для Entity Framework 6


namespace БД_АВТОРИЗАЦИЯ
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
        }

        private void LoadReportsForRole(string userRole)
        {
            ReportsComboBox.Items.Clear();

            if (userRole == "Пекарь")
            {
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о произведённой продукции" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о нехватке ингредиентов" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о заказанных продуктах" });
            }
            else if (userRole == "Менеджер по закупкам")
            {
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о необходимых закупках" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о поставках по ингредиентам" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о поступлении и расходе ингредиентов" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о состоянии запасов" });
            }
            else if (userRole == "Администратор")
            {
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о произведённой продукции" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о нехватке ингредиентов" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о заказанных продуктах" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о необходимых закупках" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о поставках по ингредиентам" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о поступлении и расходе ингредиентов" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёт о состоянии запасов" });
                ReportsComboBox.Items.Add(new ComboBoxItem { Content = "Отчёты по общей прибыли" });
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var data = ReportsDataGrid.ItemsSource as IEnumerable<object>;
            if (data == null || !data.Any())
            {
                MessageBox.Show("Нет данных для экспорта");
                return;
            }

            // Реализация экспорта в CSV
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv",
                FileName = "Отчёт.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var properties = data.First().GetType().GetProperties();
                    writer.WriteLine(string.Join(";", properties.Select(p => p.Name)));

                    foreach (var item in data)
                    {
                        var values = properties.Select(p => p.GetValue(item)?.ToString());
                        writer.WriteLine(string.Join(";", values));
                    }
                }

                MessageBox.Show("Отчёт успешно экспортирован");
            }
        }

        private void ReportsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedReport = (ReportsComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(selectedReport))
                return;

            using (var context = new BakeryEntities4())
            {
                switch (selectedReport)
                {
                    case "Отчёт о произведённой продукции":
                        DisplayProducedProductsReport(context);
                        break;
                    case "Отчёт о нехватке ингредиентов":
                        DisplayInsufficientIngredientsReport(context);
                        break;
                    case "Отчёт о заказанных продуктах":
                        DisplayOrderedProductsReport(context);
                        break;
                    case "Отчёт о необходимых закупках":
                        DisplayNecessaryPurchasesReport(context);
                        break;
                    case "Отчёт о поставках по ингредиентам":
                        DisplaySuppliesReport(context);
                        break;
                    case "Отчёт о поступлении и расходе ингредиентов":
                        DisplayIngredientUsageReport(context);
                        break;
                    case "Отчёт о состоянии запасов":
                        DisplayInventoryStateReport(context);
                        break;                  
                    case "Отчёт о прибыли":
                        DisplayProfitReport(context);
                        break;
                    default:
                        MessageBox.Show("Неизвестный отчёт");
                        break;
                }
            }
        }

        private void DisplayProducedProductsReport(BakeryEntities4 context)
        {
            var reportData = context.Products
                .Select(p => new
                {
                    p.id,
                    Название = p.pName,
                    Тип = p.ptype,
                    Цена = p.pricePerUnit,
                    Объем = p.outputVolume,
                    Срок_Хранения = p.shelflife
                })
                .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название продукта", Binding = new Binding("Название") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Тип продукта", Binding = new Binding("Тип") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена за единицу", Binding = new Binding("Цена") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Объем выпуска", Binding = new Binding("Объем") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Срок хранения", Binding = new Binding("Срок_Хранения") });
        }

        private void DisplayInsufficientIngredientsReport(BakeryEntities4 context)
        {
            const int minimumRequiredQuantity = 20;

            var reportData = context.Ingredients
             .Where(i => i.availableQuantity < minimumRequiredQuantity)
             .Select(i => new
             {
                 i.id,
                 Название = i.iName,
                 Единица = i.unitOfMeasurement,
                 Доступно = i.availableQuantity ?? 0,
                 Требуется = minimumRequiredQuantity
             })
             .ToList()
             .Select(i => new
             {
                 i.id,
                 i.Название,
                 i.Единица,
                 i.Доступно,
                 i.Требуется,
                 Необходимо = Math.Max(i.Требуется - i.Доступно, 0)
             })
             .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название ингредиента", Binding = new Binding("Название") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Единица измерения", Binding = new Binding("Единица") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Доступное количество", Binding = new Binding("Доступно") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Требуемое количество", Binding = new Binding("Требуется") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Недостает", Binding = new Binding("Необходимо") });
        }

        private void DisplayOrderedProductsReport(BakeryEntities4 context)
        {
            var reportData = context.OrderedProducts
            .Include(op => op.Products)
            .Include(op => op.Orders) 
            .Select(op => new
            {
                Order_ID = op.order_id, 
                Product_ID = op.product_id, 
                Статус_заказа = op.Orders.condition, 
                Дата_заказа = op.Orders.orderDate, 
                Название_продукта = op.Products.pName, 
                Количество = op.quantityOfProducts, 
                Цена = op.price
            })
            .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Order ID", Binding = new Binding("Order_ID") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Product ID", Binding = new Binding("Product_ID") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Статус заказа", Binding = new Binding("Статус_заказа") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата заказа", Binding = new Binding("Дата_заказа") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название продукта", Binding = new Binding("Название_продукта") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество", Binding = new Binding("Количество") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена", Binding = new Binding("Цена") }); 
        }


        private void DisplayNecessaryPurchasesReport(BakeryEntities4 context)
        {
            var reportData = context.Ingredients
                .Where(i => (i.availableQuantity ?? 0) < 20) 
                .Select(i => new
                {
                    i.id,
                    Название = i.iName,
                    Требуемое_количество = 20 - (i.availableQuantity ?? 0),
                    Поставщик = context.suppliedIngredients
                                .Where(si => si.ingredient_id == i.id)
                                .Select(si => si.Suppliers.sName)
                                .FirstOrDefault() ?? "Неизвестно"
                })
                .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("Название") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Требуемое количество", Binding = new Binding("Требуемое_количество") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Поставщик", Binding = new Binding("Поставщик") });
        }

        private void DisplaySuppliesReport(BakeryEntities4 context)
        {
            var reportData = context.suppliedIngredients
           .Include(si => si.Suppliers)
           .Include(si => si.Ingredients)
           .Select(si => new
           {
               Поставщик = si.Suppliers.sName,
               Ингредиент = si.Ingredients.iName,
               Количество = si.ingredient_quntity,
               Цена = si.price
           })
           .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Поставщик", Binding = new Binding("Поставщик") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Ингредиент", Binding = new Binding("Ингредиент") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Количество", Binding = new Binding("Количество") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена", Binding = new Binding("Цена") });
        }

        private void DisplayIngredientUsageReport(BakeryEntities4 context)
        {
            var reportData = context.Ingredients
            .Select(i => new
            {
                i.id,
                Название = i.iName,
                Приход = context.suppliedIngredients
                              .Where(si => si.ingredient_id == i.id)
                              .Sum(si => (decimal?)si.ingredient_quntity) ?? 0,
                Расход = context.ProductСomposition
                            .Where(pc => pc.ingredient_id == i.id)
                            .Select(pc => pc.product_id)   
                            .Distinct()                    
                            .Count()                        
            })
            .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название ингредиента", Binding = new Binding("Название") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Поступление", Binding = new Binding("Приход") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Расход", Binding = new Binding("Расход") });
        }

        private void DisplayInventoryStateReport(BakeryEntities4 context)
        {
            var reportData = context.Ingredients
            .Select(i => new
            {
                i.id,
                Название = i.iName,
                Доступно = i.availableQuantity,
                Цена = i.pricePerUnit,
                Стоимость_запасов = i.availableQuantity * i.pricePerUnit
            })
            .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("id") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Название ингредиента", Binding = new Binding("Название") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Доступное количество", Binding = new Binding("Доступно") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Цена за единицу", Binding = new Binding("Цена") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Стоимость запасов", Binding = new Binding("Стоимость_запасов") });
        }        

        private void DisplayProfitReport(BakeryEntities4 context)
        {
            var reportData = context.Orders
           .Where(o => o.condition == "Выполнен")  
           .Select(o => new
           {
                o.id,
                Общая_цена = o.OrderedProducts.Sum(op => op.price),  
                Дата_заказа = o.orderDate
           })
           .ToList();

            ReportsDataGrid.ItemsSource = reportData;
            ReportsDataGrid.Columns.Clear();

            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID заказа", Binding = new Binding("id") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Общая цена", Binding = new Binding("Общая_цена") });
            ReportsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Дата заказа", Binding = new Binding("Дата_заказа") });
        }    
    }
}

