using MyContacts.Utils.Singleton;
using MyContacts.Utils.SMSSettings;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyContacts.ViewModel
{
    class SMSViewModel : INotifyPropertyChanged
    {
        SMSSent sent;
        SingletonToNavigation singleton;
        SingletonToContact singletonContact;
        private INavigation navigation;

        private string textSMS;

        public string TextSMS
        {
            get { return textSMS; }
            set { textSMS = value;Notify(); }
        }

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; Notify(); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; Notify(); }
        }

        private string uri;

        public string Uri
        {
            get { return uri; }
            set { uri = value; Notify(); }
        }


        public SMSViewModel()
        {
            singleton = SingletonToNavigation.GetInstance();
            singletonContact = SingletonToContact.GetInstance();
            sent = new SMSSent();
            navigation = singleton.Navigation;
            this.Number = singletonContact.Contact.NumberPhone;
            this.Name = singletonContact.Contact.Name;
            this.Uri = singletonContact.Contact.Uri;
            SentSMSCommand = new Command(SentSMS);
        }

        public ICommand SentSMSCommand { get; private set; }
        private async void SentSMS()
        {
            await sent.SendSms(TextSMS, $"+380{Number}");
            await GoBack();
        }

        private async Task GoBack()
        {
            await navigation.PopModalAsync();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
