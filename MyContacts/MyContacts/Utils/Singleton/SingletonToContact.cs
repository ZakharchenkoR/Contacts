using MyContacts.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyContacts.Utils.Singleton
{
    class SingletonToContact
    {
        public Contact Contact { get; set; }
        public bool Update { get; set; }
        private SingletonToContact() { }
        static SingletonToContact instance;
        public static SingletonToContact GetInstance()
        {
            return instance ?? (instance = new SingletonToContact());
        }
    }
}
