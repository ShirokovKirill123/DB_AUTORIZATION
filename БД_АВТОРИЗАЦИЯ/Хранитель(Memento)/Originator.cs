using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace БД_АВТОРИЗАЦИЯ.Хранитель_Memento_
{
    // Управление состоянием авторизации
    class Originator
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; private set; }

        public Memento CreateMemento()
        {
            return new Memento(Username, Role,Password);
        }

        public void SetMemento(Memento memento)
        {
            Username = memento.Username;
            Role = memento.Role;
            Password = memento.Password;
        }
    }
}
