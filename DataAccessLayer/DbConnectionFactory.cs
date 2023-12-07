using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    class DbConnectionFactory
    {
        public static IDbConnection GetDbConnection(string connectionString)
        {
            IDbConnection connection = null;
            connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
