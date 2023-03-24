// See https://aka.ms/new-console-template for more information
using FatRepository.SQLServer;
using Microsoft.EntityFrameworkCore;


var context = new BloggingContext();

var repo = new FatRepository<Blog>(context);

var val = repo.Find(x => x.Name.Equals("asd"), nameof(Blog.Name));

var val2 = repo.Find<BlogSummary>(x => x.Name.Equals("pritom"), x=> new { x.Name, x.PostCount});

var blogSummaries = context.Blogs
    .Select(blog => new BlogSummary
    {
        Name = blog.Name,
        PostCount = blog.Posts.Count
    })
    .ToList();




public class BlogSummary
{
    public string Name { get; set; }
    public int PostCount { get; set; }
}

public class Blog
{
    public string Name { get; set; }
    public int PostCount { get; set; }
}

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
}