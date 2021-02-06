using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.TeacherEnrollment.ViewModel
{
  public class TeacherEnrollmentVM
    {
        public int TeacherEnrollmentId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
