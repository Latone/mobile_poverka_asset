using mobile_poverka_asset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Microsoft.Data.Sqlite;

namespace mobile_poverka_asset.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>() {};
        }
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }
        public async Task<int> GetNumberOfitems() {
            return await Task.FromResult(items.Count);
        }
        public async Task<List<Item>> GetAllitems()
        {
            return await Task.FromResult(items);
        }
        public async Task<bool> ClearAllItems()
        {
            items.Clear();
            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string serial, string  idchannel)
        {
            //var oldItem = items.Where().FirstOrDefault();
            items.RemoveAll((Item arg) => arg.Serial == serial && arg.idchannel == idchannel);

            return await Task.FromResult(true);
        }
        public async Task<List<Item>> ReturnAllItemsThatAreNotAddedAsync() {
            //var list = items.Select((Item arg) => { if (arg.inDataBase == false) { arg.inDataBase = true; return arg; } }).ToList<Item>();
            
            //Bruh ->
            //Return all items from local list that are not currently added to DB
            var list = items.Where((Item arg) => arg.inDataBase == false).Select((Item arg) => { arg.inDataBase = true; return arg; }).ToList();
            return await Task.FromResult(list);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}