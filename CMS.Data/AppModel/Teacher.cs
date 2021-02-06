using CMS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.AppModel
{
    [Table("Teacher")]
    public class Teacher:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Teacher name is required", AllowEmptyStrings = false)]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "Birthdate is required", AllowEmptyStrings = false)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Salary is required", AllowEmptyStrings = false)]
        public double Salary { get; set; }
        public virtual ICollection<TeacherEnrollment> TeacherEnrollments { get; set; }
    }
   

}
