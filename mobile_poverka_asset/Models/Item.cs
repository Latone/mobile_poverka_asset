using System;

namespace mobile_poverka_asset.Models
{
    public class Item
    {
        public string Id { get; set; } = "";
        public string Serial { get; set; }
        public string idchannel { get; set; }
        public string spisok_id { get; set; }
        public bool inDataBase { get; set; } = false;
    }
}