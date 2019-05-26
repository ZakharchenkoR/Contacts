using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MyContacts.Droid.Utils;
using MyContacts.Droid;
using Xamarin.Forms;
using SQLite;
using MyContacts.Utils.InterfacesToSQLite;

[assembly: Dependency(typeof(SQLite_Android))]
namespace MyContacts.Droid.Utils
{
    public class SQLite_Android : ISQLite
    {
        public string GetDataBasePath(string fileName)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            if (!File.Exists(path))
            {
                Context context = Android.App.Application.Context;
                var dbAssetStream = context.Assets.Open(fileName);

                var dbFileStream = new FileStream(path, FileMode.OpenOrCreate);
                var buffet = new byte[1024];
                int b = buffet.Length;
                int length;
                while ((length = dbAssetStream.Read(buffet, 0, b)) > 0)
                {
                    dbFileStream.Write(buffet, 0, length);
                }

                dbFileStream.Flush();
                dbFileStream.Close();
                dbAssetStream.Close();
            }
            return path;
        }
    }
}