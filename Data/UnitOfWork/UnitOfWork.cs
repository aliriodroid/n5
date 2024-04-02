using N5User.Data.Context;
using N5User.Data.Repositories;

namespace N5User.Data.UnitOfWork;

public class UnitOfWork:IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IPermissionRepository PermissionRepository { get; }

    public UnitOfWork(DataContext dataContext, IPermissionRepository permissionRepository)
    {
        _dataContext = dataContext;
        PermissionRepository = permissionRepository;
    }
    
    public async Task<int> Save()
    {
        return await _dataContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dataContext.Dispose();
    }
    
}