using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.StudentEnrollment.ViewModel
{
  public  class StudentEnrollmentVM
    {
        public int StudentEnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}
