using MyContacts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Utils.DBSettings
{
    public interface IRepository
    {
        Task<IEnumerable<Contact>> GetItemsAsync();
        Task<Contact> GetItemAsync(int id);
        Task DeleteItemAsync(Contact item);
        Task SaveItemAsync(Contact item);
        Task UpdateItemAsync(Contact item);
    }
}
