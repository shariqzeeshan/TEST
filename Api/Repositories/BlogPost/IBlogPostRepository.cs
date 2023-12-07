using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Api.Repositories.BlogPost;
using DataAccessLayer;

namespace Api
{
    public interface IBlogPostRepository : IGenericRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetDataSet(string commandText, CommandType commandType, IDbDataParameter[] parameters = null);

        Task<List<BlogPost>> GetBlogPostsBySP(DbConnector _dbConnector);
    }
}
