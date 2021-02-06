using CMS.Configuration;
using CMS.Data.Common;
using CMS.Repository;
using CMS.Services.StudentEnrollment.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.StudentEnrollment
{
    public class StudentEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentEnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<StudentEnrollmentVM>> GetAll()
        {
            var list =await _unitOfWork.Repository<CMS.Data.AppModel.StudentEnrollment>().GetAll().Include(x=>x.Students).Include(x=>x.Courses).Include(x=>x.Subjects).Where(x => x.Deleted == false)
                .Select(x=>new StudentEnrollmentVM { 
                   StudentEnrollmentId =x.StudentEnrollmentId,
                   StudentId = x.StudentId,
                   StudentName = x.Students.StudentName,
                   CourseId = x.CourseId,
                   CourseName = x.Courses.CourseName,
                   SubjectId =x.SubjectId,
                   SubjectName = x.Subjects.SubjectName,
                   GradeId = x.GradeId,
                   GradeName = x.Grades.GradeName
                })
                .OrderByDescending(x => x.StudentEnrollmentId).ToListAsync();
            return list;
        }

        public async Task<List<StudentEnrollmentVM>> GetStudentEnrollmentById(string searchBy)
        {

            var list = _unitOfWork.Repository<CMS.Data.AppModel.StudentEnrollment>().GetAll().Include(x => x.Students).Include(x => x.Courses).Include(x => x.Subjects).Where(x => x.Deleted == false)
               .Select(x => new StudentEnrollmentVM
               {
                   StudentEnrollmentId = x.StudentEnrollmentId,
                   StudentId = x.StudentId,
                   StudentName = x.Students.StudentName,
                   CourseId = x.CourseId,
                   CourseName = x.Courses.CourseName,
                   SubjectId = x.SubjectId,
                   SubjectName = x.Subjects.SubjectName,
                   GradeId = x.GradeId,
                   GradeName = x.Grades.GradeName
               })
               .OrderByDescending(x => x.StudentEnrollmentId).AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                list = list.Where(x => x.StudentName.ToLower().Contains(searchBy.ToLower()) || 
                x.CourseName.ToLower().Contains(searchBy.ToLower()) || 
                x.SubjectName.ToLower().Contains(searchBy.ToLower()) ||
                x.GradeName.ToLower().Contains(searchBy.ToLower())||
                x.SubjectId.ToString().Contains(searchBy) ||
                x.CourseId.ToString().Contains(searchBy) ||
                x.SubjectId.ToString().Contains(searchBy) ||
                x.GradeId.ToString().Contains(searchBy)).AsQueryable();
            }
            return await list.ToListAsync();
        }
        //public async Task<CMS.Data.AppModel.Student> GetById(int id)
        //{
        //    var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Student>().FindAsync(x => x.StudentId == id);
        //    return entity;
        //}

        public async Task<StudentEnrollmentVM> GetStudentEnrollmentById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.StudentEnrollment>().FindAsync(x => x.StudentEnrollmentId == id);
            if (entity != null)
            {
                var data = new StudentEnrollmentVM
                {
                    StudentEnrollmentId = entity.StudentEnrollmentId,
                    StudentId = entity.StudentId,
                    CourseId = entity.CourseId,
                    SubjectId = entity.SubjectId,
                    GradeId = entity.GradeId,
                };
                return data;
            }
            else
            {
                var data = new StudentEnrollmentVM();
                return data;
            }


        }

        //public CMS.Data.AppModel.Student GetByName(string name)
        //{
        //    return _unitOfWork.Repository<CMS.Data.AppModel.Student>().GetAll().Where(x => x.StudentName.Trim().Equals(name.Trim())).SingleOrDefault();
        //}
        public async Task<ResultModel> Insert(StudentEnrollmentVM model)
        {
            try
            {
                //if (GetByName(model.StudentName) != null)
                //    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.StudentName) };
                //if (GetByRegistrationNumber(model.RegistrationNo) != null)
                //    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.RegistrationNo) };

                var entity = new CMS.Data.AppModel.StudentEnrollment
                {
                   StudentId = model.StudentId,
                   CourseId = model.CourseId,
                   SubjectId = model.SubjectId,
                   GradeId = model.GradeId,
                   CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<CMS.Data.AppModel.StudentEnrollment>().InsertAsync(entity);

                return new ResultModel { Id = entity.StudentId.ToString(), Result = true, Message = NotificationMessageConfig.InsertSuccessMessage(model.StudentId.ToString()) };
            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.InsertErrorMessage(model.StudentId.ToString()) };
            }
        }

        public async Task<ResultModel> Update(StudentEnrollmentVM model)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CMS.Data.AppModel.StudentEnrollment>().FindAsync(x => x.StudentEnrollmentId == model.StudentEnrollmentId);
                entity.StudentId = model.StudentId;
                entity.CourseId = model.CourseId;
                entity.StudentId = model.StudentId;
                entity.GradeId = model.GradeId;
                entity.UpdateDate = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.StudentId.ToString(), Result = true, Message = NotificationMessageConfig.UpdateSuccessMessage(model.StudentId.ToString()) };
            }
            catch (Exception)
            {
                return new ResultModel { Id = model.StudentId.ToString(), Result = false, Message = NotificationMessageConfig.UpdateErrorMessage(model.StudentId.ToString()) };
            }
        }

        public async Task<ResultModel> Delete(int id)
        {
            var model = await _unitOfWork.Repository<CMS.Data.AppModel.StudentEnrollment>().FindAsync(x => x.StudentId == id);
            try
            {
                if (model != null)
                {
                    model.Deleted = true;
                    model.DeleteDate = DateTime.Now;
                    _unitOfWork.Commit();
                    return new ResultModel { Result = true, Message = NotificationMessageConfig.DeleteSuccessMessage(model.StudentId.ToString()), Id = model.StudentId.ToString() };
                }
                else
                {
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.NotFoundError };
                }

            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.DeleteErrorMessage(model.StudentId.ToString()) };
            }
        }

    }
}
