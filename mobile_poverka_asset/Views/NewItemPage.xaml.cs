using mobile_poverka_asset.Models;
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

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            
            //Return key to focus on after dealt with the "TopEntry" - input (returning command)
            this.TopEntry.ReturnCommand = new Command(() => this.BottomEntry.Focus());

            // or you can use this to call a command on your viewmodel
            //this.BottomEntry.ReturnCommand = new Command(() => this.TopEntry.Focus());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
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
    }
}