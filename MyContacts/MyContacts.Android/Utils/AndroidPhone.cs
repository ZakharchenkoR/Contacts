using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyContacts.Droid.Utils;
using MyContacts.Utils.CallSetings;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidPhone))]
namespace MyContacts.Droid.Utils
{
    public class AndroidPhone : IPhoneCall
    {
        [Obsolete]
        public void Call(string phoneNumber)
        {
            try
            {
                var uri = Android.Net.Uri.Parse($"tel:{phoneNumber}");
                var intent = new Intent(Intent.ActionCall, uri);
                Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("Ok", (sender, args) => 
                {
                })
                .SetMessage(ex.ToString())
                .SetTitle("Android Exeption").Show();
            }
        }
    }
}