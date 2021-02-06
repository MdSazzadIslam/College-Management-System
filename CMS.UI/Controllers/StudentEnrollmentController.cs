using CMS.Services.StudentEnrollment;
using CMS.Services.StudentEnrollment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class StudentEnrollmentController : BaseController
    {
        private readonly StudentEnrollmentService _studentEnrollmentService;
        public StudentEnrollmentController(StudentEnrollmentService studentEnrollmentService)
        {
            _studentEnrollmentService = studentEnrollmentService;
        }
        // GET: StudentEnrollment
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Student Enrollment";
            ViewBag.ActionName = "Student Enrollment List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }
        public async Task<JsonResult>  GetStudentEnrollment()
        {
            var list =await _studentEnrollmentService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult>  GetStudentEnrollmentById(string searchBy)
        {
            var list =await _studentEnrollmentService.GetStudentEnrollmentById(searchBy);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetStudentEnrollmentToUpdate(int id)
        {
            var list = await _studentEnrollmentService.GetStudentEnrollmentById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var model = new StudentEnrollmentVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "StudentEnrollment";
            ViewBag.ActionName = "New";
            return View("NewOrEdit", model);
        }
        public ActionResult Edit(int id)
        {

            var model = new StudentEnrollmentVM();
            model.StudentEnrollmentId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "StudentEnrollment";
            ViewBag.ActionName = "Edit";
            return View("NewOrEdit", model);
        }
        public async Task<JsonResult> Insert(StudentEnrollmentVM data)
        {
            if (data != null)
            {
                if (data.StudentEnrollmentId == 0)
                {
                    var model = await _studentEnrollmentService.Insert(data);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else
                {
                    var model = await _studentEnrollmentService.Update(data);
                    return Json(new { Result = model.Result, Message = model.Message });
                }

            }
            else
            {
                return Json(new { Result = false, Message = "No record found to save!!!" });
            }
        }
    }
}