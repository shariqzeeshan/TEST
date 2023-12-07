using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Api
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);
        Task<bool> MyCustomRepositoryMethodExampleAsync();
    }
}
