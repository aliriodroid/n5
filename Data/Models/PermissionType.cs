using Microsoft.EntityFrameworkCore;

namespace N5User.Data.Models;

public class PermissionType
{
    [Comment("Unique Id")]
    public int Id { get; set; }
    
    [Comment("Permission description")]
    public string Description { get; set; }
    
    public List<Permission> Permissions { get; set; }
}