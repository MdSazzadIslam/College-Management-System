using CMS.Configuration;
using CMS.Data.Common;
using CMS.Repository;
using CMS.Services.Teacher.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Teacher
{
    public class TeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TeacherVM>> GetAll()
        {
            var list = await _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().GetAll().Where(x => x.Deleted == false)
                .Select(x=> new TeacherVM{
                TeacherId = x.TeacherId,
                TeacherName=x.TeacherName,
                BirthDate = x.BirthDate,
                Salary = x.Salary,
                })
                .OrderByDescending(x => x.TeacherId).ToListAsync();
            return list;
        }

        public async Task<List<TeacherVM>>  GetTeacherById(string searchBy)
        {

            var list = _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().GetAll().Where(x => x.Deleted == false)
                .Select(x=> new TeacherVM{
                TeacherId = x.TeacherId,
                TeacherName=x.TeacherName,
                BirthDate = x.BirthDate,
                Salary = x.Salary,
                })
                .OrderByDescending(x => x.TeacherId).AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                list = list.Where(x => x.TeacherName.ToLower().Contains(searchBy.ToLower()) || x.Salary.ToString().Contains(searchBy.ToLower())).AsQueryable();
            }
            return await list.ToListAsync();
        }
        public async Task<CMS.Data.AppModel.Teacher> GetById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().FindAsync(x => x.TeacherId == id);
            return entity;
        }
        public async Task<TeacherVM> GetTeacherById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().FindAsync(x => x.TeacherId == id);
            if (entity != null)
            {
                var data = new TeacherVM
                {
                    TeacherId = entity.TeacherId,
                    TeacherName = entity.TeacherName,
                    BirthDate = entity.BirthDate,
                    Salary = entity.Salary
                };
                return data;
            }
            else
            {
                var data = new TeacherVM();
                return data;
            }


        }

        public CMS.Data.AppModel.Teacher GetByName(string name)
        {
            return _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().GetAll().Where(x => x.TeacherName.Trim().Equals(name.Trim())).SingleOrDefault();
        }
        public async Task<ResultModel> Insert(TeacherVM model)
        {
            try
            {
                if (GetByName(model.TeacherName) != null)
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.TeacherName) };

                var entity = new CMS.Data.AppModel.Teacher
                {
                    TeacherName = model.TeacherName,
                    BirthDate = model.BirthDate,
                    Salary= model.Salary,
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<CMS.Data.AppModel.Teacher>().InsertAsync(entity);

                return new ResultModel { Id = entity.TeacherId.ToString(), Result = true, Message = NotificationMessageConfig.InsertSuccessMessage(model.TeacherName) };
            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.InsertErrorMessage(model.TeacherName) };
            }
        }

        public async Task<ResultModel> Update(TeacherVM model)
        {
            try
            {
                var entity = await GetById(model.TeacherId);
                entity.TeacherName = model.TeacherName;
                entity.BirthDate = model.BirthDate;
                entity.Salary = model.Salary;
                entity.UpdateDate = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.TeacherId.ToString(), Result = true, Message = NotificationMessageConfig.UpdateSuccessMessage(model.TeacherName) };
            }
            catch (Exception)
            {
                return new ResultModel { Id = model.TeacherId.ToString(), Result = false, Message = NotificationMessageConfig.UpdateErrorMessage(model.TeacherName) };
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
                    return new ResultModel { Result = true, Message = NotificationMessageConfig.DeleteSuccessMessage(model.TeacherName), Id = model.TeacherId.ToString() };
                }
                else
                {
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.NotFoundError };
                }

            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.DeleteErrorMessage(model.TeacherName) };
            }
        }

    }
}
