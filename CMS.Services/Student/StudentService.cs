using CMS.Configuration;
using CMS.Data.Common;
using CMS.Repository;
using CMS.Services.Student.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Student
{
    public class StudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CMS.Repository.AppContext _appContext;
        public StudentService(IUnitOfWork unitOfWork, CMS.Repository.AppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
        }
        public async Task<List<StudentVM>> GetAll()
        {
            var list =await _unitOfWork.Repository<CMS.Data.AppModel.Student>().GetAll().Where(x => x.Deleted == false)
                .Select(x=>new StudentVM { 
                    StudentId =x.StudentId,
                    StudentName = x.StudentName,
                    BirthDate = x.BirthDate,
                    RegistrationNo = x.RegistrationNo
                })
                .OrderByDescending(x => x.StudentId).ToListAsync();
            return list;

            //var list = _unitOfWork.Repository<CMS.Data.AppModel.Student>().GetAll().Where(x => x.Deleted == false).OrderByDescending(x => x.StudentId).AsQueryable();
            //return list;
        }

        public async Task<List<StudentDetailsVM>> GetStudentDetails()
        {
            var list = await (from s in _appContext.Students
                              join se in _appContext.StudentEnrollments on s.StudentId equals se.StudentId into ste
                              from sted in ste.DefaultIfEmpty()
                              join sub in _appContext.Subjects on sted.SubjectId equals sub.SubjectId into su
                              from subj in su.DefaultIfEmpty()
                              join c in _appContext.Courses on subj.CourseId equals c.CourseId
                              join g in _appContext.Grades on sted.GradeId equals g.GradeId into gr
                              from grd in gr.DefaultIfEmpty()
                              where s.Deleted == false
                              select new StudentDetailsVM
                              {
                                  StudentId = s.StudentId,
                                  StudentName = s.StudentName,
                                  RegistrationNo = s.RegistrationNo,
                                  CourseName = c.CourseName,
                                  SubjectName = subj.SubjectName,
                                  SubjectCode = subj.SubjectCode,
                                  Grade = grd.GradeName

                              }).ToListAsync();

            return list;
        }

        public IQueryable<CMS.Data.AppModel.Student> GetStudentById(string searchBy)
        {

            var list = _unitOfWork.Repository<CMS.Data.AppModel.Student>().GetAll().Where(x => x.Deleted == false).OrderByDescending(x => x.StudentId).AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                list = list.Where(x => x.StudentName.ToLower().Contains(searchBy.ToLower()) || x.RegistrationNo.ToLower().Contains(searchBy.ToLower())).AsQueryable();
            }
            return list;
        }
        public async Task<CMS.Data.AppModel.Student> GetById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Student>().FindAsync(x => x.StudentId == id);
            return entity;
        }
        public async Task<StudentVM> GetStudentById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Student>().FindAsync(x => x.StudentId == id);
            if (entity != null)
            {
                var data = new StudentVM
                {
                    StudentId = entity.StudentId,
                    StudentName = entity.StudentName,
                    BirthDate= entity.BirthDate,
                    RegistrationNo = entity.RegistrationNo
                };
                return data;
            }
            else
            {
                var data = new StudentVM();
                return data;
            }


        }

        public CMS.Data.AppModel.Student GetByName(string name)
        {
            return _unitOfWork.Repository<CMS.Data.AppModel.Student>().GetAll().Where(x => x.StudentName.Trim().Equals(name.Trim())).SingleOrDefault();
        }
        public CMS.Data.AppModel.Student GetByRegistrationNumber(string name)
        {
            return _unitOfWork.Repository<CMS.Data.AppModel.Student>().GetAll().Where(x => x.RegistrationNo.Trim().Equals(name.Trim())).SingleOrDefault();
        }
        public async Task<ResultModel> Insert(StudentVM model)
        {
            try
            {
                if (GetByName(model.StudentName) != null)
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.StudentName) };
                if (GetByRegistrationNumber(model.RegistrationNo) != null)
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.RegistrationNo) };

                var entity = new CMS.Data.AppModel.Student
                {
                    RegistrationNo = model.RegistrationNo,
                    StudentName = model.StudentName,
                    BirthDate = model.BirthDate,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<CMS.Data.AppModel.Student>().InsertAsync(entity);

                return new ResultModel { Id = entity.StudentId.ToString(), Result = true, Message = NotificationMessageConfig.InsertSuccessMessage(model.StudentName) };
            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.InsertErrorMessage(model.StudentName) };
            }
        }

        public async Task<ResultModel> Update(StudentVM model)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Student>().FindAsync(x => x.StudentId == model.StudentId);
                entity.StudentName = model.StudentName;
                entity.BirthDate = model.BirthDate;
                entity.RegistrationNo = model.RegistrationNo;
                entity.UpdateDate = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.StudentId.ToString(), Result = true, Message = NotificationMessageConfig.UpdateSuccessMessage(model.StudentName) };
            }
            catch (Exception)
            {
                return new ResultModel { Id = model.StudentId.ToString(), Result = false, Message = NotificationMessageConfig.UpdateErrorMessage(model.StudentName) };
            }
        }

        public async Task<ResultModel> Delete(int id)
        {
            var model = await _unitOfWork.Repository<CMS.Data.AppModel.Student>().FindAsync(x => x.StudentId == id);
            try
            {
                if (model != null)
                {
                    model.Deleted = true;
                    model.DeleteDate = DateTime.Now;
                    _unitOfWork.Commit();
                    return new ResultModel { Result = true, Message = NotificationMessageConfig.DeleteSuccessMessage(model.StudentName), Id = model.StudentId.ToString() };
                }
                else
                {
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.NotFoundError };
                }

            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.DeleteErrorMessage(model.StudentName) };
            }
        }

    }
}
