using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace БД_АВТОРИЗАЦИЯ.Хранитель_Memento_
{
    //Хранение состояния авторизации
    class Memento
    {
        public string Username { get; private set; }
        public string Role { get; private set; }
        public string Password { get; private set; }

        public Memento(string username, string role, string password)
        {
            Username = username;
            Role = role;
            Password = password;
        }
    }
}
