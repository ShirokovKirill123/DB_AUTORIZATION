using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using БД_АВТОРИЗАЦИЯ.Observer;

namespace Bakery_Project.Observer
{
    public interface IObservable
    {
        void AddObserver(IObserver observer); 
        void RemoveObserver(IObserver observer); 
        void NotifyObservers(string message); 
    }
}
