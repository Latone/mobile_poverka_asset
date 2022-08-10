using mobile_poverka_asset.Database;
using mobile_poverka_asset.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mobile_poverka_asset.ViewModels
{
    public class DB_Search_lvl2_priborDetailedViewModel : BaseViewModel
    {
        private List<Item> resultItem;
        public Command ChangeContent { get; }
        public Command DeleteContent { get; }
        public DB_Search_lvl2_priborDetailedViewModel() {
            ResultItem = SearchDevice.currentItem;
            ChangeContent = new Command(ChangeContentButton_Clicked);
            DeleteContent = new Command(DeleteContentButton_Clicked);
        }
        async void DeleteContentButton_Clicked()
        {
            await ModifyDelete.DeletePribor();
            await Shell.Current.GoToAsync("..");
        }
        async void ChangeContentButton_Clicked() {
            await ModifyDelete.ModifyPribor();
        }
        public List<Item> ResultItem
        {
            get
            {
                return resultItem;
            }
            set
            {
                resultItem = value;
                OnPropertyChanged(nameof(ResultItem));
            }
        }
    }
}
