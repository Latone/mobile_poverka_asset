using Npgsql;
using System;
using System.Data.SqlClient;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using mobile_poverka_asset.ViewModels;
using System.Data.Sql;
using System.Text.Json;

namespace mobile_poverka_asset.Database
{
    public static class Connection
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        public static void changeConnectionStatusTo(string val, bool buttonf)
        {
            connText = val;
            button = buttonf;
            NotifyStaticPropertyChanged("ConnectionUpdate");
        }
        static string connText = "Нет соединения";
        static bool button = true;
        static NpgsqlConnection conn;
        public static string ListOfDBProfiles = "ListOfDBProfiles";
        public static NpgsqlConnection getConn() {
            return conn;
        }
        static public string getStatus()
        {
            return connText;
        }
        static public bool getStatusButton()
        {
            return button;
        }
        private static string getSettings() {
           

            var json = Preferences.Get(ListOfDBProfiles, "none");
            var list = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);
            
            var pref = Preferences.Get("LastConnDB", 0);

            return @"Server=" + list[pref].Server + ";" +
                                "Port=" + list[pref].Port + ";" +
                                "User Id=" + list[pref].UserId + ";" +
                                "Password=" + list[pref].Password + ";" +
                                "Database=" + list[pref].Database;
        }
        public static void SaveCurrentProfile(List<Models.ConnectionProfile> Updatedlist, int index)
        {
            var json = JsonSerializer.Serialize(Updatedlist);
            Preferences.Set(ListOfDBProfiles, json);

            Preferences.Set("LastConnDB", index);
        }
        public static void Connect()
        {
            changeConnectionStatusTo("Устанавливаем соединение..",false);//--Триггер на изменение поля

            conn = new NpgsqlConnection(getSettings());//@"Server=192.168.43.4;Port=5432;User Id=postgres;Password=vjytnf1234;Database=postgres");
            try { 

            conn.Open();
                if (conn != null && conn.State == ConnectionState.Open)
                    changeConnectionStatusTo("Соединение установлено",false); //--Триггер на изменение поля
                else {
                    changeConnectionStatusTo("Ошибка соединения",true); //--Триггер на изменение поля
                }
        }
            catch (Exception ex){
                //smth
                Console.WriteLine("Error Content Page -<-"+ ex.Message);
            
            }

    //NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO pribor (serial, idchannel) values (100,434)", conn);





    /*int numAffected = cmd.ExecuteNonQuery();
    if (numAffected == 0) {
        Console.WriteLine("Data not affected");
    }
    else
        Console.WriteLine("Number of rows affected: "+numAffected);*/




    /*NpgsqlDataReader reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        boolfound = true;
        string val = reader.ToString();
        Console.WriteLine("connection established" + "\n"+ val);
    }
    if (boolfound == false)
    {
        Console.WriteLine("Data does not exist");
    }
    reader.Close();*/

}
    }
}
