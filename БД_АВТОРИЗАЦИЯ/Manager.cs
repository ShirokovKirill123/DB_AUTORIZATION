using Bakery_Project.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using БД_АВТОРИЗАЦИЯ.Хранитель_Memento_;

namespace БД_АВТОРИЗАЦИЯ
{
    class Manager
    {
        public static Frame MainFrame {  get; set; }
        public static Ordinator User { get; private set; } = new Ordinator();
        public static IngredientStockNotifier IngredientObserver { get; private set; } = new IngredientStockNotifier();

    }
}
