using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem_ASP.NET_Core_MVC.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Father Name is Required")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        public string Mobile { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Department")]
        public int DepID { get; set; }

        [NotMapped]
        public string Department { get; set; }
    }
}
