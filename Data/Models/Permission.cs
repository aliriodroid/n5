using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace N5User.Data.Models;

public class Permission
{
    [Comment("Unique Id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public  int Id { get; set; }
    
    [Comment("Employee Forename")]
    public required string EmployeeForename { get; set; }
    
    [Comment("Employee Surename")]
    public required string EmployeeSurname { get; set; }
    
    public  PermissionType PermissionType { get; set; }
    
    [Comment("Permission Type")]
    public required int PermissionTypeId { get; set; }
    
    [Comment("Permission granted on date")]
    public required DateTime PermissionDate { get; set; }
    
   
}