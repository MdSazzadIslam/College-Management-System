using CMS.Services.Teacher;
using CMS.Services.Teacher.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly TeacherService _teacherService;
        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        // GET: Teacher
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Teacher";
            ViewBag.ActionName = "Teacher List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }

        //GET Employee/GetEmployee
        public async Task<JsonResult>  GetTeacher()
        {
            var list = await _teacherService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult>  GetTeacherById(string searchBy)
        {
            var list = await _teacherService.GetTeacherById(searchBy);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getTeacherToUpdate(int id)
        {
            var list = await _teacherService.GetById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
            var model = new TeacherVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Teacher";
            ViewBag.ActionName = "Add New Teacher";
            return View("NewOrEdit", model);
        }


        /// <summary>  
        /// Insert new Teacher  
        /// </summary>  
        /// <param name="Teacher"></param>  
        /// <returns></returns>  
        public async Task<JsonResult> Insert(TeacherVM Teacher)
        {
            if (Teacher != null)
            {
                if (Teacher.TeacherId == 0)
                {
                    var model = await _teacherService.Insert(Teacher);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else
                {
                    var model = await _teacherService.Update(Teacher);
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

            var model = new TeacherVM();
            model.TeacherId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Teacher";
            ViewBag.ActionName = "Edit Teacher";
            return View("NewOrEdit", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var response = await _teacherService.Delete(id);
            return Json(new { Result = response.Result, Message = response.Message, Id = response.Id });
        }
    }

}