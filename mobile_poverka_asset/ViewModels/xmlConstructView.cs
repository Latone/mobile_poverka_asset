using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using mobile_poverka_asset.Services;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace mobile_poverka_asset.ViewModels
{
    public class xmlConstructView : BaseViewModel
    {
        public Command SendXML { get; }
        public xmlConstructView() {
            SendXML = new Command(sendXML_Clicked);
            IP = Preferences.Get("XMLIP", "192.168.1.1");
            Port = Preferences.Get("XMLPORT", "30001");
        }
       
        public void sendXML_Clicked() {

            try {
                if (!XML.XML.CreateXML(IP, Int32.Parse(Port)))
                    return;
            }
            catch (Exception ex) {
            }
            DependencyService.Get<IMessage>().ShortAlert("XML-документ отправлен на адрес:\n"+IP+":"+Port);

            ItemViewNamingAndXML.returnedFromXML = true;

            Preferences.Set("XMLIP", IP);
            Preferences.Set("XMLPORT", port);

            DataStore.ClearAllItems();
            Shell.Current.GoToAsync("..");
        }
        private string ip;
        public string IP {
            get {
                return ip;
            }
            set {
                ip = value;
                OnPropertyChanged(nameof(IP));
            }
        }
        private string port;
        public string Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                OnPropertyChanged(nameof(Port));
            }
        }
    }
}
