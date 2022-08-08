using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using mobile_poverka_asset.Services;
using System.Threading.Tasks;

namespace mobile_poverka_asset.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Главная";
            Connection.StaticPropertyChanged += OnCSChange;
            Connection.changeConnectionStatusTo("Нет соединения",true);
            Button_Connect = true;
        }
        void OnCSChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionUpdate")
            {
                ConnectionStatus = "Статус: " + Connection.getStatus();
                Button_Connect = Connection.getStatusButton();

                Device.BeginInvokeOnMainThread(() =>
                {
                    string alertstring = Connection.getStatus().Contains("установ") ?
                                            Connection.getStatus() + " с базой данных \'test\'" : Connection.getStatus();
                    DependencyService.Get<IMessage>().ShortAlert(alertstring);
                });
            }
        }
        
        private bool VButton_Connect;
        public bool Button_Connect
        {
            get
            {
                return VButton_Connect;
            }
            set
            {
                VButton_Connect = value;
                OnPropertyChanged(nameof(Button_Connect));
            }
        }
        private string VConnectionStatus;
        public string ConnectionStatus
        {
            get
            {
                return VConnectionStatus;
            }
            set
            {
                VConnectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }

    }
}