using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Subject.ViewModel
{
   public class SubjectListVM
    {
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public double Salary { get; set; }
        public int NoOfStudent { get; set; }
    }
}
