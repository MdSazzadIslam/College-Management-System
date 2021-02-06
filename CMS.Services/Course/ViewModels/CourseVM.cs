using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Course.ViewModels
{
  public  class CourseVM
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course code is required", AllowEmptyStrings = false)]
        public string CourseCode { get; set; }
        [Required(ErrorMessage = "Course name is required", AllowEmptyStrings = false)]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course credit is required", AllowEmptyStrings = false)]
        public double CourseCredit { get; set; }
    }
}
