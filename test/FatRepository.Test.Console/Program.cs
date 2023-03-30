// See https://aka.ms/new-console-template for more information

using FatRepository.Installer;
using Microsoft.EntityFrameworkCore;


var context = new BloggingContext();

var repo = FatFactoryInstaller.CreateFatRepository<Blog, BloggingContext>(context);
var unitOfWork = FatFactoryInstaller.CreateUnitOfWork(context);

var val = repo.Find(x => x.Name!.Equals("asd"), includableMembers: new string[] { nameof(Blog.Name) });

var val2 = repo.Find(x => x.Name == "pritom", x => new
{
    x.Name,
    Id = x.PostCount
});

var blogSummaries = context.Blogs!
    .Select(blog => new BlogSummary
    {
        Name = blog.Name,
        PostCount = blog.PostCount
    })
    .ToList();

unitOfWork.Commit();




public class BlogSummary
{
    public string? Name { get; set; }
    public int PostCount { get; set; }
}

public class Blog
{
    public string? Name { get; set; }
    public int PostCount { get; set; }
}

public class BloggingContext : DbContext
{
    public DbSet<Blog>? Blogs { get; set; }
}