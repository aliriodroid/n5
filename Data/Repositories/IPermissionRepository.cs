using N5User.Data.Dtos;
using N5User.Data.Models;

namespace N5User.Data.Repositories;

public interface IPermissionRepository
{
    public Task<List<PermissionDto>> GetPermissionsAsync();
    public Task<Permission> GetPermisionById(int id);
    public Task<Permission> RequestPermissionAsync(Permission permission);
    public Task<int> ModifyPermissionAsync(int id, Permission permission);
}