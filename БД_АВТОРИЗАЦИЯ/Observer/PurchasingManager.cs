using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using БД_АВТОРИЗАЦИЯ.Observer;

namespace Bakery_Project.Observer
{
    public class PurchasingManager : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"Менеджер по закупкам получил уведомление: {message}");
        }
    }
}
