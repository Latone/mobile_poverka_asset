using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using System;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Threading;

namespace mobile_poverka_asset.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        async void WriteDBButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await InsertDevice.Query(_viewModel.pool_name);
                _viewModel.pool_name = "";
                _viewModel.Items.Clear();
                
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }

        }

    }
    
}