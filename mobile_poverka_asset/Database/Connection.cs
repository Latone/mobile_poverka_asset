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
using Xamarin.Forms;
using mobile_poverka_asset.Services;
using System.Linq;

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
        static SqlConnection connMS;
        public static string ListOfDBProfiles = "ListOfDBProfiles";
        public static NpgsqlConnection getConn()
        {
            return conn;
        }
        public static SqlConnection getConnMS()
        {
            return connMS;
        }
        static public string getStatus()
        {
            return connText;
        }
        static public bool getStatusButton()
        {
            return button;
        }
        private static string getSettings()
        {
            var json = Preferences.Get(ListOfDBProfiles, "none");
            var list = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);

            var pref = Preferences.Get("LastConnDB", 0);

            return @"Server=" + list[pref].Server + ";" +
                                "Port=" + list[pref].Port + ";" +
                                "User Id=" + list[pref].UserId + ";" +
                                "Password=" + list[pref].Password + ";" +
                                "Database=" + list[pref].Database;
        }

        public static string getSettingsMS()
        {
            var json = Preferences.Get(ListOfDBProfiles, "none");
            var list = JsonSerializer.Deserialize<List<Models.ConnectionProfile>>(json);

            var pref = Preferences.Get("LastConnDB", 0);

            return @"Server="+list[pref].Server +","+ list[pref].Port + ";" +
                                "User Id=" + list[pref].UserId + ";" +
                                "Password=" + list[pref].Password + ";" +
                                "Initial Catalog=" + list[pref].Database + ";" +
                                "MultipleActiveResultSets = true";
        }
        public static void SaveCurrentProfile(List<Models.ConnectionProfile> Updatedlist, int index)
        {
            var json = JsonSerializer.Serialize(Updatedlist);
            Preferences.Set(ListOfDBProfiles, json);

            Preferences.Set("LastConnDB", index);
        }
        public static bool Disconnect() {
            if (!(conn.State == ConnectionState.Open) && !(connMS.State == ConnectionState.Open))
                return false;


            changeConnectionStatusTo("Разрываем соединение..", false);
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                   conn.Close();

                if (connMS != null && connMS.State == ConnectionState.Open)
                    connMS.Close();

                if (conn != null && conn.State == ConnectionState.Closed)
                    changeConnectionStatusTo("Соединение разорвано", false); //--Триггер на изменение поля
                else
                {
                    changeConnectionStatusTo("Ошибка отключения", true); //--Триггер на изменение поля
                }

                if (connMS != null && connMS.State == ConnectionState.Closed)
                    changeConnectionStatusTo("Соединение разорвано", false); //--Триггер на изменение поля
                else
                {
                    changeConnectionStatusTo("Ошибка отключения", true); //--Триггер на изменение поля
                }
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);
                changeConnectionStatusTo("Ошибка отключения", true);
            }
            return true;
        }
        public static void Connect()
        {
            /*try
            {
                using (var db = new PoverkaContext())
                {
                    var blogs = db.Spisoks
                        .Where(b => b.Id > 3)
                        .OrderByDescending(b => b.Id)
                        .ToList();

                    var gg = blogs;
                }
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);
                changeConnectionStatusTo("Ошибка соединения", true);
            }*/
            changeConnectionStatusTo("Устанавливаем соединение..", false);//--Триггер на изменение поля

           //@"Server=192.168.43.4;Port=5432;User Id=postgres;Password=vjytnf1234;Database=postgres");

           //POSTGRESQL
            try
            {
                conn = new NpgsqlConnection(getSettings());
                conn.Open();
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    changeConnectionStatusTo("Соединение установлено", false); //--Триггер на изменение поля
                    return;
                }
                else
                {
                    conn.Close();
                    changeConnectionStatusTo("Ошибка соединения", true); //--Триггер на изменение поля
                }
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);
                //changeConnectionStatusTo("Ошибка соединения", true); --Uncomment this if everything gone wild
            }
            //MSSQL
            try
            {
                connMS = new SqlConnection(getSettingsMS());
                connMS.Open();
                if (connMS != null && connMS.State == ConnectionState.Open)
                    changeConnectionStatusTo("Соединение установлено", false); //--Триггер на изменение поля
                else
                {
                    changeConnectionStatusTo("Ошибка соединения", true); //--Триггер на изменение поля
                }
            }
            catch (Exception ex)
            {
                //smth
                connMS.Close();
                Console.WriteLine("Error Content Page -<-" + ex.Message);
                changeConnectionStatusTo("Ошибка соединения", true);
            }
        }
    }
}