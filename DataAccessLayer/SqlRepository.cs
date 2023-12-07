using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// The concrete implementation of a SQL repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class SqlRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            return DbConnectionFactory.GetDbConnection(_connectionString);
        }

        public abstract void DeleteAsync(int id);
        public abstract Task<IEnumerable<TEntity>> GetAllAsync();
        public abstract Task<TEntity> FindAsync(int id);
        public abstract void InsertAsync(TEntity entity);
        public abstract void UpdateAsync(TEntity entityToUpdate);

        public Task<IEnumerable<TEntity>> GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        {
            throw new System.NotImplementedException();
        }

        //public override Task<IEnumerable<Employee>> GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null)
        //{
        //    using (var connection = GetOpenConnection())
        //    {
        //        connection.Open();

        //        //var categories = connection.Query<Employee>("GetEmployees", null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        var categories = connection.Query<Employee>(commandText, null, commandType: CommandType.StoredProcedure);
        //        return (Task<IEnumerable<Employee>>)categories;

        //        ////using (var command = database.CreateCommand(commandText, commandType, connection))
        //        //using (var command = new SqlCommand(commandText, connection))
        //        //{
        //        //    if (parameters != null)
        //        //    {
        //        //        foreach (var parameter in parameters)
        //        //        {
        //        //            command.Parameters.Add(parameter);
        //        //        }
        //        //    }

        //        //    var dataset = new DataSet();
        //        //    //var dataAdaper = database.CreateAdapter(command);
        //        //    var dataAdaper = new SqlDataAdapter(command);
        //        //    dataAdaper.Fill(dataset);

        //        //    return dataset;
        //        //}
        //    }
        //}

        //public abstract Task<IEnumerable<TEntity>> GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);


    }
}
