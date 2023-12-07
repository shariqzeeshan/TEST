using System;

namespace Api
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Contents { get; set; }

        public DateTime Timestamp { get; set; }

        public Category Category { get; set; }

    }
}
