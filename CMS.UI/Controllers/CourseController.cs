
using CMS.Services.Course;
using CMS.Services.Course.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class CourseController : BaseController
    {
        private readonly CourseService _courseService;
        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: Course
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Course";
            ViewBag.ActionName = "Course List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }

        //GET Employee/GetEmployee
        public async Task<JsonResult> GetCourse()
        {
            var list = await _courseService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourseDetails()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Course";
            ViewBag.ActionName = "Course Details";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }

        public async Task<JsonResult> GetCourseDetailsList()
        {
            var list = await _courseService.GetCourseDetails();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(string searchBy)
        {
            var list = _courseService.GetCourseById(searchBy);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> getCourseToUpdate(int id)
        {
            var list = await _courseService.GetById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult New()
        {
            var model = new CourseVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Course";
            ViewBag.ActionName = "Add New Course";
            return View("NewOrEdit", model);
        }


        /// <summary>  
        /// Insert new course  
        /// </summary>  
        /// <param name="Course"></param>  
        /// <returns></returns>  
        public async Task<JsonResult> Insert(CourseVM course)
        {
            if (course != null)
            {
                if (course.CourseId == 0) {
                    var model = await _courseService.Insert(course);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else {
                    var model = await _courseService.Update(course);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
               
            }
            else
            {
                return Json(new { Result = false, Message = "No record found to save!!!"});
            }
        }

        public ActionResult Edit(int id)
        {

            var model = new CourseVM();
            model.CourseId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Course";
            ViewBag.ActionName = "Edit Course";
            return View("NewOrEdit", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var response = await _courseService.Delete(id);
            return Json(new { Result = response.Result, Message = response.Message, Id = response.Id });
        }
    }
}