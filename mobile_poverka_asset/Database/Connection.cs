using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using mobile_poverka_asset.ViewModels;

namespace mobile_poverka_asset.Database
{
    public static class Connection
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        public static void changeConnectionStatusTo(string val)
        {
            connText = val;
            NotifyStaticPropertyChanged("ConnectionUpdate");
        }
        static string connText = "Нет соединения";
        static NpgsqlConnection conn;
        public static NpgsqlConnection getConn() {
            return conn;
        }
        static public string getStatus()
        {
            return connText;
        }
            public static void Connect()
        {
            changeConnectionStatusTo("Устанавливаем соединение..");//--Триггер на изменение поля
            
            conn = new NpgsqlConnection(@"Server=192.168.43.4;Port=5432;User Id=postgres;Password=vjytnf1234;Database=postgres");
            
            conn.Open();
                if (conn != null && conn.State == ConnectionState.Open)
                    changeConnectionStatusTo("Соединение установлено"); //--Триггер на изменение поля
                else {
                    changeConnectionStatusTo("Ошибка соединения"); //--Триггер на изменение поля
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
