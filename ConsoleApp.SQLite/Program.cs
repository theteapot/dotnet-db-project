using System;
using System.Collections.Generic;

namespace ConsoleApp.SQLite
{
    public class Program
    {
        public static void Main()
        {
            using (var db = new BloggingContext())
            {
                //Checking if blog to be added is in current db context using url
                Blog newBlog = new Blog { Url = "http://www.google.com/" };
                AddBlog(newBlog, db);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine("\n All blogs in database:");

                LinkedList<string> blogUrls = new LinkedList<string>();
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(" - {0} : {1}", blog.BlogId, blog.Url);
                }
            }
        }
   
        protected static void AddBlog(Blog newBlog, BloggingContext db) 
        {
            foreach (var blog in db.Blogs)
            {
                if (newBlog.Url == blog.Url) {
                    return;
                }
            }
            db.Blogs.Add(newBlog);
        }

        protected static void Deduplify(BloggingContext db) 
        {
            HashSet<string> urlHashSet = new HashSet<string>();
            foreach (var blog in db.Blogs)
            {
                if (!urlHashSet.Contains(blog.Url)) 
                {
                    urlHashSet.Add(blog.Url);
                } 
                else 
                {
                    db.Remove(blog);
                }
            }
        }
    }

}
