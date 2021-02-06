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
    [Table("StudentEnrollment")]
  public  class StudentEnrollment :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentEnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int GradeId { get; set; }

        public virtual Student Students { get; set; }
        public virtual Course Courses { get; set; }
        public virtual Subject Subjects { get; set; }
        public virtual Grade Grades { get; set; }
    }
    public class StudentEnrollmentMap : EntityTypeConfiguration<StudentEnrollment>
    {
        public StudentEnrollmentMap()
        {
            this.HasRequired(x => x.Students).WithMany(x => x.StudentEnrollments).HasForeignKey(x => x.StudentId);
            this.HasRequired(x => x.Courses).WithMany(x => x.StudentEnrollments).HasForeignKey(x => x.CourseId);
            this.HasRequired(x => x.Subjects).WithMany(x => x.StudentEnrollments).HasForeignKey(x => x.SubjectId).WillCascadeOnDelete(false);
            this.HasRequired(x => x.Grades).WithMany(x => x.StudentEnrollments).HasForeignKey(x => x.GradeId);  
        }

    }
}
