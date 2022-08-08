using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.Views;
using System.Threading.Tasks;

namespace mobile_poverka_asset.ViewModels
{
    public class DB_Search_lvl2ViewModel : INotifyPropertyChanged
    {
        public string item_id;

        public event PropertyChangedEventHandler PropertyChanged;
        public Command<Item> ItemTappedlvl2 { get; }
        public ICommand PerformSearchItem => new Command<string>((string query) =>
        {
            SearchResultsItem = SearchDevice.GetSearchResultsPribor(query, SearchDevice.spisok_id);
        });
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<Item> searchResultsItem;
        public List<Item> SearchResultsItem
        {
            get
            {
                return searchResultsItem;
            }
            set
            {
                searchResultsItem = value;
                NotifyPropertyChanged();
            }
        }
        public DB_Search_lvl2ViewModel() {
            ItemTappedlvl2 = new Command<Item>(OnItemSelected);
        }
        /*async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the db_search_lvl2_priborDetailed onto the
            //
            //
            // stack
            await Shell.Current.GoToAsync($"{nameof(DB_Search_lvl2_priborDetailed)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }*/
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            List<Item> h = new List<Item>();
            h.Add(item);
            SearchDevice.currentItem = h;

            item_id = item.Id;
            SearchDevice.item_id = item.Id;

            await Shell.Current.GoToAsync($"{nameof(DB_Search_lvl2_priborDetailed)}");
        }
    }
}
