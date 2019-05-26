using MyContacts.Utils.Singleton;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyContacts.ViewModel
{
    class SMSViewModel : INotifyPropertyChanged
    {
        SingletonToNavigation singleton = SingletonToNavigation.GetInstance();
        SingletonToContact singletonContact = SingletonToContact.GetInstance();
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



        public SMSViewModel()
        {
            navigation = singleton.Navigation;
            this.Number = singletonContact.Contact.NumberPhone;
            this.Name = singletonContact.Contact.Name;
            SentSMSCommand = new Command(SentSMS);
        }

        public ICommand SentSMSCommand { get; private set; }
        private void SentSMS()
        {
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
                smsMessenger.SendSms($"+380{Number}", $"{TextSMS}");
            navigation.PopModalAsync();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
