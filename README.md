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

### What you get:
You will get two interface to intract with.
```
1. IFatRepository<Entity,DbContext>();
2. IFatUnitOfWork<DbContext>();
```

```IFatRepository``` will give you some handful methods to access to operate. But If you need the <b>full control</b> over your dbset class then ```GetEntity()``` method is there to accomodate your likings.

```IFatUnitOfWork``` will give you all the control related dbcontext class. It is nothing but a wrapper around the ```DbContext.Database``` instance. You can access SaveChanges and Transaction from there.

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
		_repository.InsertOne(b);
		_unitOfWork.Commit();
	}

}
```


Made with C# ❤