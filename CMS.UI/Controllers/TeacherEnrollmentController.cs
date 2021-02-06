using CMS.Services.TeacherEnrollment;
using CMS.Services.TeacherEnrollment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class TeacherEnrollmentController : BaseController
    {
        private readonly TeacherEnrollmentService _TeacherEnrollmentService;
        public TeacherEnrollmentController(TeacherEnrollmentService TeacherEnrollmentService)
        {
            _TeacherEnrollmentService = TeacherEnrollmentService;
        }
        // GET: TeacherEnrollment
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Teacher Enrollment";
            ViewBag.ActionName = "Teacher Enrollment List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }
        public async Task<JsonResult> GetTeacherEnrollment()
        {
            var list = await _TeacherEnrollmentService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetTeacherEnrollmentById(string searchBy)
        {
            var list = await _TeacherEnrollmentService.GetTeacherEnrollmentById(searchBy);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetTeacherEnrollmentToUpdate(int id)
        {
            var list = await _TeacherEnrollmentService.GetTeacherEnrollmentById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var model = new TeacherEnrollmentVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "TeacherEnrollment";
            ViewBag.ActionName = "New";
            return View("NewOrEdit", model);
        }
        public ActionResult Edit(int id)
        {

            var model = new TeacherEnrollmentVM();
            model.TeacherEnrollmentId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "TeacherEnrollment";
            ViewBag.ActionName = "Edit";
            return View("NewOrEdit", model);
        }
        public async Task<JsonResult> Insert(TeacherEnrollmentVM data)
        {
            if (data != null)
            {
                if (data.TeacherEnrollmentId == 0)
                {
                    var model = await _TeacherEnrollmentService.Insert(data);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else
                {
                    var model = await _TeacherEnrollmentService.Update(data);
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