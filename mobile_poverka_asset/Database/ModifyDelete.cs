using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobile_poverka_asset.Database
{
    class ModifyDelete
    {
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

            NpgsqlCommand cmd = new NpgsqlCommand(pr_query, Connection.getConn());

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
            cmd = new NpgsqlCommand(sp_query, Connection.getConn());

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
