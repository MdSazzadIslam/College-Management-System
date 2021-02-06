using CMS.Services.Subject;
using CMS.Services.Subject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class SubjectController : BaseController
    {
        private readonly SubjectService _subjectService;
        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        // GET: Subject
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Subject";
            ViewBag.ActionName = "Subject List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }

        //GET Employee/GetEmployee
        public async Task<JsonResult> GetSubject()
        {
            var list = await _subjectService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetSubjectById(string searchBy)
        {
            var list = await _subjectService.GetSubjectById(searchBy);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubjectDetails()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Subject";
            ViewBag.ActionName = "Subject Details";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }
        public async Task<JsonResult> GetSubjectDetailsList()
        {
            var list = await _subjectService.GetSubjectDetails();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult>  GetSubjectToUpdate(int id)
        {
            var list =await _subjectService.GetSubjectById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectByCourseId(int id)
        {
            var list = _subjectService.GetSubjectByCourseId(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var model = new SubjectVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Subject";
            ViewBag.ActionName = "Add New Subject";
            return View("NewOrEdit", model);
        }


        /// <summary>  
        /// Insert new Subject  
        /// </summary>  
        /// <param name="Subject"></param>  
        /// <returns></returns>  
        public async Task<JsonResult> Insert(SubjectVM Subject)
        {
            if (Subject != null)
            {
                if (Subject.SubjectId == 0) {
                    var model = await _subjectService.Insert(Subject);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else {
                    var model = await _subjectService.Update(Subject);
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

            var model =new SubjectVM();
            model.SubjectId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Subject";
            ViewBag.ActionName = "Edit Subject";
            return View("NewOrEdit", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var response = await _subjectService.Delete(id);
            return Json(new { Result = response.Result, Message = response.Message, Id = response.Id });
        }


    }
}