using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using N5User.Data.Context;
using N5User.Data.Dtos;
using N5User.Data.Models;
using Nest;

namespace N5User.Data.Repositories;

public class PermissionRepository:IPermissionRepository
{
    private readonly DataContext _dataContext;

    public PermissionRepository(DataContext dataContext )
    {
        _dataContext = dataContext;
    }
    
    public async Task<Permission> GetPermisionById(int id)
    {
        return await _dataContext.Permissions.Where(x => x.Id == id).Include(s=>s.PermissionType).FirstOrDefaultAsync();
    }

    public async Task<List<PermissionDto>> GetPermissionsAsync()
    {
        List<PermissionDto> permissionsDto = new List<PermissionDto>();
        var permissions = await _dataContext.Permissions.Include(s => s.PermissionType).ToListAsync();

        foreach (var permission in permissions)
        {
            PermissionTypeDto permTypeDto = new PermissionTypeDto(permission.PermissionType.Description);
            
            PermissionDto permDto = new PermissionDto
            (permission.EmployeeForename,
                permission.EmployeeSurname,
                permission.PermissionTypeId,
                permission.PermissionDate,
                permTypeDto);
            permissionsDto.Add(permDto);
        }
        return permissionsDto;
    }


    public async Task<Permission> RequestPermissionAsync(Permission permission)
    {
        
        var result = _dataContext.Permissions.Add(permission);
        return result.Entity;
    }

    public async Task<int> ModifyPermissionAsync(int id, Permission permission)
    {
        var a = _dataContext.Permissions.Update(permission);
        return a.Entity.Id;
 
    }

}