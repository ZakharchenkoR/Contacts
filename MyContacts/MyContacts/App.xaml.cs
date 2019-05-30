using MyContacts.Utils.DBSettings;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyContacts
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "ContactsDB.db";
        public static IRepository database;
        public static IRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContactAsyncRepository(DATABASE_NAME);
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
