# WIP FatRepository

An Handful predefined methods that every project needs. 

Tasks:
- [x] Basic Add Update Methods
- [x] Simple Find Methods (Sync and Async)
- [x] Includable Queries (Sync and Async)
- [x] Selectable Queries (Sync and Async)
- [x] Create Contract for the use
- [x] Extension for service collection
- [x] Create Factories for console applications
- [ ] Create tests for all the methods

-------------------------------------------------------------------------

Two ways to use it. 
Microsoft DI:
```c#
services.AddFatRepository();
```


Factory:
```
IFatRepository repo = FatFactoryInstaller.CreateFatRepository<Blog>(context);
IFatUnitOfWork unitOfWork = FatFactoryInstaller.CreateUnitOfWork<Blog>(context);
```

Now you can use this in your application service layer.


```c#
public class BlogService 
{
	private readony IFatRepository<Blog> _repository;
	private readonly IFatUnitOfWork _unitOfWork;

	public BlogService(IFatRepository<Blog> repo, IFatUnitOfWork unitOfWork) 
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