using System;
using System.Collections.Generic;
using Xunit;

namespace ConsoleApp.SQLite.Tests
{
    public class ConsoleAppSQLite_Blogs
    {
        [Fact]
        public void AddingBlogsAndPosts()
        {
            using (var db = new BloggingContext())
            {   
                List<Post> postList = new List<Post>();
                Post newPost = new Post {Title = "title", Content = "Content"};
                postList.Add(newPost);
                Blog newBlog = new Blog { Url = "www.notRealUrl.com", Posts = postList };
                db.Blogs.Add(newBlog);
                Assert.Equal(db.SaveChanges(), 2);

                //See if adding 

                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(" - {0} : {1}", blog.BlogId, blog.Url);
                }

                foreach (var post in db.Posts)
                {
                    Console.WriteLine(" - {0} : {1} : {2} : {3} : {4}", post.BlogId, post.Blog, post.PostId, post.Title, post.Content);
                }

                //Remove test entries
                db.Blogs.Remove(newBlog);
                Assert.Equal(db.SaveChanges(), 2);
                //db.Remove(newBlog);
            }
        }
    }
}
