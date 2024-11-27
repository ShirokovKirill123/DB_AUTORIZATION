using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using БД_АВТОРИЗАЦИЯ.State;
using Bakery_Project;
using БД_АВТОРИЗАЦИЯ;

namespace Bakery_Project.State
{
    public class NewOrderState : IOrderState
    {
        public void MarkAsNew(Orders order)
        {
            order.condition = "Новый";
        }

        public void MarkAsCompleted(Orders order)
        {
            order.condition = "Выполнен";
        }

        public void MarkAsCanceled(Orders order)
        {
            order.condition = "Отменён";
        }
    }

    public class CompletedOrderState : IOrderState
    {
        public void MarkAsNew(Orders order)
        {
            Console.WriteLine($"Заказ {order.id} выполнен. Добавление невозможно");
        }

        public void MarkAsCompleted(Orders order)
        {
            Console.WriteLine($"Заказ {order.id} уже выполнен.");
        }

        public void MarkAsCanceled(Orders order)
        {
            Console.WriteLine($"Заказ {order.id} выполнен. Отмена невозможна");
        }
    }

    public class CanceledOrderState : IOrderState
    {
        public void MarkAsNew(Orders order)
        {
            Console.WriteLine($"Заказ {order.id} отменён. Добавление невозможно");
        }

        public void MarkAsCompleted(Orders order)
        {
            Console.WriteLine($"Заказ {order.id} отменён");
        }

        public void MarkAsCanceled(Orders order)
        {
            Console.WriteLine($"Заказ {order.id} уже отменён");
        }
    }
}
