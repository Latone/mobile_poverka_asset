using mobile_poverka_asset.Models;
using mobile_poverka_asset.Services;
using mobile_poverka_asset.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_poverka_asset.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        readonly NewItemViewModel _viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new NewItemViewModel();

            //Return key to focus on after dealt with the "TopEntry" - input (returning command)
            this.TopEntry.ReturnCommand = new Command(() => this.BottomEntry.Focus());

            // or you can use this to call a command on your viewmodel
            //this.BottomEntry.ReturnCommand = new Command(() => this.TopEntry.Focus());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            await Task.Run(() =>
            {
                Task.Delay(100);
                Device.BeginInvokeOnMainThread(()=>
                {
                    this.TopEntry.Focus();
                });
            });
        }
        private void Next_Entry_Clicked(object sender, EventArgs e) {
            this.TopEntry.Focus();
        }

        private Entry Vpool_entry;

        public Entry pool_entry
        {
            get
            {
                return Vpool_entry;
            }
            set
            {
                Vpool_entry = value;
                OnPropertyChanged(nameof(pool_entry));
            }
        }
        /*protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var displayAlert = await this.DisplayAlert("Закрыть приложение?", "Вы собираетесь полностью закрыть приложение, вы точно уверены?", "Да", "Нет");
                if (displayAlert)
                {
                    DependencyService.Get<IAndroidMethods>().CloseApp();
                }

            });
            return true;
        }*/
    }
}