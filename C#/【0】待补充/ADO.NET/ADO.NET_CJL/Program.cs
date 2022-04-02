using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace ADO.NET_CJL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        //mysql
        void Demo1()
        {
            string connectString = "";
            MySqlConnection mySqlConnection = new MySqlConnection(connectString);
            if (mySqlConnection.State != ConnectionState.Open)
            {
                mySqlConnection.Open();
            }
            string sql = "";
            //查询
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sql, mySqlConnection);
            DataTable dt=new DataTable();
            mySqlDataAdapter.Fill(dt);
            //非查询
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
        }
    }
}
