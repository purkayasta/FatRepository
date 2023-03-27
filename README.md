# FatRepository For EFCore
## Give it a star if you like the project. 👏 🌠 🌟

FatRepository is an Handful predefined methods that every efcore projects need. 

![Nuget](https://img.shields.io/nuget/v/FatRepository)
![Nuget](https://img.shields.io/nuget/dt/FatRepository?style=plastic)
![Nuget](https://img.shields.io/github/repo-size/purkayasta/FatRepository?style=social)
![Nuget](https://img.shields.io/github/last-commit/purkayasta/FatRepository?style=flat-square)

[Nuget](https://www.nuget.org/packages/FatRepository/)

## Usage:
### Required:
> Have to configure DbContext first then register the service.


-------------------------------------------------------------------------
### Service Registration:

With Microsoft DI:
```c#
services.AddFatRepository();
```

With Factory:
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