using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using БД_АВТОРИЗАЦИЯ.Observer;

namespace Bakery_Project.Observer
{
    public class PurchasingManagerObserver : IObserver, INotifyPropertyChanged
    {
        private string _notificationMessage;

        public string NotificationMessage
        {
            get { return _notificationMessage; }
            set
            {
                if (_notificationMessage != value)
                {
                    _notificationMessage = value;
                    OnPropertyChanged(nameof(NotificationMessage)); 
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update(string message)
        {
            NotificationMessage = message;  
        }
    }
}
