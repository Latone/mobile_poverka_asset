using System;
using System.Collections.Generic;
using System.Text;

namespace mobile_poverka_asset.ViewModels
{
    public class DB_Search_lvl2_priborDetailedViewModel : BaseViewModel
    {
        private string itemId;

        private string serial;
        private string idchannel;
        private string idspisok;
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

        public string IDspisok
        {
            get => idspisok;
            set => SetProperty(ref idspisok, value);
        }

        public string ItemId
        {
            get => itemId;
            set => SetProperty(ref idchannel, value);
        }
    }
}
