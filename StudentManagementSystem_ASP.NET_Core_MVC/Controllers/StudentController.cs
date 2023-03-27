using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem_ASP.NET_Core_MVC.Models;

namespace StudentManagementSystem_ASP.NET_Core_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _Db;
        public StudentController(StudentContext Db)
        {
            _Db = Db;
        }
        public IActionResult StudentList()
        {
            try
            {
                var stdList = from a in _Db.tbl_Student
                              join b in _Db.tbl_Departments
                              on a.DepID equals b.ID
                              into Dep //give alias to table b (departments)
                              from b in Dep.DefaultIfEmpty()

                              select new Student
                              {
                                  ID = a.ID,
                                  Name = a.Name,
                                  FName = a.FName,
                                  Email = a.Email,
                                  Mobile = a.Mobile,
                                  Description = a.Description,
                                  Department = b == null ? "" : b.Department
                              };

                return View(stdList);
            }
            catch (Exception ex)
            {
                return View(); ;
            }

        }
        public IActionResult Create(Student student)
        {
            LoadDepartments();
            return View(student);
        }

        //When we are making a POST Call then we have to declare the method as async
        //and the call that is used to modified the DB will be written with await
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            try
            {
                //if any value in the model is null then it will return false thats why I used 'not' operator
                if (!ModelState.IsValid)
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach(ModelError error in allErrors)
                    {
                        Console.WriteLine(allErrors);
                    }
                    //insert logic
                    if (student.ID == 0)
                    {
                        _Db.tbl_Student.Add(student);
                        await _Db.SaveChangesAsync();
                    }
                    //update logic
                    else
                    {
                        _Db.Entry(student).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();
                    }
                    return RedirectToAction("StudentList");
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("StudentList");
            }
           
        }

        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var std = await _Db.tbl_Student.FindAsync(id);
                if(std!=null)
                {
                    _Db.tbl_Student.Remove(std);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("StudentList");
            }
            catch (Exception ex)
            {
                return RedirectToAction("StudentList");
            }

        }

        private void LoadDepartments()
        {
            try
            {
                List<Departments> depList = new List<Departments>();
                depList = _Db.tbl_Departments.ToList();
                depList.Insert(0, new Departments { ID = 0, Department = "Please Select" });

                //The ViewBag is used to pass data from the controller to the view.
                ViewBag.DepList = depList;  
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
