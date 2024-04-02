using N5User.Data.Models;

namespace N5User.Data.Dtos;

public record PermissionDto(string employeeForename,string employeeSurname,int permissionTypeId,DateTime permissionDate,PermissionTypeDto permissionType);