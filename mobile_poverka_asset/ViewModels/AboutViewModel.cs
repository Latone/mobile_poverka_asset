using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using mobile_poverka_asset.Database;

namespace mobile_poverka_asset.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Главная";
            Connection.StaticPropertyChanged += OnCSChange;
            Connection.changeConnectionStatusTo("Нет соединения");
        }
        void OnCSChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionUpdate")
                ConnectionStatus = "Статус: " + Connection.getStatus();
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