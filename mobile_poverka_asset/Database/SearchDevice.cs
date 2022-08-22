using mobile_poverka_asset.Models;
using mobile_poverka_asset.ViewModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;

namespace mobile_poverka_asset.Database
{
    class SearchDevice
    {
        public static string spisok_id;
        public static string item_id;
        public static string numofSpisokTable = "";
        public static string numofPriborTable = "";
        public static List<Spisok> currentSpisok;
        public static List<Item> currentItem;
        public static List<Spisok> GetSearchResultsSpisok(string query_pretext,string numOfRows)
        {
            if (!((Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open) ||
                (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open))) return new List<Spisok>();


            //Check if tables exist
            string table_query_spisok = "";
            string table_query_pribor = "";
            
            table_query_spisok = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = \'spisok\';";
            table_query_pribor = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = \'pribor\';";

            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                cmd = new NpgsqlCommand(table_query_spisok, Connection.getConn());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                numofSpisokTable = reader.GetValue(0).ToString();
                reader.Close();

                cmd = new NpgsqlCommand(table_query_pribor, Connection.getConn());
                reader = cmd.ExecuteReader();

                reader.Read();
                numofPriborTable = reader.GetValue(0).ToString();
                reader.Close();
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                cmd = new SqlCommand(table_query_spisok, Connection.getConnMS());
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                numofSpisokTable = reader.GetValue(0).ToString();
                reader.Close();

                cmd = new SqlCommand(table_query_pribor, Connection.getConnMS());
                reader = cmd.ExecuteReader();

                reader.Read();
                numofPriborTable = reader.GetValue(0).ToString();
                reader.Close();
            }


            //
            string query="";
            if (numOfRows == "-1" && query_pretext!="") //Get all results without limit when searching
                query = "SELECT * FROM spisok WHERE name LIKE \'%" + query_pretext + "%\' ORDER BY id DESC;";
            else //Limit results (even at the end of the serach (empty string))
                query = "SELECT * FROM spisok WHERE name LIKE \'%" + query_pretext + "%\' ORDER BY id DESC LIMIT "+numOfRows+";";

           
            List<Spisok> list = new List<Spisok>();

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open
                && numofPriborTable!="0" && numofSpisokTable!="0")
            {
                cmd = new NpgsqlCommand(query, Connection.getConn());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                //reader.Read();
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
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open
                && numofPriborTable != "0" && numofSpisokTable != "0")
            {
                if (numOfRows == "-1" && query_pretext != "") //Get all results without limit when searching
                    query = "SELECT * FROM spisok WHERE name LIKE \'%" + query_pretext + "%\' ORDER BY id DESC;";
                else //Limit results (even at the end of the serach (empty string))
                    query = "SELECT TOP("+numOfRows+") * FROM spisok WHERE name LIKE \'%" + query_pretext + "%\' ORDER BY id DESC;";


                cmd = new SqlCommand(query, Connection.getConnMS());
                SqlDataReader reader = cmd.ExecuteReader();

                //reader.Read();
                
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
            }

            

            return list;
        }
        // select count(*) from spisok where name =
        public static bool CheckForTables() {
            //Check if tables exist
            string table_query_spisok = "";
            string table_query_pribor = "";

            table_query_spisok = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = \'spisok\';";
            table_query_pribor = "SELECT count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = \'pribor\';";

            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                cmd = new NpgsqlCommand(table_query_spisok, Connection.getConn());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                numofSpisokTable = reader.GetValue(0).ToString();
                reader.Close();

                cmd = new NpgsqlCommand(table_query_pribor, Connection.getConn());
                reader = cmd.ExecuteReader();

                reader.Read();
                numofPriborTable = reader.GetValue(0).ToString();
                reader.Close();
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                cmd = new SqlCommand(table_query_spisok, Connection.getConnMS());
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                numofSpisokTable = reader.GetValue(0).ToString();
                reader.Close();

                cmd = new SqlCommand(table_query_pribor, Connection.getConnMS());
                reader = cmd.ExecuteReader();

                reader.Read();
                numofPriborTable = reader.GetValue(0).ToString();
                reader.Close();
            }
            if (numofSpisokTable == "0" || numofPriborTable == "0" ||
                numofSpisokTable == "" || numofPriborTable == "") {
                return false;
                numofPriborTable = "";
                numofSpisokTable = "";
            }
            numofPriborTable = "";
            numofSpisokTable = "";
            return true;

        }
        public static int GetLastSpisoksID()
        {
            if (!((Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open) ||
                (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open))) return -1;
            string sp_query = "select distinct id from spisok order by id desc limit 1;";
            int endValue = -1;

            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                cmd = new NpgsqlCommand(sp_query, Connection.getConn());
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
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                cmd = new SqlCommand(sp_query, Connection.getConnMS());
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

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
            }

            return endValue;
        }
        public static int GetNumberOfTodaysSpisok(string toLookFor) {
            if (!((Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open) ||
                (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open))) return -1;

            string sp_query = "select count(*) from spisok where name like \'"+toLookFor+"%\';";
            int endValue = -1;

            dynamic cmd;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                
                try
                {
                    cmd = new NpgsqlCommand(sp_query, Connection.getConn());
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
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                cmd = new SqlCommand(sp_query, Connection.getConnMS());
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

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
            }
            
            return endValue;
        }
        public static List<Item> GetSearchResultsPribor(string query_pretext, string spisok_id)
        {
            //SearchDevice.spisok_id = spisok_id;
            if (spisok_id == null && currentSpisok!=null)
                spisok_id = currentSpisok[0].Id;

            if (Connection.getConn() == null && Connection.getConnMS() == null &&
                Connection.getConn().State == ConnectionState.Closed &&
                Connection.getConnMS().State == ConnectionState.Closed) return new List<Item>();

            string query = "SELECT * FROM pribor WHERE serial LIKE \'%"+query_pretext+"%\' AND spisok_id="+spisok_id;
            
            List<Item> list = new List<Item>();
            var cmd = (dynamic)null;

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                cmd = new NpgsqlCommand(query, Connection.getConn());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                //reader.Read();
                //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                while (reader.Read())
                {
                    //for (int i = 0; i < columns.Count; i++) {
                    list.Add(new Item
                    {
                        Id = reader.GetValue(0).ToString(),
                        Serial = reader.GetValue(1).ToString(),
                        idchannel = reader.GetValue(2).ToString(),
                        spisok_id = reader.GetValue(3).ToString()
                    });
                    // }
                }


                reader.Close();
            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                cmd = new SqlCommand(query, Connection.getConnMS());
                SqlDataReader reader = cmd.ExecuteReader();

                //reader.Read();
                
                //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                while (reader.Read())
                {
                    //for (int i = 0; i < columns.Count; i++) {
                    list.Add(new Item
                    {
                        Id = reader.GetValue(0).ToString(),
                        Serial = reader.GetValue(1).ToString(),
                        idchannel = reader.GetValue(2).ToString(),
                        spisok_id = reader.GetValue(3).ToString()
                    });
                    // }
                }


                reader.Close();
            }

            return list;
        }
    }
}
