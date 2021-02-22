using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Teacher.ViewModel
{
  public  class TeacherVM
    {
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Teacher name is required", AllowEmptyStrings = false)]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "Birthdate is required", AllowEmptyStrings = false)]
        public string BirthDate { get; set; }


        //[JsonIgnore]
        //public DateTime? BirthDate { get; set; }
        //[JsonProperty("BirthDate")]
        //public string DateAsString
        //{
        //    get
        //    {
        //        return BirthDate != null ? BirthDate.Value.ToString("dd/MM/yyyy") : null;
        //    }
        //    set
        //    {
        //        BirthDate = string.IsNullOrEmpty(value) ? default(DateTime?) : DateTime.ParseExact(value, "dd/MM/yyyy", null);
        //    }
        //}

        [Required(ErrorMessage = "Salary is required", AllowEmptyStrings = false)]
        public double Salary { get; set; }

    }
}
