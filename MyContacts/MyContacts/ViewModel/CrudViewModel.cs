using MyContacts.Model;
using MyContacts.Utils.Singleton;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MyContacts.View;
using Plugin.Media;
using Plugin.Media.Abstractions;
using MyContacts.Utils.SMSSettings;

namespace MyContacts.ViewModel
{
    class CrudViewModel:INotifyPropertyChanged
    {
        SingletonToNavigation singletonNavigation;
        SingletonToContact contact;
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
        public bool IsEnabledAdd
        {
            get { return isEnabled; }
            set { isEnabled = value; Notify(); }
        }

        private bool isEnabledOther = false;
        public bool IsEnabledOther
        {
            get { return isEnabledOther; }
            set { isEnabledOther = value; Notify(); }
        }


        private string uri;

        public string Uri
        {
            get { return uri; }
            set { uri = value; Notify(); }
        }



        public CrudViewModel()
        {
            singletonNavigation = SingletonToNavigation.GetInstance();
            Navigation = singletonNavigation.Navigation;
            contact = SingletonToContact.GetInstance();
            if(contact.Update)
            {
                this.Name = contact.Contact.Name;
                this.LastName = contact.Contact.LastName;
                this.NumberPhone = contact.Contact.NumberPhone;
                this.E_Mail = contact.Contact.E_Mail;
                this.Uri = contact.Contact.Uri;
                this.IsEnabledAdd = false;
                this.IsEnabledOther = true;
            }
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            AddCommand = new Command(Add);
            DeleteCommand = new Command(DeleteContact);
            UpdateCommand = new Command(UpdateContact);
            AddPhoto = new Command(GetPhoto);
            SentSMSCommand = new Command(SentSMS);
        }

        public ICommand AddCommand { get; private set; }
        private async void Add()
        {
            Contact temp = new Contact { Name = this.Name, LastName = this.LastName, E_Mail = this.E_Mail, NumberPhone = this.NumberPhone ,Uri = this.Uri};
            await App.Database.SaveItemAsync(temp);
            await Navigation.PopModalAsync();
        }

        public ICommand DeleteCommand { get; set; }
        private async void DeleteContact()
        {
            await App.Database.DeleteItemAsync(contact.Contact);
            contact.Contact = null;
            contact.Update = false;
            await Navigation.PopModalAsync();
        }

        public ICommand UpdateCommand { get; private set; }
        private async void UpdateContact()
        {
            contact.Contact.E_Mail = this.E_Mail;
            contact.Contact.Name = this.Name;
            contact.Contact.LastName = this.LastName;
            contact.Contact.NumberPhone = this.NumberPhone;
            await App.Database.UpdateItemAsync(contact.Contact);
            contact.Contact = null;
            contact.Update = false;
            await Navigation.PopModalAsync();
        }

        public ICommand SentSMSCommand { get; private set; }
        private  void SentSMS()
        {
            Navigation.PushModalAsync(new SMSView());
        }

        public ICommand AddPhoto { get; private set; }
        private async void GetPhoto()
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    Uri = photo.Path;
                }
            }
            catch(Exception)
            {

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
