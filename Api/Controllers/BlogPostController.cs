using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Api.Repositories.BlogPost;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private IBlogPostRepository _blogPostRepo;
        private readonly DbConnector _dbConnector;

        public BlogPostController(IBlogPostRepository blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
            _dbConnector = new DbConnector(Startup.Configuration.GetConnectionString("Default"));
        }

        /// Postman GET action (using SELECT statement)
        /// https://localhost:44382/api/blogPost
        [HttpGet]
        public async Task<string> Get()
        {
            var blogPosts = await _blogPostRepo.GetAllAsync();
            return Newtonsoft.Json.JsonConvert.SerializeObject(blogPosts);
        }

        ///Postman GET action (using Stored Proc)
        ///https://localhost:44382/api/blogPost/getallbysp
        [HttpGet("GetAllBySP")]
        public async Task<IEnumerable<BlogPost>> GetAllBySP()
        {
            var blogPosts = await _blogPostRepo.GetBlogPostsBySP(_dbConnector);
            return blogPosts;
        }



        [HttpGet("{id}")]
        public async Task<string> GetById(int Id)
        {
            var post = await _blogPostRepo.FindAsync(Id);
            return Newtonsoft.Json.JsonConvert.SerializeObject(post);
        }

        /* POSTMAN Payload
         * {
            "Title":"Blog Insert",
            "Contents":"<p>This is for Insert testing</p>",
            "Timestamp":"2023-01-01",
            "Category":{"CategoryId":2,"Name":"General"}
        }
         * */
        [HttpPost]
        public IActionResult Create(BlogPost blogPost)
        {
            _blogPostRepo.InsertAsync(blogPost);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogPost blogPost)
        {
            _blogPostRepo.UpdateAsync(blogPost);
            return Ok();
        }

        ///Postman Delete action
        ///https://localhost:44382/api/blogPost/4
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blogPostRepo.DeleteAsync(id);
            return Ok();
        }
    }
}