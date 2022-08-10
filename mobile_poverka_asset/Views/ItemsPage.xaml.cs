using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using System;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Threading;
using mobile_poverka_asset.Services;

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
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                var displayAlert = await this.DisplayAlert("Закрыть приложение?", "Вы собираетесь полностью закрыть приложение, вы точно уверены?", "Да", "Нет");
                if (displayAlert)
                {
                    DependencyService.Get<IAndroidMethods>().CloseApp();
                }

            });
            return true;
        }

            async void WriteDBButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_viewModel.pool_name.Length < 1)
                {
                    _viewModel.pool_error = true;
                    return;
                }
                else
                    _viewModel.pool_error = false;

                if (_viewModel.Items.Count < 1)
                {
                   // await Shell.Current.GoToAsync($"{nameof(NewItemPage)}");
                    DependencyService.Get<IMessage>().LongAlert("Добавьте приборы, прежде чем создать новый список");
                   await Navigation.PushAsync(new NewItemPage(), true);
                    return;
                }
                await InsertDevice.Query(_viewModel.pool_name);
                if(Connection.getConn() == null)
                    DependencyService.Get<IMessage>().LongAlert("Нет подключения\nс Базой Данных");
                else
                    DependencyService.Get<IMessage>().LongAlert("Добавлено "+_viewModel.Items.Count+ " приборов\nв новый список \""+ _viewModel.pool_name+"\"");
                
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