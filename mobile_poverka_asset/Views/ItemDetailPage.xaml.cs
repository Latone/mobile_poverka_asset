using mobile_poverka_asset.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using System;
using Xamarin.Forms.Xaml;
using mobile_poverka_asset.Models;

namespace mobile_poverka_asset.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
        async void ChangeContentButton_Clicked(object sender, EventArgs e)
        {
            Item item = await BaseViewModel.DataStore.GetItemAsync(((ItemDetailViewModel)BindingContext).ItemId);
            item.Serial = ((ItemDetailViewModel)BindingContext).Serial;
            item.idchannel = ((ItemDetailViewModel)BindingContext).IDchannel;

            await BaseViewModel.DataStore.UpdateItemAsync(item);

            await Navigation.PopAsync();
        }
    }
}