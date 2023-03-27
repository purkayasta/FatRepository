# WIP FatRepository

An Handful predefined methods that every efcore projects need. 

Tasks:
- [x] Basic Add Update Methods
- [x] Simple Find Methods (Sync and Async)
- [x] Includable Queries (Sync and Async)
- [x] Selectable Queries (Sync and Async)
- [x] Create Contract for the use
- [x] Extension for service collection
- [x] Create Factories for console applications/webapi

-------------------------------------------------------------------------

Two ways to use it. 
Microsoft DI:
```c#
services.AddFatRepository();
```


Factory:
```
var dbContext = new YourDbContext();
IFatRepository repo = FatFactoryInstaller.CreateFatRepository<Blog, YourDbContext>(dbContext);
IFatUnitOfWork unitOfWork = FatFactoryInstaller.CreateUnitOfWork<YourDbContext>(dbContext);
```

Now you can use this in your application service layer.


```c#
public class BlogService 
{
	private readony IFatRepository<Blog, YourDbContext> _repository;
	private readonly IFatUnitOfWork<YourDbContext _unitOfWork;

	public BlogService(IFatRepository<Blog, YourDbContext> repo, IFatUnitOfWork<YourDbContext> unitOfWork) 
	{
		_repository = repo;
		_unitOfWork = unitOfWork;
	}

	public void Add(Blog b) 
	{
		_repository.Add(b);
		_unitOfWork.Commit();
	}

}
```



Made with C# ❤