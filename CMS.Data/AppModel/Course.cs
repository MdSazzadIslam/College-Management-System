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
    [Table("Course")]
    public class Course:BaseModel
    {
        public Course()
        {
           
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course code is required", AllowEmptyStrings = false)]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Course name is required", AllowEmptyStrings = false)]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course credit is required", AllowEmptyStrings = false)]
        public double CourseCredit { get; set; }

        public virtual ICollection<StudentEnrollment> StudentEnrollments { get; set; }
        public virtual ICollection<TeacherEnrollment> TeacherEnrollments { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

    }
}
