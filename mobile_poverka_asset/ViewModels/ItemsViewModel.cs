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

        public Command AddItemCommand { get; }
        public ItemsViewModel()
        {
            Title = "Список приборов";

            AddItemCommand = new Command(OnAddItem);
        }
        private async void OnAddItem(object obj)
        {
            if (Connection.getConn() == null && Connection.getConnMS() == null &&
                Connection.getConn().State == ConnectionState.Closed &&
                Connection.getConnMS().State == ConnectionState.Closed)
            {
                DependencyService.Get<IMessage>().LongAlert("Нет подключения\nс Базой Данных");
                return;
            }
            await Shell.Current.GoToAsync(nameof(NewItemPage));
            //await App.Current.MainPage.Navigation.PushAsync(new NewItemPage());
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

    }
}