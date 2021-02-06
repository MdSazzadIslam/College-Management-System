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
    [Table("TeacherEnrollment")]
  public class TeacherEnrollment:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherEnrollmentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public virtual Teacher Teachers { get; set; }
        public virtual Course Courses { get; set; }
        public virtual Subject Subjects { get; set; }

    }
    public class TeacherEnrollmentMap : EntityTypeConfiguration<TeacherEnrollment>
    {
        public TeacherEnrollmentMap()
        {
            this.HasRequired(x => x.Teachers).WithMany(x => x.TeacherEnrollments).HasForeignKey(x => x.SubjectId);
            this.HasRequired(x => x.Courses).WithMany(x => x.TeacherEnrollments).HasForeignKey(x => x.CourseId);
            this.HasRequired(x => x.Subjects).WithMany(x => x.TeacherEnrollments).HasForeignKey(x => x.SubjectId).WillCascadeOnDelete(false);
        }

    }
}
