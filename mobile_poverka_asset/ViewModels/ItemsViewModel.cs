using mobile_poverka_asset.Models;
using mobile_poverka_asset.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using System.Data;
using mobile_poverka_asset.Services;

namespace mobile_poverka_asset.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {

        public Command CreateTablesCommand { get; }
        public Command AddItemCommand { get; }
        public ItemsViewModel()
        {
            Title = "Список приборов";

            AddItemCommand = new Command(OnAddItem);

            CreateTablesCommand = new Command(CreateTables_Clicked);

            if (!SearchDevice.CheckForTables())
            {
                CreateTablesVis = true;
                CreateSpisokVis = false;
            }
            else
            {
                CreateTablesVis = false;
                CreateSpisokVis = true;
            }
        }
        async void CreateTables_Clicked()
        {
            if (!checkConn())
                return;

            CreateTables.Create();
            return;
        }
        private async void OnAddItem(object obj)
        {
            if (!checkConn())
                return;
            await Shell.Current.GoToAsync(nameof(NewItemPage));
            //await App.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }
        public bool checkConn()
        {
            if (!((Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)||
                (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)))
            {
                DependencyService.Get<IMessage>().LongAlert("Нет подключения\nс Базой Данных");
                return false;
            }
            return true;
        }
        public void OnAppearing()
        {
            IsBusy = true;

            if (!SearchDevice.CheckForTables())
            {
                CreateTablesVis = true;
                CreateSpisokVis = false;
            }
            else
            {
                CreateTablesVis = false;
                CreateSpisokVis = true;
            }
        }


        private bool createSpisokVis;
        public bool CreateSpisokVis
        {
            get
            {
                return createSpisokVis;
            }
            set
            {
                createSpisokVis = value;
                OnPropertyChanged(nameof(CreateSpisokVis));
            }
        }

        private bool createTablesVis;
        public bool CreateTablesVis
        {
            get
            {
                return createTablesVis;
            }
            set
            {
                createTablesVis = value;
                OnPropertyChanged(nameof(CreateTablesVis));
            }
        }
    }
}