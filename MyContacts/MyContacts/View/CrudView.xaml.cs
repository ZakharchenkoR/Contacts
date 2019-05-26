using MyContacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyContacts.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrudView : ContentPage
    {
        public CrudView()
        {
            InitializeComponent();
            this.BindingContext = new CrudViewModel();
        }
    }
}