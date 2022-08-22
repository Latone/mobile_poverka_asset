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
    public class DB_Search_lvl2ViewModel : BaseViewModel
    {
        public string item_id;

        public Command<Item> ItemTapped { get; }
        public Command DeletePool { get; }
        public Command sendXML { get; }
    public DB_Search_lvl2ViewModel()
        {
            ItemTapped = new Command<Item>(OnItemSelected);
            DeletePool = new Command(DeletePoolButton_Clicked);
            sendXML = new Command(SENDxmlButton_Clicked);
            SearchResultsItem = SearchDevice.GetSearchResultsPribor("", SearchDevice.spisok_id);
        }
        public ICommand PerformSearchItem => new Command<string>((string query) =>
        {
            SearchResultsItem = SearchDevice.GetSearchResultsPribor(query, SearchDevice.spisok_id);
        });
            async void SENDxmlButton_Clicked()
        {
            SearchResultsItem = SearchDevice.GetSearchResultsPribor("", SearchDevice.spisok_id);
            Models.Spisok spisok = new Models.Spisok()
            {
                Id = CurrentSpisok[0].Id,
                Name = CurrentSpisok[0].Name,
                Date = CurrentSpisok[0].Date.Substring(0,CurrentSpisok[0].Date.IndexOf(' ')),
                Count = CurrentSpisok[0].Count,
                Complete = CurrentSpisok[0].Complete,
                Comment = CurrentSpisok[0].Comment,
            };
            XML.XML.Assign(SearchResultsItem, spisok);

            await Shell.Current.GoToAsync(nameof(xmlConstructPage));
        }
        async void DeletePoolButton_Clicked()
        {
            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert($"Вы точно хотите удалить \n\"{CurrentSpisok[0].Name}\"?",
                $"ID: [{CurrentSpisok[0].Id}]", "Удалить", "Отмена");
                if (answer == true)
                {
                    await ModifyDelete.deletePool();
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }

        }
        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {

                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private List<Spisok> currentSpisok;
        public List<Spisok> CurrentSpisok
        {
            get
            {
                return SearchDevice.currentSpisok;
            }
            set
            {
                this.currentSpisok = value;
                OnPropertyChanged(nameof(CurrentSpisok));
            }
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
                OnPropertyChanged(nameof(SearchResultsItem));
            }
        }
        
        public void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            SearchResultsItem = SearchDevice.GetSearchResultsPribor(searchBar.Text, SearchDevice.spisok_id);
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
