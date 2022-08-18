using mobile_poverka_asset.Database;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_poverka_asset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DB_Search_lvl2 : ContentPage
    {
        public DB_Search_lvl2()
        {
            InitializeComponent();
            Title = "Список: "+ SearchDevice.currentSpisok[0].Name;
            //CurrentSpisok = SearchDevice.currentSpisok;

            RedVis = true;
            DelVis = true;
            DropVis = false;
            SaveVis = false;

            //searchResultsItem.ItemsSource = SearchDevice.GetSearchResultsPribor("", SearchDevice.spisok_id);

            //BindingContext = _viewModel = new DB_Search_lvl2ViewModel();
        }
        
        void ModifyPoolButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                RedVis = false;
                DelVis = false;
                DropVis = true;
                SaveVis = true;

                ReadOnly = false;

                //await ModifyDelete.deletePool();
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }
        }
        void DropModifyPoolButton_Clicked(object sender, EventArgs e)
        {
            RedVis = true;
            DelVis = true;
            DropVis = false;
            SaveVis = false;
        }
        void SaveModifyPoolButton_Clicked(object sender, EventArgs e)
        {
            RedVis = true;
            DelVis = true;
            DropVis = false;
            SaveVis = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            searchResultsItem.ItemsSource = SearchDevice.GetSearchResultsPribor("", SearchDevice.spisok_id);
            currentSpisok.ItemsSource = null;
            currentSpisok.ItemsSource =  SearchDevice.currentSpisok;
            
        }
        private bool readOnly;
        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; OnPropertyChanged(nameof(ReadOnly)); }
        }

        private bool redVis;
        public bool RedVis
        {
            get
            { 
                return redVis;
            }
            set { 
                redVis = value; 
                OnPropertyChanged(nameof(RedVis)); 
            }
        }

        private bool delVis;
        public bool DelVis
        {
            get 
            { 
                return delVis; 
            }
            set { 
                delVis = value; 
                OnPropertyChanged(nameof(DelVis)); 
            }
        }

        private bool dropVis;
        public bool DropVis
        {
            get 
            {
                return dropVis; 
            }
            set {
                dropVis = value;
                OnPropertyChanged(nameof(DropVis)); 
            }
        }

        private bool saveVis;
        public bool SaveVis
        {
            get { return saveVis; }
            set { saveVis = value; OnPropertyChanged(nameof(SaveVis)); }
        }
        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            searchResultsItem.ItemsSource = SearchDevice.GetSearchResultsPribor(searchBar.Text, SearchDevice.spisok_id);
        }
       
    }
}