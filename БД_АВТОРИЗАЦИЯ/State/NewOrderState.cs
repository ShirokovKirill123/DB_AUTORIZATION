using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using БД_АВТОРИЗАЦИЯ.State;
using Bakery_Project;
using БД_АВТОРИЗАЦИЯ;
using System.Windows;

namespace Bakery_Project.State
{
    public class NewOrderState : IOrderState
    {

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
        public void MarkAsCompleted(Orders order)
        {
            MessageBox.Show($"Заказ {order.id} уже выполнен");
        }

        public void MarkAsCanceled(Orders order)
        {
            MessageBox.Show($"Заказ {order.id} выполнен. Отмена невозможна");
        }
    }

    public class CanceledOrderState : IOrderState
    {
        public void MarkAsCompleted(Orders order)
        {
            MessageBox.Show($"Заказ {order.id} отменён. Невозможно выполнить");
        }

        public void MarkAsCanceled(Orders order)
        {
            MessageBox.Show($"Заказ {order.id} уже отменён");
        }
    }
}
