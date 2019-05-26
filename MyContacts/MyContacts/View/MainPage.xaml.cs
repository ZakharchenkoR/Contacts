using MyContacts.Utils.Singleton;
using MyContacts.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyContacts
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        SingletonToNavigation singleton = SingletonToNavigation.GetInstance();
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            singleton.Navigation = this.Navigation;
            this.BindingContext = new MainPageViewModel { Navigation = this.Navigation };

        }

        protected override async void OnAppearing()
        {
            await App.Database.CreateTableAsync();
            list.ItemsSource = await App.Database.GetItemsAsync();
            base.OnAppearing();
        }
    }
}
