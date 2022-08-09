using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mobile_poverka_asset.Database;
using mobile_poverka_asset.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using mobile_poverka_asset.Services;
using mobile_poverka_asset.Models;
using Xamarin.Essentials;
using System.Text.Json;
using System.Linq;

namespace mobile_poverka_asset.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            List<Task> tasks = new List<Task>();
            try
            {
                tasks.Add(Task.Run(() => Connection.Connect()));
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }
        }
        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //connlist
        }
        protected override bool OnBackButtonPressed()
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
        }
        void ConnectDBButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Connection.Connect();
            }
            catch (Exception ex){
                //smth
                Console.WriteLine("Error Content Page -<-"+ ex.Message);
            
            }
        }
    }
}