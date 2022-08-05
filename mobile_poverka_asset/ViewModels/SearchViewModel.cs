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
    public class SearchViewModel : INotifyPropertyChanged
    {
        private List<string> listPicker;
        public Command<Spisok> SpisokTapped { get; }
        public ICommand ModifyButton { get; set; }
        public ICommand DropModifyButton { get; set; }
        public ICommand SaveModifyButton { get; set; }

        public string spisok_id;
        public event PropertyChangedEventHandler PropertyChanged;

        public SearchViewModel(){

            listPicker = new List<string>();
            listPicker.AddRange(new List<string>
            {
                "5",
                "10",
                "20",
                "30",
            });

            SelectedPicker = listPicker[0];

            SpisokTapped = new Command<Spisok>(OnSpisokSelected);
        }
        async void OnSpisokSelected(Spisok spisok)
        {
            if (spisok == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            List<Spisok> h = new List<Spisok>();
            h.Add(spisok);
            SearchDevice.currentSpisok = h;

            spisok_id=spisok.Id;
            SearchDevice.spisok_id = spisok.Id;

            await Shell.Current.GoToAsync($"{nameof(DB_Search_lvl2)}");
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand PerformSearchItem => new Command<string>((string query) =>
        {
            SearchResultsItem = SearchDevice.GetSearchResultsPribor(query,spisok_id);
        });

        public ICommand PerformSearchSpisok => new Command<string>((string query) =>
        {
            SearchResultsSpisok = SearchDevice.GetSearchResultsSpisok(query,SelectedPicker);
        });
       
        public List<string> ListPicker
        {
            get
            {
                return listPicker;
            }
            set
            {
                listPicker = value;
                NotifyPropertyChanged();
            }
        }
        private string selectedPicker;
        public string SelectedPicker
        {
            get
            {
                return selectedPicker;
            }
            set
            {

                selectedPicker = value;
                SearchResultsSpisok = SearchDevice.GetSearchResultsSpisok("", value);
                NotifyPropertyChanged();
            }
        }
        
        
        private Task<bool> DisplayAlert(string v1, string v2, string v3, string v4)
        {
            throw new NotImplementedException();
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
        private List<Spisok> searchResultsSpisok;
        public List<Spisok> SearchResultsSpisok
        {
            get
            {
                return searchResultsSpisok;
            }
            set
            {
                searchResultsSpisok = value;
                NotifyPropertyChanged();
            }
        }
    }
}
