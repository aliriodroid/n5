using Microsoft.EntityFrameworkCore;
using N5User.Data.Models;

namespace N5User.Data.Context;

public class DataContext:DbContext
{
        public DataContext(){}

    public DataContext(DbContextOptions<DataContext> options ){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
        
    public DbSet<PermissionType> PermissionTypes {get;set;}
    public DbSet<Permission> Permissions {get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        
        modelBuilder.Entity<PermissionType>().HasData(
            new PermissionType()
            {
                Id = 1,
                Description = "Manager"
            },
            new PermissionType()
            {
                Id = 2,
                Description = "Admin",
            },
            new PermissionType()
            {
                Id = 3,
                Description = "Sales",
            }
        );
        modelBuilder.Entity<Permission>().HasData(
            
                new Permission()
                {
                    Id = 1,
                    EmployeeForename = "Michael",
                    EmployeeSurname = "Scott",
                    PermissionTypeId = 1,
                    PermissionDate = DateTime.Now,
                },
                new Permission()
                {
                    Id = 2,
                    EmployeeForename = "Pam",
                    EmployeeSurname = "Beasly",
                    PermissionTypeId = 2,
                    PermissionDate = DateTime.Now,
                },
                new Permission()
                {
                    Id = 3,
                    EmployeeForename = "Jim",
                    EmployeeSurname = "Halpert",
                    PermissionTypeId = 2,
                    PermissionDate = DateTime.Now,
                }
                
        );
    }
        
    
}