using mobile_poverka_asset.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using mobile_poverka_asset.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using mobile_poverka_asset.Database;
using mobile_poverka_asset.Services;
using System.Linq;

namespace mobile_poverka_asset.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string serial;
        private string idchannel;

        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();


            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(serial)
                && !String.IsNullOrWhiteSpace(idchannel);
        }

        public string Serial
        {
            get => serial;
            set => SetProperty(ref serial, value);
        }

        public string IDchannel
        {
            get => idchannel;
            set => SetProperty(ref idchannel, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            if (Items.Count == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Список пуст, добавьте приборы");
                return;
            }
            var displayAlert = await App.Current.MainPage.DisplayAlert("Завершить добавление приборов?", "Перейти к этапу названия пула?", "Да", "Нет");
            if (displayAlert)
            {
                await Shell.Current.GoToAsync(nameof(ItemDetailPageNamingAndXML));
            }
            
        }

        private async void OnSave()
        {
            var list = await DataStore.GetAllitems();

            if (list.Where(arg => arg.Serial == Serial).Any())
            {
                DependencyService.Get<IMessage>().ShortAlert("Данный серийный номер уже существует в текущем списке");
                return;
            }
            if (list.Where(arg => arg.idchannel == idchannel).Any())
            {
                DependencyService.Get<IMessage>().ShortAlert("Данный номер канала уже используется в текущем списке");
                return;
            }

            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Serial = serial,
                idchannel = this.idchannel
            };
           
            await DataStore.AddItemAsync(newItem);
            await ExecuteLoadItemsCommand();
            Serial = "";
            IDchannel = "";
            // This will pop the current page off the navigation stack
            // await Shell.Current.GoToAsync("..");
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the
            //
            //
            // stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private string Vpool_name = "";

        public string pool_name
        {
            get
            {
                return Vpool_name;
            }
            set
            {
                Vpool_name = value;
                OnPropertyChanged(nameof(pool_name));
            }
        }
        
    }
}
