using CMS.Configuration;
using CMS.Data.Common;
using CMS.Repository;
using CMS.Services.TeacherEnrollment.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.TeacherEnrollment
{
    public class TeacherEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CMS.Repository.AppContext _appContext;
        public TeacherEnrollmentService(IUnitOfWork unitOfWork, CMS.Repository.AppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
        }
        public async Task<List<TeacherEnrollmentVM>> GetAll()
        {
            var list = await (from te in _appContext.TeacherEnrollments
                        join tea in _appContext.Teachers on te.TeacherId equals tea.TeacherId
                        join c in _appContext.Courses on te.CourseId equals c.CourseId
                        join s in _appContext.Subjects on te.SubjectId equals s.SubjectId
                        where te.Deleted == false
                        orderby te.TeacherEnrollmentId descending
                        select new TeacherEnrollmentVM
                        {
                            TeacherEnrollmentId = te.TeacherEnrollmentId,
                            TeacherId = te.TeacherId,
                            TeacherName = tea.TeacherName,
                            CourseId = te.CourseId,
                            CourseName = c.CourseName,
                            SubjectId = te.SubjectId,
                            SubjectName = s.SubjectName
                        }).ToListAsync();

            return list;
        }

        public async Task<List<TeacherEnrollmentVM>> GetTeacherEnrollmentById(string searchBy)
        {


            var list = (from te in _appContext.TeacherEnrollments
                              join tea in _appContext.Teachers on te.TeacherId equals tea.TeacherId
                              join c in _appContext.Courses on te.CourseId equals c.CourseId
                              join s in _appContext.Subjects on te.SubjectId equals s.SubjectId
                              where te.Deleted == false
                              orderby te.TeacherEnrollmentId descending
                              select new TeacherEnrollmentVM
                              {
                                  TeacherEnrollmentId = te.TeacherEnrollmentId,
                                  TeacherId = te.TeacherId,
                                  TeacherName = tea.TeacherName,
                                  CourseId = te.CourseId,
                                  CourseName = c.CourseName,
                                  SubjectId = te.SubjectId,
                                  SubjectName = s.SubjectName
                              }).AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                list = list.Where(x => x.TeacherName.ToLower().Contains(searchBy.ToLower()) ||
                x.CourseName.ToLower().Contains(searchBy.ToLower()) ||
                x.SubjectName.ToLower().Contains(searchBy.ToLower()) ||
                x.SubjectId.ToString().Contains(searchBy) ||
                x.CourseId.ToString().Contains(searchBy) ||
                x.SubjectId.ToString().Contains(searchBy)).AsQueryable();
            }
            return await list.ToListAsync();
        }
        //public async Task<CMS.Data.AppModel.Teacher> GetById(int id)
        //{
        //    var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().FindAsync(x => x.TeacherId == id);
        //    return entity;
        //}

        public async Task<TeacherEnrollmentVM> GetTeacherEnrollmentById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.TeacherEnrollment>().FindAsync(x => x.TeacherEnrollmentId == id);
            if (entity != null)
            {
                var data = new TeacherEnrollmentVM
                {
                    TeacherEnrollmentId = entity.TeacherEnrollmentId,
                    TeacherId = entity.TeacherId,
                    CourseId = entity.CourseId,
                    SubjectId = entity.SubjectId
                };
                return data;
            }
            else
            {
                var data = new TeacherEnrollmentVM();
                return data;
            }


        }
        public async Task<ResultModel> Insert(TeacherEnrollmentVM model)
        {
            try
            {
                var entity = new CMS.Data.AppModel.TeacherEnrollment
                {
                    TeacherId = model.TeacherId,
                    CourseId = model.CourseId,
                    SubjectId = model.SubjectId,
                    CreateDate = DateTime.Now
                };

                 _unitOfWork.Repository<CMS.Data.AppModel.TeacherEnrollment>().Insert(entity);
                 await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.TeacherId.ToString(), Result = true, Message = NotificationMessageConfig.InsertSuccessMessage(model.TeacherId.ToString()) };
            }
            catch (Exception e)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.InsertErrorMessage(model.TeacherId.ToString()) };
            }
        }

        public async Task<ResultModel> Update(TeacherEnrollmentVM model)
        {
            try
            {
                var entity = await _unitOfWork.Repository<CMS.Data.AppModel.TeacherEnrollment>().FindAsync(x => x.TeacherEnrollmentId == model.TeacherEnrollmentId);
                entity.TeacherId = model.TeacherId;
                entity.CourseId = model.CourseId;
                entity.SubjectId = model.SubjectId;
                entity.UpdateDate = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.TeacherId.ToString(), Result = true, Message = NotificationMessageConfig.UpdateSuccessMessage(model.TeacherId.ToString()) };
            }
            catch (Exception)
            {
                return new ResultModel { Id = model.TeacherId.ToString(), Result = false, Message = NotificationMessageConfig.UpdateErrorMessage(model.TeacherId.ToString()) };
            }
        }

        public async Task<ResultModel> Delete(int id)
        {
            var model = await _unitOfWork.Repository<CMS.Data.AppModel.TeacherEnrollment>().FindAsync(x => x.TeacherId == id);
            try
            {
                if (model != null)
                {
                    model.Deleted = true;
                    model.DeleteDate = DateTime.Now;
                    _unitOfWork.Commit();
                    return new ResultModel { Result = true, Message = NotificationMessageConfig.DeleteSuccessMessage(model.TeacherId.ToString()), Id = model.TeacherId.ToString() };
                }
                else
                {
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.NotFoundError };
                }

            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.DeleteErrorMessage(model.TeacherId.ToString()) };
            }
        }

        //public ResultModel ShowingCourseDetails()
        //{





        //}
    }
}
