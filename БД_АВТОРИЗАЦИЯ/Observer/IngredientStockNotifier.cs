using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using БД_АВТОРИЗАЦИЯ.Observer;

namespace Bakery_Project.Observer
{
    public class IngredientStockNotifier : IObservable
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        public void NotifyInsufficientStock(string notification)
        {
            string message = $"Предупреждение: {notification}";
            NotifyObservers(message);  
        }
    }
}
