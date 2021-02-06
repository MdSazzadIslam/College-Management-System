using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Student.ViewModel
{
  public  class StudentDetailsVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RegistrationNo { get; set; }
        public string CourseName { get; set; }
        public string SubjectName { get; set;}
        public string SubjectCode { get; set; }
        public string Grade { get; set; }
    }
}
