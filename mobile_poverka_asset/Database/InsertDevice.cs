using Npgsql;
using System.Data.SqlClient;
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
            if (!((Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open) ||
                (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open))) return;

            List<Item> list = await BaseViewModel.DataStore.ReturnAllItemsThatAreNotAddedAsync();
            if (list.Count == 0) return;



            //Spisok table
            string sqlFormattedDate = DateTime.Today.ToString("dd.MM.yyyy");
            int dateYEAR  = DateTime.Today.Year;
            int dateMONTH = DateTime.Today.Month;
            int dateDAY = DateTime.Today.Day;
            //string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");

            string sp_query = "INSERT INTO spisok(name, date, count, complete, comment) values (\'" +
                pool_name + "\', \'" + sqlFormattedDate + "\', " + list.Count +", " +false+ ", " + "\'Added via phone\');";

            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(sp_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                sp_query = "INSERT INTO spisok(name, date, count, complete, comment) values (\'" +
                pool_name + "\',"+ "\'"+ dateYEAR + "-"+ dateMONTH+ "-"+ dateDAY + "\'," + list.Count + ", " + 0 + ", " + "\'Added via phone\');"; //STR_TO_DATE(\"" + sqlFormattedDate.Replace('.',' - ') + "\",\"%d-%m-%y\"), "

                cmd = new SqlCommand(sp_query, Connection.getConnMS());
            }

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


            int spisok_id = -1;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                cmd = new NpgsqlCommand(spisok_id_text, Connection.getConn());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                spisok_id = Int32.Parse(reader[0].ToString());
                reader.Close();
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                spisok_id_text = "select top(1) * from spisok order by id desc;";
                cmd = new SqlCommand(spisok_id_text, Connection.getConnMS());
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                spisok_id = Int32.Parse(reader[0].ToString());
                reader.Close();
            }

            
            

            //Pribor table
            string query = "INSERT INTO pribor (serial, idchannel, spisok_id) values ";
            foreach (Item item in list) {
                query += "(\'" + item.Serial + "\', \'" + item.idchannel + "\', " + spisok_id + "),";
            }
            query = query.Remove(query.Length - 1);
            query += ";";

            
            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(query, Connection.getConnMS());


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
            //foreach (Item arg in list) await BaseViewModel.DataStore.DeleteItemAsync(arg.Serial, arg.idchannel);
            
        }
    }

}
