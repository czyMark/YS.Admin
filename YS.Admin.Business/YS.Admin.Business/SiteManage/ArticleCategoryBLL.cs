using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YS.Admin.Util;
using YS.Admin.Util.Extension;
using YS.Admin.Util.Model;
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;
using YS.Admin.Service.SiteManage;
using YS.Admin.Model.Result;
using YS.Admin.Web.Code;

namespace YS.Admin.Business.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2022-01-02 18:06
    /// 描 述：业务类
    /// </summary>
    public class ArticleCategoryBLL
    {
        private ArticleCategoryService articleCategoryService = new ArticleCategoryService();

        #region 获取数据
        public async Task<TData<List<ArticleCategoryEntity>>> GetList(ArticleCategoryListParam param)
        {
            TData<List<ArticleCategoryEntity>> obj = new TData<List<ArticleCategoryEntity>>();
            obj.Data = await articleCategoryService.GetList(param);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;

        }
        //public async Task<TData<List<ZtreeInfo>>> GetZtreeArticleCategoryList(ArticleCategoryListParam param)
        //{
        //    var obj = new TData<List<ZtreeInfo>>();
        //    obj.Data = new List<ZtreeInfo>();
        //    List<ArticleCategoryEntity> departmentList = await articleCategoryService.GetList(param);
          
        //    foreach (ArticleCategoryEntity articleCategory in departmentList)
        //    {
        //        obj.Data.Add(new ZtreeInfo
        //        {
        //            id = articleCategory.Id,
        //            pId = articleCategory.ParentId,
        //            name = articleCategory.Title
        //        });
        //    }
        //    obj.Tag = 1;
        //    return obj;
        //}
        public async Task<TData<List<ArticleCategoryEntity>>> GetPageList(ArticleCategoryListParam param, Pagination pagination)
        {
            //TData<List<ArticleCategoryEntity>> obj = new TData<List<ArticleCategoryEntity>>();
            //obj.Data = await articleCategoryService.GetPageList(param, pagination);
            //obj.Total = pagination.TotalCount;
            //obj.Tag = 1;
            //return obj;
            TData<List<ArticleCategoryEntity>> obj = new TData<List<ArticleCategoryEntity>>();
            pagination.Sort = "SortId";
            pagination.SortType = "Asc";
            obj.Data = await articleCategoryService.GetPageList(param, pagination);
            //OperatorInfo operatorInfo = await Operator.Instance.Current();
            //if (operatorInfo.IsSystem != 1)
            //{
            //    List<long> childrenDepartmentIdList = await GetChildrenArticleCategoryIdList(obj.Data, operatorInfo.DepartmentId.Value);
            //    obj.Data = obj.Data.Where(p => childrenDepartmentIdList.Contains(p.Id.Value)).ToList();
            //}
            //List<UserEntity> userList = await userService.GetList(new UserListParam { UserIds = string.Join(",", obj.Data.Select(p => p.PrincipalId).ToArray()) });
            //foreach (DepartmentEntity entity in obj.Data)
            //{
            //    if (entity.PrincipalId > 0)
            //    {
            //        entity.PrincipalName = userList.Where(p => p.Id == entity.PrincipalId).Select(p => p.RealName).FirstOrDefault();
            //    }
            //    else
            //    {
            //        entity.PrincipalName = string.Empty;
            //    }
            //}
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<List<ZtreeInfo>>> GetZtreeArticleCategorytList(ArticleCategoryListParam param)
        {
            var obj = new TData<List<ZtreeInfo>>();
            obj.Data = new List<ZtreeInfo>();
            List<ArticleCategoryEntity> articlecategoryList = await articleCategoryService.GetList(param);
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (operatorInfo.IsSystem != 1)
            {
                List<long> childrenArticleCategoryIdList = await GetChildrenArticleCategoryIdList(articlecategoryList, operatorInfo.DepartmentId.Value);
                articlecategoryList = articlecategoryList.Where(p => childrenArticleCategoryIdList.Contains(p.Id.Value)).ToList();
            }
            foreach (ArticleCategoryEntity articleCategory in articlecategoryList)
            {
                obj.Data.Add(new ZtreeInfo
                {
                    id = articleCategory.Id,
                    pId = articleCategory.ParentId,
                    name = articleCategory.Title
                });
            }
            obj.Tag = 1;
            return obj;
        }
        public async Task<TData<ArticleCategoryEntity>> GetEntity(long id)
        {
            TData<ArticleCategoryEntity> obj = new TData<ArticleCategoryEntity>();
            obj.Data = await articleCategoryService.GetEntity(id);
            if (obj.Data != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }

        /// <summary>
        /// 获取同级别的类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TData<List<ArticleCategoryEntity>>> GetPeersList(long id)
        {
            TData<List<ArticleCategoryEntity>> obj = new TData<List<ArticleCategoryEntity>>();
            obj.Data = await articleCategoryService.GetPeersList(id);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }

        /// <summary>
        /// 获取低一个级别的类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TData<List<ArticleCategoryEntity>>> GetSubordinateLevelList(long id)
        {
            TData<List<ArticleCategoryEntity>> obj = new TData<List<ArticleCategoryEntity>>();
            obj.Data = await articleCategoryService.GetSubordinateLevelList(id);
            obj.Total = obj.Data.Count;
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ArticleCategoryEntity entity)
        {
            TData<string> obj = new TData<string>();
            await articleCategoryService.SaveForm(entity);
            obj.Data = entity.Id.ToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await articleCategoryService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取当前部门及下面所有的部门
        /// </summary>
        /// <param name="articlecategoryList"></param>
        /// <param name="articlecategoryId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetChildrenArticleCategoryIdList(List<ArticleCategoryEntity> articlecategoryList, long articlecategoryId)
        {
            if (articlecategoryList == null)
            {
                articlecategoryList = await articleCategoryService.GetList(null);
            }
            List<long> articlecategoryIdList = new List<long>();
            articlecategoryIdList.Add(articlecategoryId);
            GetChildrenArticleCategoryIdList(articlecategoryList, articlecategoryId, articlecategoryIdList);
            return articlecategoryIdList;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取该部门下面所有的子部门
        /// </summary>
        /// <param name="articlecategoryList"></param>
        /// <param name="articlecategoryId"></param>
        /// <param name="articlecategoryIdList"></param>
        private void GetChildrenArticleCategoryIdList(List<ArticleCategoryEntity> articlecategoryList, long articlecategoryId, List<long> articlecategoryIdList)
        {
            var children = articlecategoryList.Where(p => p.ParentId == articlecategoryId).Select(p => p.Id.Value).ToList();
            if (children.Count > 0)
            {
                articlecategoryIdList.AddRange(children);
                foreach (long id in children)
                {
                    GetChildrenArticleCategoryIdList(articlecategoryList, id, articlecategoryIdList);
                }
            }
        }
        #endregion

        #region 前台获取数据
        public async Task<List<ArticleCategoryEntity>> GetWebList(ArticleCategoryListParam param)
        {
            //List<ArticleCategoryEntity> obj = new List<ArticleCategoryEntity>();
            return await articleCategoryService.GetWebList(param);
            //return obj;

        }
        #endregion
    }
}
