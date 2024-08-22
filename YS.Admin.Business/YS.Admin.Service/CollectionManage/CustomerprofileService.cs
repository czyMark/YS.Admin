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
using YS.Admin.Entity;
using YS.Admin.IdGenerator;
using static QRCoder.PayloadGenerator.RussiaPaymentOrder;
using YS.Admin.Entity.SystemManage;
using YS.Admin.Web.Code;
using YS.Admin.Entity.OrganizationManage;

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 17:04
    /// 描 述：客户档案服务类
    /// </summary>
    public class CustomerprofileService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<CustomerprofileEntity>> GetList(CustomerprofileListParam param)
        {
            var expression = ListFilter(param);

            //增加用户验证，只查询当前用户的档案数据
            //获取当前用户id
            BaseExtensionEntity bee = new BaseExtensionEntity();
            bee.Create();
            //验证用户是否是超级管理员

            var exp = LinqExtensions.True<UserEntity>();
            exp = exp.And(t => t.Id == bee.BaseCreatorId);
            UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            if (user.IsSystem != 1)
            {
                expression = expression.And(t => t.BaseCreatorId == user.Id);
            }

            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<CustomerprofileEntity>> GetPageList(CustomerprofileListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            //增加用户验证，只查询当前用户的档案数据
            //获取当前用户id
            BaseExtensionEntity bee = new BaseExtensionEntity();
            bee.Create();
            //验证用户是否是超级管理员

            var exp = LinqExtensions.True<UserEntity>();
            exp = exp.And(t => t.Id == bee.BaseCreatorId);
            UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            if (user.IsSystem != 1)
            {
                expression = expression.And(t => t.BaseCreatorId == user.Id);
            }

            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<CustomerprofileEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<CustomerprofileEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(CustomerprofileEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                //前往 collection_bn 表查询 编号数据
                //当前年开始
                //开始编码  年份+8位自增数字
                int year = DateTime.Now.Year.ToString().ParseToInt();

                var expression = LinqExtensions.True<BNEntity>();

                expression = expression.And(t => t.Year == year);


                BNEntity bN = await this.BaseRepository().FindEntity<BNEntity>(expression);
                //在编号上直接增加
                //查询上一张表中是否有记录 
                entity.StartCode = (bN.Bn+1).ToString();
                entity.EndCode = (bN.Bn + entity.BanknoteCount.ParseToInt() + entity.StampCount.ParseToInt() + entity.CoinCount.ParseToInt()).ToString();
                
                //查询最新的表
                string verifysql = "select * from collection_pringdatamaintag order by  BaseCreateTime desc limit 1";
                var lastEntity = await this.BaseRepository().FindObject<PringdatamaintagEntity>(verifysql);
                if (lastEntity == null)
                {
                    //todo:请新建数据表
                }
                entity.PrintDataId = lastEntity.Id;

                //先修改初始表
                bN.Bn = entity.EndCode.ParseToLong();
                await bN.Modify();
                await this.BaseRepository().Update(bN);
                 
                //在修改档案表
                await entity.Create();
                await this.BaseRepository().Insert(entity);

                //新增对应条目的数据
                {
                    //纸币
                    int rowCount = entity.BanknoteCount.ParseToInt();
                    int tempCode = entity.StartCode.Substring(2, 8).ParseToInt();
                    InsertBaseData(tempCode, rowCount, "1", lastEntity.DataTableName, entity);
                }
                {
                    //邮票
                    int rowCount = entity.StampCount.ParseToInt();
                    int tempCode = entity.StartCode.Substring(2, 8).ParseToInt() + entity.BanknoteCount.ParseToInt();
                    InsertBaseData(tempCode, rowCount, "2", lastEntity.DataTableName, entity);
                }
                {
                    //硬币
                    int rowCount = entity.CoinCount.ParseToInt();
                    int tempCode = entity.StartCode.Substring(2, 8).ParseToInt() + entity.BanknoteCount.ParseToInt() + entity.StampCount.ParseToInt();
                    InsertBaseData(tempCode, rowCount, "3", lastEntity.DataTableName, entity);
                }




            }
            //没有修改档案的逻辑
            //else
            //{
            //    await entity.Modify();
            //    await this.BaseRepository().Update(entity);
            //}
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<CustomerprofileEntity>(idArr);
        }
        #endregion

        #region 私有方法

        private async void InsertBaseData(int tempCode, int rowCount, string dataTag, string dataTableName, CustomerprofileEntity entity)
        {

            //为解决同时插入10000条数据 部分数据无法插入，内存溢出的情况。
            //分批次插入。每500条插入一次

            string insertSql = string.Empty;

            string insertSqlTemp = @"INSERT INTO {0}
(`Id`, 
`CustomerProfileId`,
`CustomerName`,
`IDCode`, 
`DataTag`, 
`BaseIsDelete`,
`BaseCreateTime`,
`BaseModifyTime`,
`BaseCreatorId`,
`BaseModifierId`,
`BaseVersion`)
VALUES
({1},{2},'{3}','{4}',{5},{6},NOW(),NOW(),{9},{10},{11});";
            for (int i = 0; i < rowCount; i++)
            {

                //雪花id
                long id = IdGeneratorHelper.Instance.GetId();

                string IdCode = DateTime.Now.Year.ToString().Substring(2, 2) + (tempCode + i).ToString().PadLeft(8, '0');
                //插入数据到lastEntity.DataTableName

                insertSql += string.Format(insertSqlTemp,
                    dataTableName, id, entity.Id, entity.CustomerName, IdCode, dataTag,
                    entity.BaseIsDelete, entity.BaseCreateTime, entity.BaseModifyTime,
                    entity.BaseCreatorId, entity.BaseModifierId, entity.BaseVersion);
                if ((i + 1) % 500 == 0)
                {
                    await this.BaseRepository().ExecuteBySql(insertSql);
                    insertSql = string.Empty;
                }
            }
            if (!string.IsNullOrEmpty(insertSql))
            {
                await this.BaseRepository().ExecuteBySql(insertSql);
            }
        }



        private Expression<Func<CustomerprofileEntity, bool>> ListFilter(CustomerprofileListParam param)
        {
            var expression = LinqExtensions.True<CustomerprofileEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.CustomerName))
                {
                    expression = expression.And(t => t.CustomerName.Contains(param.CustomerName));
                }
                if (param.PrintDataId != -1 && param.PrintDataId != null)
                {
                    expression = expression.And(t => t.PrintDataId == param.PrintDataId);
                }

            }
            return expression;
        }
        #endregion
    }
}
