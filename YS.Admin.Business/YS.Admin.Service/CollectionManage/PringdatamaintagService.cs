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
using System.Security.Cryptography;
using YS.Admin.Entity;

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:48
    /// 描 述：分表数据服务类
    /// </summary>
    public class PringdatamaintagService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<PringdatamaintagEntity>> GetList(PringdatamaintagListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            foreach (var item in list)
            {
                item.MainTagName = "表" + item.MainTagName;
            }
            return list.ToList();
        }

        public async Task<List<PringdatamaintagEntity>> GetPageList(PringdatamaintagListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            foreach ( var item in list )
            {
                item.MainTagName = "表" + item.MainTagName;
            }
            return list.ToList();
        }

        public async Task<PringdatamaintagEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<PringdatamaintagEntity>(id);
        }
        #endregion



        #region 提交数据
        public async Task SaveForm(PringdatamaintagEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                #region 计算编码
                //开始编码  年份+8位自增数字
                //string year = DateTime.Now.Year.ToString().Substring(2, 2);
                ////查询上一张表中是否有记录 
                //string verifysql = "select * from collection_pringdatamaintag order by  BaseCreateTime desc limit 1";
                //var lastEntity = await this.BaseRepository().FindObject<PringdatamaintagEntity>(verifysql);
                //if (lastEntity == null)
                //{
                //    entity.StartCode = year + "1".ToString().PadLeft(8, '0');
                //    entity.MainTagName = "1";
                //}
                //else
                //{
                //    entity.MainTagName = (lastEntity.MainTagName.ParseToInt() + 1).ToString();

                //    #region 计算开始编码

                //    //查询最后的记录 X
                //    verifysql = $"select * from {lastEntity.DataTableName} order by  CAST(IDCode AS UNSIGNED) desc limit 1";
                //    PrintdataBaseEntity PBE = await this.BaseRepository().FindObject<PrintdataBaseEntity>(verifysql);
                //    //查询没有找到编码
                //    if (PBE == null || string.IsNullOrEmpty(PBE.IDCode)) //没有查询到数据
                //    {
                //        //没有的情况编码是年+00000001
                //        entity.StartCode = lastEntity.StartCode;
                //    }
                //    else
                //    {
                //        //将编码+1变成新表起始ID
                //        string oldYear = PBE.IDCode.Substring(0, 2);
                //        //如果是当前年的情况
                //        if (oldYear == year)
                //        {
                //            int lastCode = PBE.IDCode.Substring(2, 8).ParseToInt() + 1;
                //            //年+8位编号
                //            entity.StartCode = oldYear + lastCode.ToString().PadLeft(8, '0');
                //        }
                //        else
                //        {
                //            //没有的情况编码是年+00000001
                //            entity.StartCode = oldYear + "1".ToString().PadLeft(8, '0');
                //        }
                //    }
                //    #endregion
                //}
                #endregion

                //更改为到bn表中直接查询最新编码


                //前往 collection_bn 表查询 编号数据
                //当前年开始
                //开始编码  年份+8位自增数字
                int year = DateTime.Now.Year.ToString().ParseToInt();

                var expression = LinqExtensions.True<BNEntity>();

                expression = expression.And(t => t.Year == year);


                BNEntity bN = await this.BaseRepository().FindEntity<BNEntity>(expression);
                //在编号上直接增加
                //查询上一张表中是否有记录 
                entity.StartCode = (bN.Bn + 1).ToString();

                //创建表对应的逻辑
                string tableTempName = DateTime.Now.ToString("yyyyMMddHHmmss");
                entity.DataTableName = "collection_printdata_" + tableTempName;
                string createTableSql = GetCreateTableSql(tableTempName);

                var tempExpression = LinqExtensions.True<PringdatamaintagEntity>();
                var list = await this.BaseRepository().FindList<PringdatamaintagEntity>(tempExpression);
                //

                entity.MainTagName =(list.Count()+ 1).ToString();
                await this.BaseRepository().ExecuteBySql(createTableSql);
                //新增操作
                await entity.Create();
                int sqlCount = await this.BaseRepository().Insert(entity);
                //新增异常回滚
                if (sqlCount <= 0)
                {
                    //删除表
                    await this.BaseRepository().ExecuteBySql(
                        string.Format("drop table {0};", entity.DataTableName)
                        );
                }

            }
            else
            {
                //修改操作无需调整数据等
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }


        /// <summary>
        /// 更新统计数据
        /// </summary>
        /// <param name="ids">要更新的表数据</param>
        /// <returns></returns>
        public async Task UpdataTotal(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            //查询对应表的统计结果，更新PringdatamaintagEntity中的数据
            for (int i = 0; i < idArr.Length; i++)
            {
                PringdatamaintagEntity entity = await this.BaseRepository().FindEntity<PringdatamaintagEntity>(idArr[i]);

                var list = await this.BaseRepository().FindList<PrintdataBaseEntity>($"select * from {entity.DataTableName};");

                entity.BanknoteCount = list.ToList().Where(p => p.DataTag == 1).Count();
                entity.StampCount = list.ToList().Where(p => p.DataTag == 2).Count();
                entity.CoinCount = list.ToList().Where(p => p.DataTag == 3).Count();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<PringdatamaintagEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<PringdatamaintagEntity, bool>> ListFilter(PringdatamaintagListParam param)
        {
            var expression = LinqExtensions.True<PringdatamaintagEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.MainTagName))
                {
                    expression = expression.And(t => t.MainTagName.Contains(param.MainTagName));
                }
            }
            return expression;
        }

        /// <summary>
        /// 获取建表sql
        /// </summary>
        /// <param name="tableName">数据表的后缀  前缀是collection_printdata_</param>
        /// <returns></returns>
        private string GetCreateTableSql(string tableName)
        {
            string baseSql = @"CREATE TABLE `collection_printdata_{0}` (
   `Id` bigint(20) NOT NULL,
  `PrintStyleID` bigint(20) DEFAULT NULL,
  `TagTypeID` bigint(20) DEFAULT NULL,
  `CustomerProfileId` bigint(20) DEFAULT NULL COMMENT '所属档案',
  `CustomerName` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '客户姓名',
  `IDCode` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '鉴定编号',
  `CollectionYear` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '年份',
  `CollectionValue` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '面值',
  `CollectionName` varchar(256) COLLATE utf8_bin DEFAULT NULL COMMENT '藏品名称	',
  `PrintArt` varchar(32) COLLATE utf8_bin DEFAULT NULL COMMENT '印刷工艺',
  `SerialCode` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '冠字编号',
  `Rating` varchar(8) COLLATE utf8_bin DEFAULT NULL COMMENT '评分',
  `HQP` varchar(8) COLLATE utf8_bin DEFAULT NULL COMMENT 'HQP',
  `StarTag` varchar(8) COLLATE utf8_bin DEFAULT NULL COMMENT '三星标志',
  `EditionPersonalization` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '版别/个性化',
  `EstimatedValue` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '估值保价',
  `Rarity` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '珍惜度',
  `IssuingUnit` varchar(256) COLLATE utf8_bin DEFAULT NULL COMMENT '发行单位',
  `OS` varchar(8) COLLATE utf8_bin DEFAULT NULL COMMENT 'OS',
  `Description` varchar(512) COLLATE utf8_bin DEFAULT NULL COMMENT '描述',
  `Edition` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '版别',
  `NumberCode` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '志号',
  `Personalization` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '个性化',
  `Material` varchar(16) COLLATE utf8_bin DEFAULT NULL COMMENT '材质',
  `Weight` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '重量',
  `Size` varchar(56) COLLATE utf8_bin DEFAULT NULL COMMENT '尺寸',
  `VirginRubber` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '原胶',
  `AppraiserName` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '鉴定师名称',
  `BigDataTag` varchar(128) COLLATE utf8_bin DEFAULT NULL COMMENT '大数据标志',
  `CollectionImage` varchar(1024) COLLATE utf8_bin DEFAULT NULL COMMENT '藏品图片，多个图片使用;分割',
  `TagTypeName` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '标签类型名称',
  `PrintStyleName` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '打印样式模版名称',
  `DataTag` int(11) DEFAULT '0' COMMENT '0无标志/1纸币数据/2邮票标志/3硬币数据标志',
  `DataState` int(11) DEFAULT '1' COMMENT '0-锁定\\\\n1-编辑\\\\n2-打印',
  `BaseIsDelete` int(11) NOT NULL COMMENT '是否删除',
  `BaseCreateTime` datetime NOT NULL COMMENT '创建时间',
  `BaseModifyTime` datetime NOT NULL COMMENT '修改时间',
  `BaseCreatorId` bigint(20) NOT NULL COMMENT '创建人',
  `BaseModifierId` bigint(20) NOT NULL COMMENT '修改人',
  `BaseVersion` int(11) NOT NULL COMMENT '数据版本(每次更新+1)',
  PRIMARY KEY (`Id`),
  KEY `FSyteId_idx{0}` (`PrintStyleID`),
  KEY `FTagTypeId_idx{0}` (`TagTypeID`),
  KEY `FCreateUserid_idx{0}` (`BaseCreatorId`),
  KEY `FEditUserId_idx{0}` (`BaseModifierId`),
  KEY `FCustomerProfileId_idx{0}` (`CustomerProfileId`),
  CONSTRAINT `FCustomerProfileId{0}` FOREIGN KEY (`CustomerProfileId`) REFERENCES `collection_customerprofile` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FSyteId{0}` FOREIGN KEY (`PrintStyleID`) REFERENCES `collection_printstyle` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FTagTypeId{0}` FOREIGN KEY (`TagTypeID`) REFERENCES `collection_tagtype` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='基础打印表';
";
            string sql = string.Format(baseSql, tableName);
            return sql;
        }
        #endregion
    }
}
