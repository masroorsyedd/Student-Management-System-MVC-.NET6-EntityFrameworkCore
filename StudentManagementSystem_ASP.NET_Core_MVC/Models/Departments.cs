using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem_ASP.NET_Core_MVC.Models
{
    //Class name should be Department but due to conflict with the DB Property Name "Department" I have to make the class name as Departments
    public class Departments
    {
        [Key]
        public int ID { get; set; }

        public string Department { get; set; }
    }
}
