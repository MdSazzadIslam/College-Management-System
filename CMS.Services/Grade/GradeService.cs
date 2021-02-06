using CMS.Configuration;
using CMS.Data.Common;
using CMS.Repository;
using CMS.Services.Grade.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Grade
{
    public class GradeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<List<GradeVM>> GetAll()
        {
            var list =await _unitOfWork.Repository<CMS.Data.AppModel.Grade>().GetAll().Where(x => x.Deleted == false)
                .Select(x=> new GradeVM { 
                GradeId = x.GradeId,
                GradeName = x.GradeName,
                ScoreFrom = x.ScoreFrom,
                GradePoint= x.GradePoint,
                ScoreTo = x.ScoreTo
                })
                .OrderByDescending(x=>x.GradeId)
                .ToListAsync();
            return list;
        }

        public IQueryable<CMS.Data.AppModel.Grade> GetGradeById(string searchBy)
        {

            var list = _unitOfWork.Repository<CMS.Data.AppModel.Grade>().GetAll().Where(x => x.Deleted == false).OrderByDescending(x => x.GradeId).AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                list = list.Where(x => x.GradeName.ToLower().Contains(searchBy.ToLower())).AsQueryable();
            }
            return list;
        }
        public async Task<CMS.Data.AppModel.Grade> GetById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Grade>().FindAsync(x => x.GradeId == id);
            return entity;
        }
        public async Task<GradeVM> GetGradeById(int id)
        {
            var entity = await _unitOfWork.Repository<CMS.Data.AppModel.Grade>().FindAsync(x => x.GradeId == id);
            if (entity != null)
            {
                var data = new GradeVM
                {
                    GradeId = entity.GradeId,
                    GradeName = entity.GradeName,
                    ScoreFrom = entity.ScoreFrom,
                    ScoreTo = entity.ScoreTo
                };
                return data;
            }
            else
            {
                var data = new GradeVM();
                return data;
            }


        }

        public CMS.Data.AppModel.Grade GetByName(string name)
        {
            return _unitOfWork.Repository<CMS.Data.AppModel.Grade>().GetAll().Where(x => x.GradeName.Trim().Equals(name.Trim())).SingleOrDefault();
        }

        public async Task<ResultModel> Insert(GradeVM model)
        {
            try
            {
                if (GetByName(model.GradeName) != null)
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.ExistMessage(model.GradeName) };
                //var entity = new CMS.Data.AppModel.Grade();
                //Mapper.Map(entity, model);

                var entity = new CMS.Data.AppModel.Grade
                {
                    GradeName = model.GradeName,
                    GradePoint = model.GradePoint,
                    ScoreFrom= model.ScoreFrom,
                    ScoreTo = model.ScoreTo,
                    CreateDate = DateTime.Now
                };



                await _unitOfWork.Repository<CMS.Data.AppModel.Grade>().InsertAsync(entity);

                return new ResultModel { Id = entity.GradeId.ToString(), Result = true, Message = NotificationMessageConfig.InsertSuccessMessage(model.GradeName) };
            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.InsertErrorMessage(model.GradeName) };
            }
        }

        public async Task<ResultModel> Update(GradeVM model)
        {
            try
            {
                var entity = await GetById(model.GradeId);
                entity.GradeName = model.GradeName;
                entity.GradePoint = model.GradePoint;
                entity.ScoreFrom = model.ScoreFrom;
                entity.ScoreTo = model.ScoreTo;
                entity.UpdateDate = DateTime.Now;
                await _unitOfWork.CommitAsync();

                return new ResultModel { Id = entity.GradeId.ToString(), Result = true, Message = NotificationMessageConfig.UpdateSuccessMessage(model.GradeName) };
            }
            catch (Exception)
            {
                return new ResultModel { Id = model.GradeId.ToString(), Result = false, Message = NotificationMessageConfig.UpdateErrorMessage(model.GradeName) };
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
                    return new ResultModel { Result = true, Message = NotificationMessageConfig.DeleteSuccessMessage(model.GradeName), Id = model.GradeId.ToString() };
                }
                else
                {
                    return new ResultModel { Result = false, Message = NotificationMessageConfig.NotFoundError };
                }

            }
            catch (Exception)
            {
                return new ResultModel { Result = false, Message = NotificationMessageConfig.DeleteErrorMessage(model.GradeName) };
            }
        }

    }

}
