using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyContacts.Model;
using MyContacts.Utils.InterfacesToSQLite;
using SQLite;
using Xamarin.Forms;

namespace MyContacts.Utils.DBSettings
{
    public class ContactAsyncRepository : IRepository
    {
        SQLiteAsyncConnection database;
        public ContactAsyncRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDataBasePath(filename);
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<Contact>().Wait();
        }

        public async Task CreateTableAsync()
        {
            await database.CreateTableAsync<Contact>();
        }

        public async Task DeleteItemAsync(Contact item)
        {
            await database.DeleteAsync(item);
        }

        public async Task<Contact> GetItemAsync(int id)
        {
            return await database.GetAsync<Contact>(id);
        }

        public async Task<IEnumerable<Contact>> GetItemsAsync()
        {
            return await database.Table<Contact>().ToListAsync();
        }

        public async Task SaveItemAsync(Contact item)
        {          
                await database.InsertAsync(item);           
        }

        public async Task UpdateItemAsync( Contact item)
        {
            //var temp = GetItemAsync(item.Id);
            await DeleteItemAsync(item);
            await SaveItemAsync(item);
        }
    }
}
