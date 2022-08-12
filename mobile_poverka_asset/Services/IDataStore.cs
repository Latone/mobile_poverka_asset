using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mobile_poverka_asset.Models;

namespace mobile_poverka_asset.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string serial, string idchannel);
        Task<T> GetItemAsync(string id);
        Task<int> GetNumberOfitems();
        Task<List<T>> GetAllitems();
        Task<bool> ClearAllItems();
        Task<List<T>> ReturnAllItemsThatAreNotAddedAsync();
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
