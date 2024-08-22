using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.BlogManage;
using YS.Admin.Model.Param.BlogManage;
using YS.Admin.Service.BlogManage;
using System.Linq.Expressions;

namespace YS.Admin.Business.BlogManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-16 22:05
    /// 描 述：博客文章评论业务类
    /// </summary>
    public class CommentBLL
    {
        private CommentService commentService = new CommentService();

        #region 获取数据
        public async Task<TData<List<CommentEntity>>> GetList(CommentListParam param)
        {
            TData<List<CommentEntity>> obj = new TData<List<CommentEntity>>();
            obj.Data = await commentService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<CommentListInfo>>> GetPageList(CommentListParam param, Pagination pagination)
        {
            TData<List<CommentListInfo>> obj = new TData<List<CommentListInfo>>();
            obj.Data = await commentService.GetPageList(param, pagination);
            obj.Total = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<CommentListInfo>> GetEntity(long id)
        {
            TData<CommentListInfo> obj = new TData<CommentListInfo>();
            obj.Data = await commentService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(CommentEntity entity)
        {
            TData<string> obj = new TData<string>();
            await commentService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<string>> SaveForms(List<CommentEntity> entities)
        {
            TData<string> obj = new TData<string>();
            await commentService.SaveForms(entities);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await commentService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<string>> UpdateForms(CommentEntity entity, Expression<Func<CommentEntity, CommentEntity>> updateExpression)
        {
            TData<string> obj = new TData<string>();
            long[] IdsList = TextHelper.SplitToArray<long>(entity.ids, ',');
            if (IdsList.Length.Equals(0))
            {
             	obj.Tag = 0;
                 obj.Message = "请选择操作数据";
                 return obj;
            }
            
            await commentService.Update(t => IdsList.Contains(t.Id.Value), updateExpression);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
