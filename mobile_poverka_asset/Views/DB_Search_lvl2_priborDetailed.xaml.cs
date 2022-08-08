using mobile_poverka_asset.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using System;
using Xamarin.Forms.Xaml;
using mobile_poverka_asset.Models;

namespace mobile_poverka_asset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DB_Search_lvl2_priborDetailed : ContentPage
    {
        public DB_Search_lvl2_priborDetailed()
        {
            InitializeComponent();
            BindingContext = new DB_Search_lvl2_priborDetailedViewModel();
        }
        async void ChangeContentButton_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PopAsync();
        }
    }
}