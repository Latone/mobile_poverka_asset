using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile_poverka_asset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPageNamingAndXML : ContentPage
    {
        public ItemDetailPageNamingAndXML()
        {
            InitializeComponent();
            //BindingContext = this;
        }
        private void Next_Entry_Clicked(object sender, EventArgs e)
        {
            this.profileName.Focus();
        }
    }
}