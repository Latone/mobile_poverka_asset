using mobile_poverka_asset.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile_poverka_asset.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string serial;
        private string idchannel;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {

            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Serial = serial,
                idchannel = this.idchannel
            };
           
            await DataStore.AddItemAsync(newItem);

            Serial = "";
            IDchannel = "";
            // This will pop the current page off the navigation stack
            // await Shell.Current.GoToAsync("..");
        }
    }
}
