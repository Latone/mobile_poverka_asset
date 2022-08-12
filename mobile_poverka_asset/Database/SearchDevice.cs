using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobile_poverka_asset.Database
{
    class SearchDevice
    {
        public static string spisok_id;
        public static string item_id;
        public static List<Spisok> currentSpisok;
        public static List<Item> currentItem;
        public static List<Spisok> GetSearchResultsSpisok(string query_pretext,string numOfRows)
        {
            if (Connection.getConn() == null ||
                Connection.getConn().State == ConnectionState.Closed) return new List<Spisok>();


            string query="";
            if (numOfRows == "-1" && query_pretext!="") //Get all results without limit when searching
                query = "SELECT * FROM spisok WHERE name LIKE \'%" + query_pretext + "%\' ORDER BY id DESC;";
            else //Limit results (even at the end of the serach (empty string))
                query = "SELECT * FROM spisok WHERE name LIKE \'%" + query_pretext + "%\' ORDER BY id DESC LIMIT "+numOfRows+";";

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connection.getConn());

            NpgsqlDataReader reader = cmd.ExecuteReader();

            //reader.Read();
            List<Spisok> list = new List<Spisok>();
            //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            while (reader.Read())
            {
                //for (int i = 0; i < columns.Count; i++) {
                list.Add(new Spisok
                {
                    Id = reader.GetValue(0).ToString(),
                    Name = reader.GetValue(1).ToString(),
                    Date = reader.GetValue(2).ToString(),
                    Count = reader.GetValue(3).ToString(),
                    Complete = reader.GetValue(4).ToString(),
                    Comment = reader.GetValue(5).ToString()
                });
                // }
            }


            reader.Close();

            return list;
        }
        // select count(*) from spisok where name =
        public static int GetLastSpisoksID()
        {
            if (Connection.getConn() == null ||
                Connection.getConn().State == ConnectionState.Closed) return -1;
            string sp_query = "select distinct id from spisok order by id desc limit 1;";
            int endValue = -1;
            NpgsqlCommand cmd = new NpgsqlCommand(sp_query, Connection.getConn());

            try
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                while (reader.Read())
                {
                    endValue = Int32.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }

            return endValue;
        }
        public static int GetNumberOfTodaysSpisok(string toLookFor) {
            if (Connection.getConn() == null ||
                Connection.getConn().State == ConnectionState.Closed) return -1;

            string sp_query = "select count(*) from spisok where name like \'"+toLookFor+"%\';";
            int endValue = -1;
            NpgsqlCommand cmd = new NpgsqlCommand(sp_query, Connection.getConn());

            try
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                while (reader.Read())
                {
                    endValue = Int32.Parse(reader.GetValue(0).ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                //smth
                Console.WriteLine("Error Content Page -<-" + ex.Message);

            }

            return endValue;
        }
        public static List<Item> GetSearchResultsPribor(string query_pretext, string spisok_id)
        {
            //SearchDevice.spisok_id = spisok_id;
            if (spisok_id == null && currentSpisok!=null)
                spisok_id = currentSpisok[0].Id;

            if (Connection.getConn() == null) return new List<Item>();

            string query = "SELECT * FROM pribor WHERE serial LIKE \'%"+query_pretext+"%\' AND spisok_id="+spisok_id; 

            NpgsqlCommand cmd = new NpgsqlCommand(query, Connection.getConn());

            NpgsqlDataReader reader = cmd.ExecuteReader();

            //reader.Read();
            List<Item> list = new List<Item>();
            //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
            while (reader.Read())
            {
                //for (int i = 0; i < columns.Count; i++) {
                    list.Add(new Item{ Id = reader.GetValue(0).ToString(),
                                       Serial = reader.GetValue(1).ToString(),
                                       idchannel = reader.GetValue(2).ToString(),
                                       spisok_id = reader.GetValue(3).ToString()
                    });
               // }
            }


            reader.Close();

            return list;
        }
    }
}
