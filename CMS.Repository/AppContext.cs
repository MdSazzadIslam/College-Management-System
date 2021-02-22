using CMS.Data.AppModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class AppContext : DbContext
    {

        public AppContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
           Database.SetInitializer<AppContext>(new DBlnitializer());
             
        }
        public AppContext(string name)
            : base(name)
        {

            //delete previous db
            //new database for every migration

            Database.Delete();
            Database.CreateIfNotExists();
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public virtual DbSet<TeacherEnrollment> TeacherEnrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
            .HasRequired<Course>(s => s.Courses)
            .WithMany(x => x.Subjects).HasForeignKey(x => x.CourseId)
            .WillCascadeOnDelete(false);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }

       



    }

}
