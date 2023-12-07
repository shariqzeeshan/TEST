using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Api.Repositories.BlogPost;
using Dapper;
using DataAccessLayer;

namespace Api
{
    public class BlogPostRepository : SqlRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(string connectionString) : base(connectionString) { }

        public override async void DeleteAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "DELETE FROM BlogPost WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                await conn.QueryFirstOrDefaultAsync<BlogPost>(sql, parameters);
            }
        }

        public override async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT b.Id, b.Title, b.Contents, b.CategoryId, c.[Name] FROM blogpost b INNER JOIN CATEGORY c ON c.CategoryId = b.CategoryId";
                return await conn.QueryAsync<BlogPost, Category, BlogPost>(sql, (b,c) =>
                {
                    b.Category = c;
                    return b;
                }, splitOn: "CategoryId");
            }
        }

        public override async Task<BlogPost> FindAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "SELECT * FROM blogpost WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, System.Data.DbType.Int32);
                return await conn.QueryFirstOrDefaultAsync<BlogPost>(sql, parameters);
            }
        }

        public override async void InsertAsync(BlogPost entity)
        {
            using (var conn = GetOpenConnection())
            {
                var sql = "INSERT INTO blogpost(Title, Contents, Timestamp, CategoryId) "
                    + "VALUES(@Title, @Contents, @Timestamp, @CategoryId)";

                var parameters = new DynamicParameters();
                parameters.Add("@Title", entity.Title, System.Data.DbType.String);
                parameters.Add("@Contents", entity.Contents, System.Data.DbType.String);
                parameters.Add("@Timestamp", entity.Timestamp, System.Data.DbType.DateTime);
                parameters.Add("@CategoryId", entity.Category.CategoryId, System.Data.DbType.Int64);

                await conn.QueryAsync(sql, parameters);
            }
        }

        public override async void UpdateAsync(BlogPost entityToUpdate)
        {
            using (var conn = GetOpenConnection())
            {
                var existingEntity = await FindAsync(entityToUpdate.Id);

                var sql = "UPDATE BlogPost "
                    + "SET ";

                var parameters = new DynamicParameters();
                if (existingEntity.Title != entityToUpdate.Title)
                {
                    sql += "Title=@Title,";
                    parameters.Add("@Title", entityToUpdate.Title, DbType.String);
                }

                sql = sql.TrimEnd(',');

                sql += " WHERE Id=@Id";
                parameters.Add("@Id", entityToUpdate.Id, DbType.Int32);

                await conn.QueryAsync(sql, parameters);
            }
        }

        async Task<List<BlogPost>> IBlogPostRepository.GetBlogPostsBySP(DbConnector _dbConnector)
        {
            var blogPosts = new List<BlogPost>();

            using (var command = new SqlCommand("GetBlogPosts", _dbConnector.Connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Category category = new Category { CategoryId = Convert.ToInt16(reader["CategoryId"]), Name = Convert.ToString(reader["Name"]) };
                        BlogPost blogPost = new BlogPost
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = Convert.ToString(reader["Title"]),
                            Contents = Convert.ToString(reader["Contents"]),
                            Category = category
                        };
                        blogPosts.Add(blogPost);
                    }
                }
            }

            return blogPosts;
        }


    }
}
