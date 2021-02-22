using CMS.Data.AppModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class DBlnitializer : DropCreateDatabaseAlways <AppContext>  
    {
        protected override void Seed(AppContext context)
        {

            IList<Course> courses = new List<Course>();
            IList<Subject> subjects = new List<Subject>();
            IList<Grade> grades = new List<Grade>();
            IList<Student> students = new List<Student>();
            IList<Teacher> teachers = new List<Teacher>();
            IList<TeacherEnrollment> teacherEnrollments = new List<TeacherEnrollment>();
            IList<StudentEnrollment> studentEnrollments = new List<StudentEnrollment>();

            if(!context.Courses.Any())
            {
                courses.Add(new Course() { CourseId = 501, CourseCode = "501", CourseName = "CSE", CourseCredit = 150 });
                courses.Add(new Course() { CourseId = 502, CourseCode = "502", CourseName = "BBA", CourseCredit = 147 });
                courses.Add(new Course() { CourseId = 503, CourseCode = "503", CourseName = "EEE", CourseCredit = 147 });
                courses.Add(new Course() { CourseId = 504, CourseCode = "504", CourseName = "LMS", CourseCredit = 147 });
                courses.Add(new Course() { CourseId = 505, CourseCode = "505", CourseName = "SE", CourseCredit = 147 });
                context.Courses.AddRange(courses);
            }



            if (!context.Subjects.Any())
            {
                subjects.Add(new Subject() { SubjectId = 201, SubjectCode = "DS-101", SubjectName = "Data Structure", CourseId = 501, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 202, SubjectCode = "ALG-102", SubjectName = "Algorithm", CourseId = 501, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 203, SubjectCode = "BBA-101", SubjectName = "English", CourseId = 502, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 204, SubjectCode = "BBA-102", SubjectName = "Computer Fundamental", CourseId = 502, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 205, SubjectCode = "EEE-101", SubjectName = "C++", CourseId = 503, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 206, SubjectCode = "EEE-102", SubjectName = "Java", CourseId = 503, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 207, SubjectCode = "LMS-101", SubjectName = "Computer Fundamental", CourseId = 504, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 208, SubjectCode = "LMS-102", SubjectName = "Math", CourseId = 504, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 209, SubjectCode = "SE-101", SubjectName = "Machine Learning", CourseId = 505, SubjectCredit = 3 });
                subjects.Add(new Subject() { SubjectId = 210, SubjectCode = "SE-102", SubjectName = "Databse", CourseId = 505, SubjectCredit = 3 });
                context.Subjects.AddRange(subjects);
            }

            if (!context.Grades.Any())
            {
                grades.Add(new Grade() { GradeId = 111, GradeName = "A+", GradePoint = 4.00, ScoreFrom = 80, ScoreTo = 100 });
                grades.Add(new Grade() { GradeId = 112, GradeName = "A", GradePoint = 3.75, ScoreFrom = 75, ScoreTo = 79 });
                grades.Add(new Grade() { GradeId = 113, GradeName = "A-", GradePoint = 3.50, ScoreFrom = 70, ScoreTo = 74 });
                grades.Add(new Grade() { GradeId = 114, GradeName = "B+", GradePoint = 3.25, ScoreFrom = 65, ScoreTo = 69 });
                grades.Add(new Grade()
                {
                    GradeId = 115,
                    GradeName = "B",
                    GradePoint = 3.25,
                    ScoreFrom = 60,
                    ScoreTo = 64
                });
                grades.Add(new Grade() { GradeId = 117, GradeName = "B-", GradePoint = 2.75, ScoreFrom = 55, ScoreTo = 59 });
                grades.Add(new Grade() { GradeId = 118, GradeName = "C+", GradePoint = 2.50, ScoreFrom = 50, ScoreTo = 54 });
                grades.Add(new Grade() { GradeId = 119, GradeName = "C", GradePoint = 2.25, ScoreFrom = 45, ScoreTo = 49 });
                grades.Add(new Grade() { GradeId = 120, GradeName = "D", GradePoint = 2.00, ScoreFrom = 40, ScoreTo = 45 });
                grades.Add(new Grade() { GradeId = 121, GradeName = "F", GradePoint = 2.75, ScoreFrom = 0, ScoreTo = 39 });
                context.Grades.AddRange(grades);
            }

            if (!context.Students.Any())
            {

                students.Add(new Student() { StudentId = 10001, StudentName = "A", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101010" });
                students.Add(new Student() { StudentId = 10002, StudentName = "B", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101011" });
                students.Add(new Student() { StudentId = 10003, StudentName = "C", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101012" });
                students.Add(new Student() { StudentId = 10004, StudentName = "D", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101013" });
                students.Add(new Student() { StudentId = 10005, StudentName = "E", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101014" });
                students.Add(new Student() { StudentId = 10006, StudentName = "F", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101015" });
                students.Add(new Student() { StudentId = 10007, StudentName = "G", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), RegistrationNo = "1101016" });
                context.Students.AddRange(students);
            }

            if (!context.Teachers.Any())
            {

                teachers.Add(new Teacher() { TeacherId = 10001, TeacherName = "AAA", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), Salary = 1111111 });
                teachers.Add(new Teacher() { TeacherId = 10002, TeacherName = "BBB", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), Salary = 1111112 });
                teachers.Add(new Teacher() { TeacherId = 10003, TeacherName = "CCC", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), Salary = 1111113 });
                teachers.Add(new Teacher() { TeacherId = 10004, TeacherName = "DDD", BirthDate = DateTime.Now.ToString("dd/mm/yyyy"), Salary = 1111114 });
                context.Teachers.AddRange(teachers);
            }

            if (!context.TeacherEnrollments.Any())
            {
                teacherEnrollments.Add(new TeacherEnrollment() { TeacherEnrollmentId = 1001, TeacherId = 10001, CourseId = 501, SubjectId = 201 });
                teacherEnrollments.Add(new TeacherEnrollment() { TeacherEnrollmentId = 1001, TeacherId = 10001, CourseId = 501, SubjectId = 202 });
                context.TeacherEnrollments.AddRange(teacherEnrollments);
            }

            if (!context.StudentEnrollments.Any())
            {
                studentEnrollments.Add(new StudentEnrollment() { StudentId = 10001, CourseId = 501, SubjectId = 201, GradeId = 111 });
                studentEnrollments.Add(new StudentEnrollment() { StudentId = 10002, CourseId = 502, SubjectId = 202, GradeId = 112 });
                context.StudentEnrollments.AddRange(studentEnrollments);
            }
            

            base.Seed(context);


            


        }
    }

}
