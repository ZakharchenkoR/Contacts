using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyContacts.Utils.Singleton
{
    class SingletonToNavigation
    {
        public INavigation Navigation { get; set; }
        private SingletonToNavigation() { }
        static SingletonToNavigation instance;
        public static SingletonToNavigation GetInstance()
        {
            return instance ?? (instance = new SingletonToNavigation());
        }
    }
}
