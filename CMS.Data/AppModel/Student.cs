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
    [Table("Student")]
    public class Student:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Student name is required", AllowEmptyStrings = false)]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Birthdate is required", AllowEmptyStrings = false)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Registration is required", AllowEmptyStrings = false)]
        public string RegistrationNo { get; set; }
        public virtual ICollection<StudentEnrollment> StudentEnrollments { get; set; }

    }

  
}
