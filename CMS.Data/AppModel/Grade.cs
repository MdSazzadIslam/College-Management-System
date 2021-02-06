using CMS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.AppModel
{
    [Table("Grade")]
    public class Grade : BaseModel
    {
        public Grade()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }
        [Required(ErrorMessage = "Grade name is required", AllowEmptyStrings = false)]
        public string GradeName { get; set; }

        [Required(ErrorMessage = "Grade point required", AllowEmptyStrings = false)]
        public double GradePoint { get; set; }

        [Required(ErrorMessage = "Range from required", AllowEmptyStrings = false)]
        public int ScoreFrom { get; set; }

        [Required(ErrorMessage = "Range to required", AllowEmptyStrings = false)]
        public int ScoreTo { get; set; }

       
        public virtual ICollection<StudentEnrollment> StudentEnrollments { get; set; }

    }
}
