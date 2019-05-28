using MyContacts.Model;
using MyContacts.Utils.CallSetings;
using MyContacts.Utils.Singleton;
using MyContacts.View;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyContacts.ViewModel
{
    class MainPageViewModel:INotifyPropertyChanged
    {
        SingletonToNavigation singleton;
        public INavigation Navigation;

        private bool callEnabled = false;      
        public bool CallEnabled
        {
            get { return callEnabled; }
            set
            {
                callEnabled = value;
                Notify();
            }
        }

     
        private Contact contact;
        public Contact SelectedContact
        {
            get { return contact; }
            set
            {
                contact = value;
                CallEnabled = true;
                Notify();
            }
        }


        public  MainPageViewModel()
        {
            singleton = SingletonToNavigation.GetInstance();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            AddContact = new Command(CreateContact);
            UpdateCommand = new Command(UpdateContact);
            CallCommand = new Command(Call);
        }

        public ICommand UpdateCommand { get; private set; }
        private void UpdateContact()
        {
            SingletonToContact toContact = SingletonToContact.GetInstance();
            toContact.Contact = SelectedContact;
            toContact.Update = true;
            Navigation.PushModalAsync(new CrudView());
            CallEnabled = false;
        }
        public ICommand AddContact { get; private set; }
        private  void CreateContact()
        {
            Navigation.PushModalAsync(new CrudView());
        }

        public ICommand CallCommand { get; private set; }
        private void Call()
        {
            if (SelectedContact == null)
                return;
            DependencyService.Get<IPhoneCall>().Call($"+380{SelectedContact.NumberPhone.ToString()}");
            CallEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
