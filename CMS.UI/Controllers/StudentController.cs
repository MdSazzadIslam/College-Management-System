using CMS.Services.Student;
using CMS.Services.Student.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class StudentController : BaseController
    {
        private readonly StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: Student
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Student";
            ViewBag.ActionName = "Student List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }


        public ActionResult GetStudentDetails()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Student";
            ViewBag.ActionName = "Student Details";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }
        public async Task<JsonResult> GetStudentDetailsList()
        {
            var list = await _studentService.GetStudentDetails();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //GET Employee/GetEmployee
        public async Task<JsonResult>  GetStudent()
        {
            var list = await _studentService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentById(string searchBy)
        {
            var list = _studentService.GetStudentById(searchBy).ToList();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getStudentToUpdate(int id)
        {
            var list = await _studentService.GetById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
            var model = new StudentVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Student";
            ViewBag.ActionName = "Add New Student";
            return View("NewOrEdit", model);
        }


        /// <summary>  
        /// Insert new Student  
        /// </summary>  
        /// <param name="Student"></param>  
        /// <returns></returns>  
        public async Task<JsonResult> Insert(StudentVM Student)
        {
            if (Student != null)
            {
                if (Student.StudentId == 0)
                {
                    var model = await _studentService.Insert(Student);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else
                {
                    var model = await _studentService.Update(Student);
                    return Json(new { Result = model.Result, Message = model.Message });
                }

            }
            else
            {
                return Json(new { Result = false, Message = "No record found to save!!!" });
            }
        }

        public ActionResult Edit(int id)
        {

            var model = new StudentVM();
            model.StudentId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Student";
            ViewBag.ActionName = "Edit Student";
            return View("NewOrEdit", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var response = await _studentService.Delete(id);
            return Json(new { Result = response.Result, Message = response.Message, Id = response.Id });
        }
    }

}