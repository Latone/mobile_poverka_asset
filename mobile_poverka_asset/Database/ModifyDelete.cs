using mobile_poverka_asset.Models;
using mobile_poverka_asset.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile_poverka_asset.Database
{
    class ModifyDelete
    {
        public async static Task<bool> DeletePribor() {

            string spisok_id = "";
            if (SearchDevice.spisok_id != null)
                spisok_id = SearchDevice.spisok_id;


            string pribor_id = "";
            if (SearchDevice.item_id != null)
                pribor_id = SearchDevice.item_id;
            else
                return false;

            int newPriborCount = Int32.Parse(SearchDevice.currentSpisok[0].Count) - 1;
            string pr_query = "DELETE FROM pribor WHERE id = \'" + pribor_id + "\'";
            string sp_query = "UPDATE spisok SET " +
                                    "count = \'" + newPriborCount + "\' " +
                                    "WHERE id =" + spisok_id+ ";";
           
            var cmd = (dynamic)null;


            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(pr_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(pr_query, Connection.getConnMS());

            //Remove pribor from table
            int numAffected = cmd.ExecuteNonQuery();
            if (numAffected == -1)
            {
                Console.WriteLine("Data not affected");
            }
            else
            {
                //await BaseViewModel.DataStore.DeleteItemAsync(serial.ToString(),channel.ToString());
                Console.WriteLine("Number of rows affected: " + numAffected);
                DependencyService.Get<IMessage>().ShortAlert("Прибор \'" + pribor_id + "\' удалён из списка \'" +spisok_id+"\'");
            }

            //Update count in spisok
            
            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(sp_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(sp_query, Connection.getConnMS());

            numAffected = cmd.ExecuteNonQuery();
            if (numAffected == -1)
            {
                Console.WriteLine("Data not affected");
            }
            else
            {
                //await BaseViewModel.DataStore.DeleteItemAsync(serial.ToString(),channel.ToString());
                SearchDevice.currentSpisok[0].Count = newPriborCount.ToString();
                Console.WriteLine("Number of rows affected: " + numAffected);
            }
            return await Task.FromResult(true);
        }
        public async static Task<bool> ModifyPribor()
        {
            string pribor_id = "";
            if (SearchDevice.item_id != null)
                pribor_id = SearchDevice.item_id;
            else
                return false;
            if (Connection.getConn() == null) return false;

            string pr_query = "UPDATE pribor SET " +
                                     "serial = \'" + SearchDevice.currentItem[0].Serial + "\'," +
                                     "idchannel = \'" + SearchDevice.currentItem[0].idchannel + "\'," +
                                     "spisok_id = \'" + SearchDevice.currentItem[0].spisok_id + "\' " +
                                     "WHERE id =" + pribor_id + ";";


            var cmd = (dynamic)null;


            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(pr_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(pr_query, Connection.getConnMS());

            int numAffected = cmd.ExecuteNonQuery();
            if (numAffected == -1)
            {
                Console.WriteLine("Data not affected");
            }
            else
            {
                //await BaseViewModel.DataStore.DeleteItemAsync(serial.ToString(),channel.ToString());
                Console.WriteLine("Number of rows affected: " + numAffected);
                DependencyService.Get<IMessage>().ShortAlert("Изменения для прибора \'"+pribor_id+"\' сохранены");
            }

            return await Task.FromResult(true);
        }
       
        public async static Task<bool> ModifyPool()
        {

            string spisok_id = "";
            if (SearchDevice.spisok_id != null)
                spisok_id = SearchDevice.spisok_id;
            //if (SearchDevice.currentSpisok[0] != null)
            //    spisok_id = SearchDevice.currentSpisok[0].Id;
            else
                return false;
            if (Connection.getConn() == null) return false;

            //////
            string sp_query = "UPDATE spisok SET "+
                                     "id = \'" + SearchDevice.currentSpisok[0].Id + "\',"+
                                     "name = \'" + SearchDevice.currentSpisok[0].Name + "\'," +
                                     "date = \'" + SearchDevice.currentSpisok[0].Date + "\'," +
                                     "count = \'" + SearchDevice.currentSpisok[0].Count + "\'," +
                                     "complete = \'" + SearchDevice.currentSpisok[0].Complete + "\'," +
                                     "comment = \'" + SearchDevice.currentSpisok[0].Comment + "\' " +
                                     "WHERE id =" +spisok_id+";";

            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(sp_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(sp_query, Connection.getConnMS());

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
            return await Task.FromResult(true);
        }
        public async static Task<bool> deletePool() {

            string spisok_id="";
            if(SearchDevice.spisok_id != null)
                spisok_id = SearchDevice.spisok_id;
            if (SearchDevice.currentSpisok[0] != null)
                spisok_id = SearchDevice.currentSpisok[0].Id;
            else
                return false;
            if (Connection.getConn() == null) return false;

            //1st: from pribor table
            string pr_query = "DELETE FROM pribor WHERE spisok_id = \'"+spisok_id+"\'";

            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(pr_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(pr_query, Connection.getConnMS());

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


            //2d: from spisok table
            string sp_query = "DELETE FROM spisok WHERE id = \'" + spisok_id + "\'";

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
                cmd = new NpgsqlCommand(sp_query, Connection.getConn());
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
                cmd = new SqlCommand(sp_query, Connection.getConnMS());

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
            return await Task.FromResult(true);
        }
    }
}
