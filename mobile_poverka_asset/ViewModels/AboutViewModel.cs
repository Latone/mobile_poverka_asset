using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using mobile_poverka_asset.Database;
using mobile_poverka_asset.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections.Generic;
using mobile_poverka_asset.Models;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace mobile_poverka_asset.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command SaveProfile { get; }
        public Command CreateNewProfile {get;}
        private bool createfrombutton = false;
        private bool selectedPickerWasSelected = false;
        public AboutViewModel()
        {
            Title = "Главная";
            Connection.StaticPropertyChanged += OnCSChange;
            Connection.changeConnectionStatusTo("Нет соединения",true);
            Button_Connect = true;
            SaveProfile = new Command(SaveConProfButton_Clicked);
            CreateNewProfile = new Command(CreateNewProfButton_Clicked);
            //Preferences.Clear();
            var json = Preferences.Get(Connection.ListOfDBProfiles, "none");
            //Preferences.Set("LastConnDB", 0);
            if (json == "none")
            {
                CreateNewProfButton_Clicked();
                json = Preferences.Get(Connection.ListOfDBProfiles, "none");
            }
            var connindex = Preferences.Get("LastConnDB", 0);
            List<Models.ConnectionProfile> lastprof = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);
            Connlist = new List<Models.ConnectionProfile>();
            Connlist.Add(lastprof[connindex]);

            ListPicker = new List<string>();
            ListPicker = lastprof.Select(arg => { return arg.ProfileName; }).ToList();


            SelectedPicker = ListPicker[connindex];

            if (Connlist[0].Id == null)
                Connlist[0].Id = generateID();
            Preferences.Set("LastProfId", Connlist[0].Id);
        }
        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
        void OnCSChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConnectionUpdate")
            {
                ConnectionStatus = "Статус: " + Connection.getStatus();
                Button_Connect = Connection.getStatusButton();

                
            }
        }
        void CreateNewProfButton_Clicked()
        {
            selectedPickerWasSelected = true;
            createfrombutton = true;
            var json = Preferences.Get(Connection.ListOfDBProfiles, "none");
            if (json == "none") {
                var listg = new List<Models.ConnectionProfile>();
                listg.Add(new Models.ConnectionProfile
                {
                    Id = generateID(),
                    Server = "",
                    Port = "",
                    UserId = "",
                    Password = "",
                    Database = "",
                    ProfileName = "New 1",
                });
                json = JsonSerializer.Serialize(listg);
                Preferences.Set(Connection.ListOfDBProfiles, json);
                return;
                }
            List<Models.ConnectionProfile> savetolist = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);
            List<Models.ConnectionProfile> newprof = new List<Models.ConnectionProfile>();
            newprof.Add(new Models.ConnectionProfile {
                Id = generateID(),
                Server = "",
                Port = "",
                UserId = "",
                Password = "",
                Database = "",
                ProfileName = checkForExistingProfileName("New " + (savetolist.Count + 1),true),
            });
            
            savetolist.Add(newprof[0]);
            json = JsonSerializer.Serialize(savetolist); //alloldprofiles + new Empty
            Preferences.Set(Connection.ListOfDBProfiles, json);

            
            Connlist = newprof;
            SelectedPicker = Connlist[0].ProfileName;
            ListPicker = null;
            ListPicker = savetolist.Select(arg => { return arg.ProfileName; }).ToList();
            Preferences.Set("LastProfId", Connlist[0].Id);
            SaveConProfButton_Clicked();
            selectedPickerWasSelected = false;
        }
        public string checkForExistingProfileName(string newName, bool createdbybutton)
        {
            var json = Preferences.Get(Connection.ListOfDBProfiles, "none");
            var list = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);
            while (createdbybutton)
            {
                if (list.Where(arg => arg.ProfileName == newName).Any()) { 
                    
                    int resultString = Int32.Parse(Regex.Match(newName, @"\d+").Value);
                    newName = "New " + resultString + 1;
                    continue;
                }
                else
                    return newName;

            }
            if(list.Where(arg => arg.ProfileName == newName && SelectedPicker !=arg.ProfileName).Any()  )
                return "AlreadyExisting";
            return "Ok";
        }
        void SaveConProfButton_Clicked()
        {
            try
            {
                selectedPickerWasSelected = true;
                if (checkForExistingProfileName(Connlist[0].ProfileName, false) == "AlreadyExisting" && createfrombutton==false) {
                    DependencyService.Get<IMessage>().ShortAlert("Имя профиля "+ Connlist[0].ProfileName +" уже существует");

                    return;
                }
                createfrombutton = false;
                var json = Preferences.Get(Connection.ListOfDBProfiles, "none");
                var list = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);
                var lastprofId = Preferences.Get("LastProfId", "none");
                int index = list.FindIndex(arg => arg.Id == lastprofId);
                int lp = ListPicker.FindIndex(arg => arg == list[index].ProfileName);

                var updatedobject = list.Where(arg => arg.Id == lastprofId).Select(c => { c = Connlist[0]; return c; }).ToList();//updatedlist.Where(arg => arg.Id == lastprofId).Select
                //list.ForEach(arg => arg =  arg.Id == lastprofId ? Connlist[0] : arg);
                var INDEXOFLIST = list.FindIndex(arg => arg.Id == lastprofId);
                list[INDEXOFLIST] = updatedobject[0];

                Connection.SaveCurrentProfile(list, index);

                List<string> newlist = ListPicker;
                newlist[lp] = Connlist[0].ProfileName;
                ListPicker = null;
                ListPicker = newlist;
                

                SelectedPicker = Connlist[0].ProfileName;
                Preferences.Set("LastProfId", Connlist[0].Id);
                selectedPickerWasSelected = false;
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error ViewModel Page -<-" + ex.Message);

            }
        }
        private List<string> listPicker;
        public List<string> ListPicker
        {
            get
            {
                return listPicker;
            }
            set
            {
                listPicker = value;
                OnPropertyChanged(nameof(ListPicker));
            }
        }
        private string selectedPicker;
        public string SelectedPicker
        {
            get
            {
                return selectedPicker;
            }
            set
            {

                selectedPicker = value;
                if (selectedPickerWasSelected == false)
                {
                    var json = Preferences.Get(Connection.ListOfDBProfiles, "none");
                    var list = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);
                    

                    Connlist = list.Where(arg => arg.ProfileName == selectedPicker).ToList();
                    Preferences.Set("LastProfId", Connlist[0].Id);

                    var lastprofId = Preferences.Get("LastProfId", "none");
                    int index = list.FindIndex(arg => arg.Id == lastprofId);
                    Preferences.Set("LastConnDB", index);
                    Button_Connect = true;
                }
                OnPropertyChanged(nameof(SelectedPicker));
            }
        }
        private List<Models.ConnectionProfile> connlist;
        public List<Models.ConnectionProfile> Connlist
        {
            get
            {
                return connlist;
            }
            set
            {
                connlist = value;
                OnPropertyChanged(nameof(Connlist));
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