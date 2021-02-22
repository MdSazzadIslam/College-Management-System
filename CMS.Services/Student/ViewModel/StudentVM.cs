using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Student.ViewModel
{
  public  class StudentVM
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student name is required", AllowEmptyStrings = false)]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Birthdate is required", AllowEmptyStrings = false)]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Registration is required", AllowEmptyStrings = false)]
        public string RegistrationNo { get; set; }
       

    }
}
