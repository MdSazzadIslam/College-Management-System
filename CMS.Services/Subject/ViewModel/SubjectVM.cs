using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Subject.ViewModel
{
   public class SubjectVM
    {
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Subject code is required", AllowEmptyStrings = false)]
        public string SubjectCode { get; set; }

        [Required(ErrorMessage = "Subject name is required", AllowEmptyStrings = false)]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Credit is required", AllowEmptyStrings = false)]
        public double SubjectCredit { get; set; }

        [Required(ErrorMessage = "Course Name is required", AllowEmptyStrings = false)]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
