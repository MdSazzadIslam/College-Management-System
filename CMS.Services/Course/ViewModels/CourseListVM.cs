using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Course.ViewModels
{
   public class CourseListVM
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int NoOfTeacher { get; set; }
        public int NoOfStudent { get; set; }
        public double Grade { get; set; }
    }
}
