using mobile_poverka_asset.ViewModels;
using mobile_poverka_asset.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace mobile_poverka_asset
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(DB_Search_lvl2), typeof(DB_Search_lvl2));
            Routing.RegisterRoute(nameof(DB_Search_lvl2_priborDetailed), typeof(DB_Search_lvl2_priborDetailed));

        }

    }
}
