using mobile_poverka_asset.Services;
using mobile_poverka_asset.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;

namespace mobile_poverka_asset
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IMessage, MessageAndroid>();
            MainPage = new AppShell();
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
