using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;
using YS.Admin.Service.CollectionManage;
using YS.Admin.Service.OrganizationManage;

namespace YS.Admin.Business.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-24 11:29
    /// 描 述：评分信息业务类
    /// </summary>
    public class ScoreBLL
    {
        private ScoreService scoreService = new ScoreService();

        #region 获取数据
        public async Task<TData<List<ScoreEntity>>> GetList(ScoreListParam param)
        {
            TData<List<ScoreEntity>> obj = new TData<List<ScoreEntity>>();
            obj.Data = await scoreService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ScoreEntity>>> GetPageList(ScoreListParam param, Pagination pagination)
        {
            TData<List<ScoreEntity>> obj = new TData<List<ScoreEntity>>();
            obj.Data = await scoreService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ScoreEntity>> GetEntity(long id)
        {
            TData<ScoreEntity> obj = new TData<ScoreEntity>();
            obj.Data = await scoreService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ScoreEntity entity)
        {

            TData<string> obj = new TData<string>();

            if (scoreService.ExistScore(entity))
            {
                obj.Message = "分数已经存在！";
                return obj;
            }

            await scoreService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await scoreService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
