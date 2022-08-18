using mobile_poverka_asset.Database;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile_poverka_asset.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private List<string> listPicker;
        public Command<Spisok> SpisokTapped { get; }

        //public Command<Item> ItemTapped { get; }
        public ICommand ModifyButton { get; set; }
        public ICommand DropModifyButton { get; set; }
        public ICommand SaveModifyButton { get; set; }

        public string spisok_id;
        //public string item_id;
        public event PropertyChangedEventHandler PropertyChanged;

        public SearchViewModel()
        {

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
            //ItemTapped = new Command<Item>(OnItemSelected);
        }
        
        async void OnSpisokSelected(Spisok spisok)
        {
            if (spisok == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            List<Spisok> h = new List<Spisok>();
            h.Add(spisok);
            SearchDevice.currentSpisok = h;

            spisok_id = spisok.Id;
            SearchDevice.spisok_id = spisok.Id;

            await Shell.Current.GoToAsync($"{nameof(DB_Search_lvl2)}");
        }


        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public ICommand PerformSearchSpisok => new Command<string>((string query) =>
        {
            searchResultsSpisok = SearchDevice.GetSearchResultsSpisok(query, SelectedPicker);

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

                NotifyPropertyChanged();
            }
        }


        private Task<bool> DisplayAlert(string v1, string v2, string v3, string v4)
        {
            throw new NotImplementedException();
        }

        private bool tables_exist;
        public bool Tables_exist
        {
            get
            {
                return tables_exist;
            }
            set
            {
                tables_exist = value;
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
