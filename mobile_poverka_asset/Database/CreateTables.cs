using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace mobile_poverka_asset.Database
{
    class CreateTables
    {
        public static void Create()
        {
            if (!((Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open) ||
                (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open))) return;


            //Check if tables exist
            string query_sp = "CREATE TABLE spisok( "+
                              "id INT IDENTITY(1, 1) PRIMARY KEY," +
                              "name TEXT NOT NULL," +
                              "date DATE NOT NULL," +
                              "count INTEGER NOT NULL," +
                              "complete BOOLEAN," +
                              "comment text);";

            string query_pr = "CREATE TABLE pribor( "+
                              "id INT IDENTITY(1, 1) PRIMARY KEY," +
                              "serial TEXT NOT NULL," +
                              "idchannel TEXT NOT NULL," +
                              "spisok_id INTEGER NOT NULL," +
                              "FOREIGN KEY(spisok_id) REFERENCES spisok(id));";

            if (Connection.getConn() != null && Connection.getConn().State == ConnectionState.Open)
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query_sp, Connection.getConn());
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
                cmd = new NpgsqlCommand(query_pr, Connection.getConn());
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

            }
            else if (Connection.getConnMS() != null && Connection.getConnMS().State == ConnectionState.Open)
            {
                query_sp = "CREATE TABLE spisok( " +
                              "id INT IDENTITY(1, 1) PRIMARY KEY," +
                              "name TEXT NOT NULL," +
                              "date DATE NOT NULL," +
                              "count INTEGER NOT NULL," +
                              "complete BIT," +
                              "comment text);";
                SqlCommand cmd = new SqlCommand(query_sp, Connection.getConnMS());
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
                cmd = new SqlCommand(query_pr, Connection.getConnMS());
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
            }
        }
    }
}
