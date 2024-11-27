using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bakery_Project;
using БД_АВТОРИЗАЦИЯ;

namespace БД_АВТОРИЗАЦИЯ.State
{
    public interface IOrderState
    {
        void MarkAsNew(Orders order);
        void MarkAsCompleted(Orders order);
        void MarkAsCanceled(Orders order);
    }
}
