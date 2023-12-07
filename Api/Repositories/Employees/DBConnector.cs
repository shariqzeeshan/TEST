// Data/DbConnector.cs
using System;
using System.Data.SqlClient;

namespace Api.Repositories.Employees
{
public class DbConnector : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection _connection;

    public DbConnector(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new SqlConnection(_connectionString);
        _connection.Open();
    }

    public SqlConnection Connection => _connection;

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}

}
