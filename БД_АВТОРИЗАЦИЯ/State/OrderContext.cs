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
    public class OrderContext
    {
        private IOrderState _currentState;

        public Orders Order { get; }

        public OrderContext(Orders order)
        {
            Order = order;


            if (string.IsNullOrEmpty(order.condition))
            {
                order.condition = "Новый"; 
            }

            IOrderState state = null;
            switch (order.condition)
            {
                case "Выполнен":
                    state = new CompletedOrderState();
                    break;
                case "Отменён":
                    state = new CanceledOrderState();
                    break;
                default:
                    state = new NewOrderState();
                    break;
            }

            SetState(state);
        }

        public void SetState(IOrderState state)
        {
            _currentState = state;
        }

        public void MarkAsCompleted()
        {
            _currentState.MarkAsCompleted(Order);
            UpdateState();
        }

        public void MarkAsCanceled()
        {
            _currentState.MarkAsCanceled(Order);
            UpdateState();
        }

        private void UpdateState()
        {
            IOrderState state = null;
            switch (Order.condition)
            {
                case "Выполнен":
                    state = new CompletedOrderState();
                    break;
                case "Отменён":
                    state = new CanceledOrderState();
                    break;
                default:
                    state = new NewOrderState();
                    break;
            }

            SetState(state);
        }
    }
}
