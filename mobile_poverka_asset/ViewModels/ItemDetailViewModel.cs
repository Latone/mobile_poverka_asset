using mobile_poverka_asset.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile_poverka_asset.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string serial;
        private string idchannel;
        public string Id { get; set; }

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

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Serial = item.Serial;
                IDchannel = item.idchannel;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
