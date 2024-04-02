using N5User.Data.Repositories;

namespace N5User.Data.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
   public Task<int> Save();
   public IPermissionRepository PermissionRepository { get; }
}