using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPattern
{
    /// <summary>
    /// 抽象工厂工厂类是接口或抽象，这里用简单工厂代替(n个工厂 m种产品)
    /// </summary>
    public class DbConnectionFactory
    {
        public static string _connectionString;
        public static string _dataBaseType;

        static DbConnectionFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            _dataBaseType = ConfigurationManager.AppSettings["database"];
        }

        public static IDbConnection CreateDbConnection()
        {
            IDbConnection connection = null;
            switch (_dataBaseType)
            {
                case "sqlserver":
                    connection = new SqlConnection(_connectionString);
                    break;
                case "mysql":
                    connection = new MySqlConnection(_connectionString);
                    break;
            }
            return connection;
        }
    }
}
