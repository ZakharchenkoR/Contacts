using MyContacts.Model;
using MyContacts.Utils.Singleton;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyContacts.ViewModel
{
    class CrudViewModel:INotifyPropertyChanged
    {
        SingletonToNavigation singletonNavigation = SingletonToNavigation.GetInstance();
        SingletonToContact ToContact;
        INavigation Navigation { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; Notify(); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; Notify(); }
        }

        private string e_mail;

        public string E_Mail
        {
            get { return e_mail; }
            set { e_mail = value; Notify(); }
        }


        private int numberPhone;

        public int NumberPhone
        {
            get { return numberPhone; }
            set { numberPhone = value; Notify(); }
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; Notify(); }
        }


        public CrudViewModel()
        {
            Navigation = singletonNavigation.Navigation;
            ToContact = SingletonToContact.GetInstance();
            if(ToContact.Update)
            {
                this.Name = ToContact.Contact.Name;
                this.LastName = ToContact.Contact.LastName;
                this.NumberPhone = ToContact.Contact.NumberPhone;
                this.E_Mail = ToContact.Contact.E_Mail;
                this.IsEnabled = false;
            }
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            AddCommand = new Command(Add);
            DeleteCommand = new Command(DeleteContact);
            UpdateCommand = new Command(UpdateContact);
            AddPhoto = new Command(GetPhoto);
        }

        public ICommand AddCommand { get; private set; }
        private async void Add()
        {
            Contact temp = new Contact { Name = this.Name, LastName = this.LastName, E_Mail = this.E_Mail, NumberPhone = this.NumberPhone };
            await App.Database.SaveItemAsync(temp);
            await Navigation.PopModalAsync();
        }

        public ICommand DeleteCommand { get; set; }
        private async void DeleteContact()
        {
            await App.Database.DeleteItemAsync(ToContact.Contact);
            ToContact.Contact = null;
            ToContact.Update = false;
            await Navigation.PopModalAsync();
        }

        public ICommand UpdateCommand { get; private set; }
        private async void UpdateContact()
        {
            await App.Database.UpdateItemAsync(ToContact.Contact);
            ToContact.Contact = null;
            ToContact.Update = false;
            await Navigation.PopModalAsync();
        }

        public ICommand AddPhoto { get; private set; }
        private async void GetPhoto()
        {
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
