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
using YS.Admin.Entity.SiteManage;
using YS.Admin.Model.Param.SiteManage;
using NPOI.SS.Formula.Functions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using TagLib.Ogg;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Service.CollectionManage;
using TagLib.Ape;

namespace YS.Admin.Service.SiteManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-08-05 14:00
    /// 描 述：栏目文字数据服务类
    /// </summary>
    public class ArticlesDescriptiondataService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ArticlesDescriptiondataEntity>> GetList(ArticlesDescriptiondataListParam param)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<ArticlesDescriptiondataEntity>> GetPageList(ArticlesDescriptiondataListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<ArticlesDescriptiondataEntity> GetEntity(long id)
        {
            return await BaseRepository().FindEntity<ArticlesDescriptiondataEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ArticlesDescriptiondataEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await BaseRepository().Update(entity);
            }
        }
        public async Task<string> SaveIDCOdeCreateForm(ArticlesDescriptiondataEntity entity)
        {
            //依据到的数据类型创建数据
            var list = await new PrintdataBaseService().GetDatabaseTypeDataList(
                new Model.Param.CollectionManage.PrintdataBaseListParam() { IDCode = entity.C1 },
                new Pagination()
                );
            var temp = list.FirstOrDefault();
            if (temp == null)
            {
                return $"未找到{entity.C1}藏品信息";
            }
            List<ArticlesDescriptiondataEntity> objData = new List<ArticlesDescriptiondataEntity>();
            //组织数据
            if (temp.DataTag == 1)
            {
                //{
                //    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                //    obj.ArticlesId = entity.Id;
                //    obj.C1 = "鉴定编号";
                //    obj.C2 = temp.IDCode;
                //    objData.Add(obj);
                //}
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "鉴定编号";
                    obj.C2 = temp.IDCode;
                    objData.Add(obj);
                }

                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "藏品名称";
                    obj.C2 = temp.CollectionName;
                    objData.Add(obj);
                }

                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "年份";
                    obj.C2 = temp.CollectionYear;
                    obj.C5 = "大数据标识界面显示";
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "面值";
                    obj.C2 = temp.CollectionValue;
                    obj.C5 = "大数据标识界面显示";
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "印刷工艺";
                    obj.C2 = temp.PrintArt;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "冠字编号";
                    obj.C2 = temp.SerialCode;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "评分";
                    obj.C2 = temp.Rating;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "HQP";
                    obj.C2 = temp.HQP;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "三星";
                    obj.C2 = temp.StarTag;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "版别/个性化";
                    obj.C2 = temp.EditionPersonalization;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "估值保价";
                    obj.C2 = temp.EstimatedValue;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "珍稀度";
                    obj.C2 = temp.Rarity;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "发行单位";
                    obj.C2 = temp.IssuingUnit;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "鉴定师";
                    obj.C2 = temp.AppraiserName;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "藏品图片";
                    obj.C4 = temp.CollectionImage;
                    obj.C5 = "藏品图片";
                    objData.Add(obj);
                } 
            }
            else if (temp.DataTag == 2)
            {
                //{
                //    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                //    obj.ArticlesId = entity.Id;
                //    obj.C1 = "鉴定编号";
                //    obj.C2 = temp.IDCode;
                //    objData.Add(obj);
                //}
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "鉴定编号";
                    obj.C2 = temp.IDCode;
                    objData.Add(obj);
                }

                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "藏品名称";
                    obj.C2 = temp.CollectionName;
                    objData.Add(obj);
                }

                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "年份";
                    obj.C2 = temp.CollectionYear;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "面值";
                    obj.C2 = temp.CollectionValue;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "评分";
                    obj.C2 = temp.Rating;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "OS";
                    obj.C2 = temp.OS;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "描述";
                    obj.C2 = temp.Description;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "版别";
                    obj.C2 = temp.Edition;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "志号";
                    obj.C2 = temp.NumberCode;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "原胶";
                    obj.C2 = temp.VirginRubber;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "个性化";
                    obj.C2 = temp.Personalization;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "估值";
                    obj.C2 = temp.EstimatedValue;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "珍稀度";
                    obj.C2 = temp.Rarity;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "发行单位";
                    obj.C2 = temp.IssuingUnit;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "鉴定师";
                    obj.C2 = temp.AppraiserName;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "藏品图片";
                    obj.C4 = temp.CollectionImage;
                    obj.C5 = "藏品图片";
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "大数据标志";
                    obj.C2 = temp.BigDataTag;
                    obj.C5 = "大数据标识";
                    objData.Add(obj);
                }
            }
            else if (temp.DataTag == 3)
            {
                //{
                //    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                //    obj.ArticlesId = entity.Id;
                //    obj.C1 = "鉴定编号";
                //    obj.C2 = temp.IDCode;
                //    objData.Add(obj);
                //}
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "鉴定编号";
                    obj.C2 = temp.IDCode;
                    objData.Add(obj);
                }

                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "藏品名称";
                    obj.C2 = temp.CollectionName;
                    objData.Add(obj);
                }

                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "年份";
                    obj.C2 = temp.CollectionYear;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "面值";
                    obj.C2 = temp.CollectionValue;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "评分";
                    obj.C2 = temp.Rating;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "OS";
                    obj.C2 = temp.OS;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "版别";
                    obj.C2 = temp.Edition;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "材质";
                    obj.C2 = temp.Material;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "重量";
                    obj.C2 = temp.Weight;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "尺寸";
                    obj.C2 = temp.Size;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "估值保价";
                    obj.C2 = temp.EstimatedValue;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "珍稀度";
                    obj.C2 = temp.Rarity;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "发行单位";
                    obj.C2 = temp.IssuingUnit;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "鉴定师";
                    obj.C2 = temp.AppraiserName;
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "藏品图片";
                    obj.C4 = temp.CollectionImage;
                    obj.C5 = "藏品图片";
                    objData.Add(obj);
                }
                {
                    ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                    obj.ArticlesId = entity.Id;
                    obj.C1 = "大数据标志";
                    obj.C2 = temp.BigDataTag;
                    obj.C5 = "大数据标识";
                    objData.Add(obj);
                }
            }
            {
                ArticlesDescriptiondataEntity obj = new ArticlesDescriptiondataEntity();
                obj.ArticlesId = entity.Id;
                obj.C1 = "评级日期";
                DateTime dt = temp.BaseCreateTime.Value;
                obj.C2 = dt.ToString("yyyy年MM月dd日");
                obj.C5 = "评级日期";
                objData.Add(obj);
            }
            int sortindex = 1;
            //创建数据
            foreach (var item in objData)
            {
                item.SortId = sortindex;
                await item.Create();
                await BaseRepository().Insert(item);
                sortindex++;
            }
            return "";
        }


        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await BaseRepository().Delete<ArticlesDescriptiondataEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<ArticlesDescriptiondataEntity, bool>> ListFilter(ArticlesDescriptiondataListParam param)
        {
            var expression = LinqExtensions.True<ArticlesDescriptiondataEntity>();
            if (param != null)
            {
                if (!param.pid.IsEmpty())
                {
                    expression = expression.And(t => t.ArticlesId == param.pid.ParseToLong());
                }
                if (!param.C1.IsEmpty())
                {
                    expression = expression.And(t => t.C1.Contains(param.C1) || t.C2.Contains(param.C1) || t.C3.Contains(param.C1) || t.C4.Contains(param.C1) || t.C5.Contains(param.C1));
                }
            }
            return expression;
        }
        #endregion
    }
}
