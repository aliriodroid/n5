using N5User.Data.Models;
using MediatR;
using System.Numerics;
using N5User.Data.Dtos;

namespace N5User.Commands;

public class UpdatePermissionCommand : IRequest<int>
{
    public int Id { get; set; }
    public string EmployeeForename { get; set; }
    public string EmployeeSurname { get; set; }
    public  int PermisitonTypeId { get; set; }
    public  DateTime PermissionDate { get; set; }
    

    public UpdatePermissionCommand(int id,PermissionDto permissionDto)
    {
        Id = id;
        EmployeeForename = permissionDto.employeeForename;
        EmployeeSurname = permissionDto.employeeSurname;
        PermisitonTypeId = permissionDto.permissionTypeId;
        PermissionDate = permissionDto.permissionDate;
    }
    
}