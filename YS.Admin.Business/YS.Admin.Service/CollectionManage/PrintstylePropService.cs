using System;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Data;
using YS.Admin.Data.Repository;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:47
    /// 描 述：样式属性服务类
    /// </summary>
    public class PrintstylePropService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<PrintstylePropEntity>> GetList(PrintstylePropListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<PrintstylePropEntity>> GetPageList(PrintstylePropListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }
        public async Task<List<PrintstylePropEntity>> GetAllList(PrintstylePropListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<PrintstylePropEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<PrintstylePropEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(PrintstylePropEntity entity)
        {
            //验证这个模版是否配置过这个元素列，如果配置过返回错误信息


            var expression = LinqExtensions.True<PrintstylePropEntity>();
            expression = expression.And(t => t.StyleID == entity.StyleID);
            expression = expression.And(t => t.StylePropElement == entity.StylePropElement);
           

            if (entity.Id.IsNullOrZero())
            {
                var verify = await this.BaseRepository().FindList<PrintstylePropEntity>(expression);
                if (verify.Count() > 0)
                {
                    entity.Token = "元素列已存在";
                    return;
                }
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                expression = expression.And(t => t.Id != entity.Id);


                var verify = await this.BaseRepository().FindList<PrintstylePropEntity>(expression);
                if (verify.Count() > 0)
                {
                    entity.Token = "元素列已存在";
                    return;
                }
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<PrintstylePropEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<PrintstylePropEntity, bool>> ListFilter(PrintstylePropListParam param)
        {
            var expression = LinqExtensions.True<PrintstylePropEntity>();
            if (param != null)
            {
                if (param.StyleId != -1 && param.StyleId != null)
                {
                    expression = expression.And(t => t.StyleID == param.StyleId);
                }

                if (param.StylePropElement != "-1" && !string.IsNullOrEmpty(param.StylePropElement))
                {
                    expression = expression.And(t => t.StylePropElement == param.StylePropElement);
                }

            }
            return expression;
        }
        #endregion
    }
}
