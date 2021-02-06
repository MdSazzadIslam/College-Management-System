using CMS.Configuration;
using CMS.Data.Common;
using CMS.Repository;
using CMS.Services.Subject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Subject
{
    public class SubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CMS.Repository.AppContext _appContext;
        public SubjectService(IUnitOfWork unitOfWork, CMS.Repository.AppContext appContext)
        {
            _unitOfWork = unitOfWork;
            _appContext = appContext;
        }

        public async Task<List<SubjectVM>> GetAll()
        {
            //var data = _appContext.Subjects.ToList();
            //var data1 = (from s in _appContext.Subjects
            //             join c in _appContext.Courses on s.CourseId equals c.CourseId
            //             where s.Deleted == false
            //             select new SubjectVM
            //             {
            //                 SubjectId = s.SubjectId
            //             }).ToList();

            //var list = await (from s in _appContext.Subjects
            //                  join c in _appContext.Courses on s.CourseId equals c.CourseId
            //                  where s.Deleted == false
            //                  select new SubjectVM
            //                  {
            //                      SubjectId = s.SubjectId,
            //                      SubjectCode = s.SubjectCode,
            //                      SubjectName = s.SubjectName,
            //                      SubjectCredit = s.SubjectCredit,
            //                      CourseId = s.CourseId,
            //                      CourseName =c.CourseName,
            //                  }).OrderByDescending(x => x.SubjectId).ToListAsync();

            var list = await _unitOfWork.Repository<CMS.Data.AppModel.Subject>().GetAll().Include(x => x.Courses).Where(x => x.Deleted == false)
                   .Select(x => new SubjectVM
                   {
                       SubjectId = x.SubjectId,
                       SubjectCode = x.SubjectCode,
                       SubjectName = x.SubjectName,
                       SubjectCredit = x.SubjectCredit,
                       CourseId = x.CourseId,
                       CourseName = x.Courses.CourseName,
                   }).OrderByDescending(x => x.SubjectId).ToListAsync();

            return list;
        }

        public async Task<List<SubjectVM>> GetSubjectById(string searchBy)
        {
            var list = _unitOfWork.Repository<CMS.Data.AppModel.Subject>().GetAll().Include(x => x.Courses)
                  .Select(x => new SubjectVM
                  {
                      SubjectId = x.SubjectId,
                      SubjectCode = x.SubjectCode,
                      SubjectName = x.SubjectName,
                      SubjectCredit = x.SubjectCredit,
                      CourseId = x.CourseId,
                      CourseName = x.Courses.CourseName,
                  }).OrderByDescending(x => x.SubjectId).AsQueryable();

            if (!string.IsNullOrEmpty(searchBy))
            {
                list = list.Where(x => x.SubjectName.ToLower().Contains(searchBy.ToLower()) || x.SubjectCode.ToLower().Contains(searchBy.ToLower()) || x.CourseName.ToLower().Contains(searchBy.ToLower())).AsQueryable();
            }
            return await list.ToListAsync();
        }

        public List<SubjectVM> GetSubjectByCourseId(int id)
        {
            var list = _unitOfWork.Repository<CMS.Data.AppModel.Subject>().GetAll()
                   .Where(x => x.Deleted == false && x.CourseId == id)
                   .Select(x => new SubjectVM
                   {
                       SubjectId = x.SubjectId,
                       SubjectName = x.SubjectName
                   }).OrderBy(x=>x.SubjectName).ToList();

            return list;
        }


        public async Task<List<SubjectListVM>> GetSubjectDetails()
        {
            var list = await (from s in _appContext.Subjects
                              from se in _appContext.StudentEnrollments
                              from te in _appContext.TeacherEnrollments
                              from teacher in _appContext.Teachers
                              where s.Deleted == false
                              group new { s, te, se, teacher } by s.SubjectId into cts
                              select new SubjectListVM
                              {
                                  SubjectId = cts.Key,
                                  SubjectName = cts.Select(x => x.s.SubjectName).FirstOrDefault(),
                                  SubjectCode = cts.Select(x => x.s.SubjectCode).FirstOrDefault(),
                                  TeacherName = cts.Select(x => x.teacher.TeacherName).FirstOrDefault(),
                                  Salary = cts.Select(x => x.teacher.Salary).FirstOrDefault(),
                                  NoOfStudent = cts.Select(x => x.se.StudentId).Distinct().Count(),
                              }).ToListAsync();
            return list;
        }


        public async Task<CMS.Data.AppModel.Subject> GetById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Subject>().FindAsync(x => x.SubjectId == id);
            return entity;
        }
        public async Task<SubjectVM> GetSubjectById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Subject>().FindAsync(x => x.SubjectId == id);
            if (entity != null)
            {
                var data = new SubjectVM
                {
                    SubjectId = entity.SubjectId,
                    SubjectCode = entity.SubjectCode,
                    SubjectName = entity.SubjectName,
                    SubjectCredit =entity.SubjectCredit,
                    CourseId = entity.CourseId
                };
                return data;
            }
            else
            {
                var data = new SubjectVM();
                return data;
            }


        }

        public CMS.Data.AppModel.Subject GetByName(string name)
        {
            return _unitOfWork.Repository<CMS.Data.AppModel.Subject>().GetAll().Where(x => x.SubjectName.Trim().Equals(name.Trim())).SingleOrDefault();
        }
        public CMS.Data.AppModel.Subject GetByCode(string name)
        {
            return _unitOfWork.Repository<CMS.Data.AppModel.Subject>().GetAll().Where(x => x.SubjectCode.Trim().Equals(name.Trim())).SingleOrDefault();
        }

        public async Task<ResultModel> Insert(SubjectVM model)
        {
            try
            {
                if (GetByName(model.SubjectName) != null)
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.SubjectName) };
                if (GetByName(model.SubjectCode) != null)
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.SubjectCode) };

                //var entity = new CMS.Data.AppModel.Subject();
                //Mapper.Map(entity, model);

                var entity = new CMS.Data.AppModel.Subject
                {
                    SubjectCode = model.SubjectCode,
                    SubjectName = model.SubjectName,
                    CourseId = model.CourseId,
                    SubjectCredit = model.SubjectCredit,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<CMS.Data.AppModel.Subject>().InsertAsync(entity);

                return new ResultModel { Id = entity.SubjectId.ToString(), Result = true, Message = NotificationMessageConfig.InsertSuccessMessage(model.SubjectName) };
            }
            catch (Exception e)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.InsertErrorMessage(model.SubjectName) };
            }
        }

        public async Task<ResultModel> Update(SubjectVM model)
        {
            try
            {
                var entity = await GetById(model.SubjectId);
                entity.SubjectName = model.SubjectName;
                entity.SubjectCode = model.SubjectCode;
                entity.CourseId = model.CourseId;
                entity.SubjectCredit = model.SubjectCredit;
                entity.UpdateDate = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.SubjectId.ToString(), Result = true, Message = NotificationMessageConfig.UpdateSuccessMessage(model.SubjectName) };
            }
            catch (Exception)
            {
                return new ResultModel { Id = model.SubjectId.ToString(), Result = false, Message = NotificationMessageConfig.UpdateErrorMessage(model.SubjectName) };
            }
        }

        public async Task<ResultModel> Delete(int id)
        {
            var model = await GetById(id);
            try
            {
                if (model != null)
                {
                    model.Deleted = true;
                    model.DeleteDate = DateTime.Now;

                    _unitOfWork.Commit();
                    return new ResultModel { Result = true, Message = NotificationMessageConfig.DeleteSuccessMessage(model.SubjectName), Id = model.SubjectId.ToString() };
                }
                else
                {
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.NotFoundError };
                }

            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.DeleteErrorMessage(model.SubjectName) };
            }
        }

    }
}
