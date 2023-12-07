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
    }
}
