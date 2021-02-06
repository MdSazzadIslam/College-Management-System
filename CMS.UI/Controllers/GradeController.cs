using CMS.Services.Grade;
using CMS.Services.Grade.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CMS.UI.Controllers
{
    public class GradeController : BaseController
    {
        private readonly GradeService _gradeService;
        public GradeController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }
        // GET: Grade
        public ActionResult Index()
        {
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Grade";
            ViewBag.ActionName = "Grade List";
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }

        public async Task<JsonResult>  GetGrade()
        {
            var list =await _gradeService.GetAll();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetGradeById(string searchBy)
        {
            var list = _gradeService.GetGradeById(searchBy);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list.ToList(), JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> getGradeToUpdate(int id)
        {
            var list = await _gradeService.GetById(id);
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {
            var model = new GradeVM();
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Grade";
            ViewBag.ActionName = "Add New Grade";
            return View("NewOrEdit", model);
        }


        /// <summary>  
        /// Insert new Grade  
        /// </summary>  
        /// <param name="Grade"></param>  
        /// <returns></returns>  
        public async Task<JsonResult> Insert(GradeVM Grade)
        {
            if (Grade != null)
            {
                if (Grade.GradeId == 0) {
                    var model = await _gradeService.Insert(Grade);
                    return Json(new { Result = model.Result, Message = model.Message });
                }
                else {
                    var model = await _gradeService.Update(Grade);
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

            var model = new GradeVM();
            model.GradeId = id;
            ViewBag.PageUrl = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.MenuName = "Settings";
            ViewBag.Title = "Grade";
            ViewBag.ActionName = "Edit Grade";
            return View("NewOrEdit", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var response = await _gradeService.Delete(id);
            return Json(new { Result = response.Result, Message = response.Message, Id = response.Id });
        }
    }

}