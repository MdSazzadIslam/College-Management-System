using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Grade.ViewModel
{
  public  class GradeVM
    {
        public int GradeId { get; set; }
        [Required(ErrorMessage = "Grade name is required", AllowEmptyStrings = false)]
        public string GradeName { get; set; }

        [Required(ErrorMessage = "Grade point required", AllowEmptyStrings = false)]
        public double GradePoint { get; set; }

        [Required(ErrorMessage = "Range From required", AllowEmptyStrings = false)]
        public int ScoreFrom { get; set; }

        [Required(ErrorMessage = "Range To required", AllowEmptyStrings = false)]
        public int ScoreTo { get; set; }
    }
}
