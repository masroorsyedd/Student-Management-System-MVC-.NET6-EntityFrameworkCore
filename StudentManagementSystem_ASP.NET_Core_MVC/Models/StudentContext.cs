using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem_ASP.NET_Core_MVC.Models
{
    public class StudentContext : DbContext
    {
        //Dependency Injecction of DB Connection
        public StudentContext(DbContextOptions<StudentContext> options):base(options)
        {

        }

        public DbSet<Student> tbl_Student { get; set; }
        public DbSet<Departments> tbl_Departments { get; set; }
    }
}
