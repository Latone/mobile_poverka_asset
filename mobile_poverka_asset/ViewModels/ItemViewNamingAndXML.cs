using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using mobile_poverka_asset.Views;
using mobile_poverka_asset.Database;
using mobile_poverka_asset.Services;
using System.Data;
using mobile_poverka_asset.XML;

namespace mobile_poverka_asset.ViewModels
{
    public class ItemViewNamingAndXML : BaseViewModel
    {
        public Command ClearName { get; }
        public Command Finish { get; }
        public ItemViewNamingAndXML() {
            string fulldate = DateTime.Today.Day.ToString("D2") + "-"+ DateTime.Today.Month.ToString("D2") + "-" + DateTime.Today.Year.ToString().Substring(2);
            int number = SearchDevice.GetNumberOfTodaysSpisok(fulldate)+1;
            fulldate += "-" + number.ToString("D2");

            ProfileName = fulldate;
            ClearName = new Command(ButtonClearName);
            Finish = new Command(ButtonFinish);
        }
        public void ButtonClearName() {
            ProfileName = "";
        }
        public async void ButtonFinish()
        {
            var displayAlert = await App.Current.MainPage.DisplayAlert("Завершить создание списка?", "Закончить процедуру?", "Да", "Нет");
            if (displayAlert)
            {
                displayAlert = await App.Current.MainPage.DisplayAlert("Отправить на ПК XML?", "", "Да", "Нет");
                var numbeOfItems = await DataStore.GetNumberOfitems();
                var listItems = await DataStore.GetAllitems();
                try {

                    if (Connection.getConn() == null ||
                    Connection.getConn().State == ConnectionState.Closed)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Нет подключения\nс Базой Данных");
                        return;
                    }
                    else
                    {
                        await InsertDevice.Query(ProfileName);
                        DependencyService.Get<IMessage>().LongAlert("Добавлено " + numbeOfItems + " приборов\nв новый список \"" + ProfileName + "\"");

                        ProfileName = "";

                    }

                    if (displayAlert)
                    {//XML
                        
                        Models.Spisok spisok = new Models.Spisok()
                        {
                            Id = SearchDevice.GetLastSpisoksID().ToString(),
                            Name = ProfileName,
                            Date = DateTime.Today.ToString("dd.MM.yyyy"),
                            Count = numbeOfItems.ToString(),
                            Complete = "False",
                            Comment = "Добавлено с телефона",
                        };
                        XML.XML.CreateXML(listItems, spisok);
                    }
                    await DataStore.ClearAllItems();

                }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }
            await Shell.Current.GoToAsync("../..");
            }
        }
        private string profileName;

        public string ProfileName
        {
            get
            {
                return profileName;
            }
            set
            {
                profileName = value;
                OnPropertyChanged(nameof(ProfileName));
            }
        }
    }
}
