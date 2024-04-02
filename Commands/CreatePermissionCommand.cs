using N5User.Data.Models;
using MediatR;
using System.Numerics;
using N5User.Data.Dtos;

namespace N5User.Commands;

public class CreatePermissionCommand : IRequest<Permission>
{
    public string EmployeeForename { get; set; }
    public string EmployeeSurname { get; set; }
    public  int PermisitonTypeId { get; set; }
    public  PermissionTypeDto PermisitonTypeDto { get; set; }
    public  DateTime PermissionDate { get; set; }
    

    public CreatePermissionCommand(PermissionDto permissionDto)
    {
        EmployeeForename = permissionDto.employeeForename;
        EmployeeSurname = permissionDto.employeeSurname;
        PermisitonTypeDto = permissionDto.permissionType;
        PermisitonTypeId = permissionDto.permissionTypeId;
        PermissionDate = permissionDto.permissionDate;
    }
    
}