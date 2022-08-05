using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using mobile_poverka_asset.Services;
using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;

namespace mobile_poverka_asset.Database
{
    class InsertDevice
    {
        static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public static event PropertyChangedEventHandler StaticPropertyChanged; //Можно подписываться на него

        public static void changeConnectionStatusTo(string val)
        {
            //connText = val;
            NotifyStaticPropertyChanged("DeviceUpdate");
        }
        public static async Task Query(string pool_name) {
            if (Connection.getConn() == null) return;
            List<Item> list = await BaseViewModel.DataStore.ReturnAllItemsThatAreNotAddedAsync();
            if (list.Count == 0) return;



            //Spisok table
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");

            string sp_query = "INSERT INTO spisok(name, date, count, complete, comment) values (\'" +
                pool_name + "\', \'" + sqlFormattedDate + "\', " + list.Count +", " +false+ ", " + "\'Добавлено с телефона\') RETURNING id;";
            NpgsqlCommand cmd = new NpgsqlCommand(sp_query, Connection.getConn());
            
            int numAffected = cmd.ExecuteNonQuery();
            if (numAffected == -1)
            {
                Console.WriteLine("Data not affected");
            }
            else
            {
                //await BaseViewModel.DataStore.DeleteItemAsync(serial.ToString(),channel.ToString());
                Console.WriteLine("Number of rows affected: " + numAffected);
            }

            //Get last (currently added) spisok id
            string spisok_id_text = "select id from spisok order by id desc limit 1;";
            cmd = new NpgsqlCommand(spisok_id_text, Connection.getConn());
            NpgsqlDataReader reader = cmd.ExecuteReader();
            
            reader.Read();
            int spisok_id = Int32.Parse(reader[0].ToString());
            reader.Close();

            //Pribor table
            string query = "INSERT INTO pribor (serial, idchannel, spisok_id) values ";
            foreach (Item item in list) {
                query += "(" + item.Serial + ", " + item.idchannel + ", " + spisok_id + "),";
            }
            query = query.Remove(query.Length - 1);
            query += ";";

            cmd = new NpgsqlCommand(query, Connection.getConn());

            numAffected = cmd.ExecuteNonQuery();
            if (numAffected == -1)
            {
                Console.WriteLine("Data not affected");
            }
            else
            {
                //await BaseViewModel.DataStore.DeleteItemAsync(serial.ToString(),channel.ToString());
                Console.WriteLine("Number of rows affected: " + numAffected);
            }
            foreach (Item arg in list) await BaseViewModel.DataStore.DeleteItemAsync(arg.Serial, arg.idchannel);

        }
    }

}
