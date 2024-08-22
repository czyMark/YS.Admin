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
using System.Collections;
using TagLib.NonContainer;
using YS.Admin.Entity;
using YS.Admin.Web.Code;
using YS.Admin.IdGenerator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using YS.Admin.Entity.OrganizationManage;
using TagLib.Ape;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Crypto;
using UglyToad.PdfPig.Content;

namespace YS.Admin.Service.CollectionManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表服务类
    /// </summary>
    public class PrintdataBaseService : RepositoryFactory
    {
        #region 获取数据 
        public async Task<List<PrintdataBaseEntity>> GetPageListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            string sql = string.Empty;
            string dataTableName = string.Empty;
            if (string.IsNullOrEmpty(param.TableName))
            {
                sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={param.Id} limit 1;";
                var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);
                dataTableName = itemtemp.DataTableName;
            }
            else
            {
                dataTableName = param.TableName; //获取要查询的数据表名
            }

            string whereStr = "";
            if (param.Id != -1)
            {
                whereStr += $" and CustomerProfileId={param.Id} ";
            }
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }



            //数据表名称：list.DataTableName; 
            sql = $"select count(1) as 'index' from {dataTableName} where DataTag={param.DataTag}   {whereStr}";
             

            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            
            return list.ToList();
        }

        public async Task<List<PrintdataBaseEntity>> GetPageList(PrintdataBaseListParam param, Pagination pagination)
        {
            string sql = string.Empty;
            string dataTableName = string.Empty;
            if (string.IsNullOrEmpty(param.TableName))
            {
                sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={param.Id} limit 1;";
                var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);
                dataTableName = itemtemp.DataTableName;
            }
            else
            {
                dataTableName = param.TableName; //获取要查询的数据表名
            }

            string whereStr = "";
            if (param.Id != -1)
            {
                whereStr += $" and CustomerProfileId={param.Id} ";
            }
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }



            //数据表名称：list.DataTableName; 
            sql = $"select * from {dataTableName} where DataTag={param.DataTag}   {whereStr}";

            sql += GetPaginationStr(pagination);

            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            int i = (pagination.PageIndex - 1) * pagination.PageSize + 1;
            foreach (var item in list)
            {
                item.Index = i;
                i++;
            }
            return list.ToList();
        }



        public async Task<List<PrintdataBaseEntity>> GetPrintPageListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(param.TableName))
            {
                sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={param.Id} limit 1;";
                var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);
                param.TableName = itemtemp.DataTableName;
                param.MainTagName = itemtemp.MainTagName;
            }

            string whereStr = "";
            if (param.Id != -1)
            {
                whereStr += $" and CustomerProfileId={param.Id} ";
            }
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }

            //数据表名称：list.DataTableName; 
            sql = $"select count(*) as 'index' from {param.TableName} where DataTag={param.DataTag} and DataState=2  {whereStr}";
             
            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
             
            return list.ToList();
        }

        public async Task<List<PrintdataBaseEntity>> GetPrintPageList(PrintdataBaseListParam param, Pagination pagination)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(param.TableName))
            {
                sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={param.Id} limit 1;";
                var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);
                param.TableName = itemtemp.DataTableName;
                param.MainTagName = itemtemp.MainTagName;
            }

            string whereStr = "";
            if (param.Id != -1)
            {
                whereStr += $" and CustomerProfileId={param.Id} ";
            }
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }

            //数据表名称：list.DataTableName; 
            sql = $"select * from {param.TableName} where DataTag={param.DataTag} and DataState=2  {whereStr}";


            sql += GetPaginationStr(pagination);
            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            int i = (pagination.PageIndex - 1) * pagination.PageSize + 1;
            foreach (var item in list)
            {
                item.Index = i;
                item.MainTagName = param.MainTagName;
                i++;
            }
            return list.ToList();
        }


        public async Task<List<PrintdataBaseEntity>> GetPrintTempPageListJson(PrintdataTempListParam param, Pagination pagination)
        {

            //数据表的后缀 param.Id;
            string temptablename = "collection_printdata_temp_" + param.Id;
            var tempTag = await this.BaseRepository().FindList<PrintdataTempEntity>($"select * from {temptablename}");
            if (tempTag.Count() <= 0)
            {
                return null;
            }
            string selectQuery = "";
            foreach (var item in tempTag)
            {
                //生成查询语句
                selectQuery += $" SELECT pd.*,'{item.MainTagName}' as MainTagName,s.EnglishInterpretation as RatingEnglish FROM {item.DataTableName} as pd left join collection_score as s on pd.Rating= s.score WHERE IDCode = '{item.IDCode}' union ";
            }
            selectQuery = selectQuery.Substring(0, selectQuery.Length - 6);



            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(selectQuery);
            int i = 1;
            foreach (var item in list)
            {
                item.Index = i;
                i++;
            }
            return list.ToList();
        }


        public async Task<List<PrintdataBaseEntity>> GetUnLockTempPageList(PrintdataTempListParam param, Pagination pagination)
        {


            //数据表的后缀 param.Id;
            string temptablename = "collection_printdata_temp_edit" + param.Id;
            var tempTag = await this.BaseRepository().FindList<PrintdataTempEntity>($"select * from {temptablename}");
            if (tempTag.Count() <= 0)
            {
                return null;
            }
            string selectQuery = "";
            foreach (var item in tempTag)
            {
                //生成查询语句
                selectQuery += $" SELECT *,'{item.DataTableName}' as TableName,'{item.MainTagName}' as MainTagName FROM {item.DataTableName} WHERE IDCode = '{item.IDCode}' union ";
            }
            selectQuery = selectQuery.Substring(0, selectQuery.Length - 6);



            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(selectQuery);
            int i = 1;
            foreach (var item in list)
            {
                item.Index = i;
                i++;
            }
            return list.ToList();
        }


        /// <summary>
        /// 获取指定打印类型的数据总条目
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<List<PrintdataBaseEntity>> GetPrintTypeDataListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            //获取当前用户id 查看是否是 管理员
            //增加用户验证，只查询当前用户的数据
            //获取当前用户id
            BaseExtensionEntity bee = new BaseExtensionEntity();
            bee.Create();
            //验证用户是否是超级管理员

            string whereStr = string.Empty;
            var exp = LinqExtensions.True<UserEntity>();
            exp = exp.And(t => t.Id == bee.BaseCreatorId);
            UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            if (user.IsSystem != 1) //不是超级管理员
            {
                whereStr += $" and cp.BaseCreatorId={user.Id}  ";
            }

            //表名
            if (!string.IsNullOrEmpty(param.MainTagName) && param.MainTagName != "-1")
            {
                whereStr += $" and pdmt.id= '{param.MainTagName}' ";
            }


            string sql = @"SELECT distinct DataTableName, MainTagName FROM collection_pringdatamaintag AS pdmt
JOIN collection_customerprofile AS cp ON cp.PrintDataId = pdmt.Id
WHERE cp.BaseIsDelete = 0  ";
            string selectTableSql = sql + whereStr;

            var pdmt = await this.BaseRepository().FindList<PringdatamaintagEntity>(selectTableSql);



            whereStr = " where DataState = 2 ";
            //是那种数据
            if (param.DataTag > 0)
            {
                whereStr += $"  and DataTag={param.DataTag} ";
            } 
            //标签类型
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }

            sql = "select count(*) as 'Index' from (";
            foreach (var item in pdmt)
            {
                string tempsql = $"select *  from {item.DataTableName} ";
                sql += tempsql + whereStr + " union ";
            }

            sql = sql.Substring(0, sql.Length - 6);
            sql += ")  as t ";
             
            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            return list.ToList();
        }

        /// <summary>
        /// 获取指定打印类型的数据
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<List<PrintdataBaseEntity>> GetPrintTypeDataList(PrintdataBaseListParam param, Pagination pagination)
        {
            //获取当前用户id 查看是否是 管理员
            //增加用户验证，只查询当前用户的数据
            //获取当前用户id
            BaseExtensionEntity bee = new BaseExtensionEntity();
            bee.Create();
            //验证用户是否是超级管理员

            string whereStr = string.Empty;
            var exp = LinqExtensions.True<UserEntity>();
            exp = exp.And(t => t.Id == bee.BaseCreatorId);
            UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            if (user.IsSystem != 1) //不是超级管理员
            {
                whereStr += $" and cp.BaseCreatorId={user.Id}  ";
            }

            //表名
            if (!string.IsNullOrEmpty(param.MainTagName) && param.MainTagName != "-1")
            {
                whereStr += $" and pdmt.id= '{param.MainTagName}' ";
            }


            string sql = @"SELECT distinct DataTableName, MainTagName FROM collection_pringdatamaintag AS pdmt
JOIN collection_customerprofile AS cp ON cp.PrintDataId = pdmt.Id
WHERE cp.BaseIsDelete = 0  ";
            string selectTableSql = sql + whereStr;

            var pdmt = await this.BaseRepository().FindList<PringdatamaintagEntity>(selectTableSql);



            whereStr = " where DataState = 2 ";
            //是那种数据
            if (param.DataTag > 0)
            {
                whereStr += $"  and DataTag={param.DataTag} ";
            } 
            //标签类型
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }

            sql = "select * from (";
            foreach (var item in pdmt)
            {
                string tempsql = $"select *,'表{item.MainTagName}' as MainTagName,'{item.DataTableName}' as TableName  from {item.DataTableName} ";
                sql += tempsql + whereStr + " union ";
            }

            sql = sql.Substring(0, sql.Length - 6);
            sql += ")  as t ";

            sql += GetPaginationStr(pagination);
            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            int i = (pagination.PageIndex-1) * pagination.PageSize +1;
            foreach (var item in list)
            {
                item.Index = i;
                i++;
            }
            return list.ToList();
        }





        /// <summary>
        /// 从数据基础表中 获取指定 打印类型的数据总条目
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<List<PrintdataBaseEntity>> GetDatabaseTypeDataListCount(PrintdataBaseListParam param, Pagination pagination)
        {
            //获取当前用户id 查看是否是 管理员
            //增加用户验证，只查询当前用户的数据
            //获取当前用户id
            BaseExtensionEntity bee = new BaseExtensionEntity();
            bee.Create();
            //验证用户是否是超级管理员


            string whereStr = string.Empty;
            var exp = LinqExtensions.True<UserEntity>();
            exp = exp.And(t => t.Id == bee.BaseCreatorId);
            UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            if (user.IsSystem != 1) //不是超级管理员
            {
                whereStr += $" and cp.BaseCreatorId={user.Id}  ";
            }

            //表名
            if (!string.IsNullOrEmpty(param.MainTagName) && param.MainTagName != "-1")
            {
                whereStr += $" and pdmt.id= '{param.MainTagName}' ";
            }


            string sql = @"SELECT distinct DataTableName, MainTagName FROM collection_pringdatamaintag AS pdmt
JOIN collection_customerprofile AS cp ON cp.PrintDataId = pdmt.Id
WHERE cp.BaseIsDelete = 0  ";
            string selectTableSql = sql + whereStr;

            var pdmt = await this.BaseRepository().FindList<PringdatamaintagEntity>(selectTableSql);



            whereStr = " where (DataState = 0 or DataState=2) ";
            //是那种数据
            if (param.DataTag > 0)
            {
                whereStr += $"  and DataTag={param.DataTag} ";
            } 
            //标签类型
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }

            sql = "select count(*) as 'Index' from (";
            foreach (var item in pdmt)
            {
                string tempsql = $"select *  from {item.DataTableName} ";
                sql += tempsql + whereStr + " union ";
            }

            sql = sql.Substring(0, sql.Length - 6);
            sql += ")  as t ";

            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            return list.ToList();
        }

        /// <summary>
        /// 从数据基础表中 获取指定 打印类型的数据
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<List<PrintdataBaseEntity>> GetDatabaseTypeDataList(PrintdataBaseListParam param, Pagination pagination)
        {

            //获取当前用户id 查看是否是 管理员
            //增加用户验证，只查询当前用户的数据
            //获取当前用户id
            BaseExtensionEntity bee = new BaseExtensionEntity();
            bee.Create();
            //验证用户是否是超级管理员

            string whereStr = string.Empty;
            var exp = LinqExtensions.True<UserEntity>();
            exp = exp.And(t => t.Id == bee.BaseCreatorId);
            UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            if (user.IsSystem != 1) //不是超级管理员
            {
                whereStr += $" and cp.BaseCreatorId={user.Id}  ";
            }

            //表名
            if (!string.IsNullOrEmpty(param.MainTagName) && param.MainTagName != "-1")
            {
                whereStr += $" and pdmt.id= '{param.MainTagName}' ";
            }


            string sql = @"SELECT distinct DataTableName, MainTagName FROM collection_pringdatamaintag AS pdmt
JOIN collection_customerprofile AS cp ON cp.PrintDataId = pdmt.Id
WHERE cp.BaseIsDelete = 0  ";
            string selectTableSql = sql + whereStr;

            var pdmt = await this.BaseRepository().FindList<PringdatamaintagEntity>(selectTableSql);



            whereStr = " where (DataState = 0 or DataState=2)  ";
            //是那种数据
            if (param.DataTag > 0)
            {
                whereStr += $"  and DataTag={param.DataTag} ";
            } 
            //标签类型
            if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            {
                whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            }
            if (!string.IsNullOrEmpty(param.CustomerName) && param.CustomerName != "-1")
            {
                whereStr += $" and CustomerName='{param.CustomerName}'";
            }

            if (!string.IsNullOrEmpty(param.IDCode) && param.IDCode != "-1")
            {
                whereStr += $" and IDCode='{param.IDCode}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionName) && param.CollectionName != "-1")
            {
                whereStr += $" and CollectionName='{param.CollectionName}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionYear) && param.CollectionYear != "-1")
            {
                whereStr += $" and CollectionYear='{param.CollectionYear}'";
            }

            if (!string.IsNullOrEmpty(param.CollectionValue) && param.CollectionValue != "-1")
            {
                whereStr += $" and CollectionValue='{param.CollectionValue}'";
            }


            sql = "select * from (";
            foreach (var item in pdmt)
            {
                string tempsql = $"select *,'表{item.MainTagName}' as MainTagName,'{item.DataTableName}' as TableName  from {item.DataTableName} ";
                sql += tempsql + whereStr + " union ";
            }

            sql = sql.Substring(0, sql.Length - 6);
            sql += ")  as t     ";

            sql += GetPaginationStr(pagination);
            //序号
            var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            int i = (pagination.PageIndex - 1) * pagination.PageSize + 1;
            foreach (var item in list)
            {
                item.Index = i;
                i++;
            }
            return list.ToList();

            //string whereStr = "\"  (DataState = 0 or DataState=2)  ";

            ////是那种数据
            //if (param.DataTag > 0)
            //{
            //    whereStr += $" and DataTag={param.DataTag} ";
            //}
            ////用户名
            //if (!string.IsNullOrEmpty(param.CustomerName))
            //{
            //    whereStr += $" and CustomerName='{param.CustomerName}'";
            //}
            ////标签类型
            //if (!string.IsNullOrEmpty(param.TagtypeName) && param.TagtypeName != "-1")
            //{
            //    whereStr += $" and TagTypeName='{param.TagtypeName}' ";
            //}







            ////获取当前用户id 查看是否是 管理员
            ////增加用户验证，只查询当前用户的数据
            ////获取当前用户id
            //BaseExtensionEntity bee = new BaseExtensionEntity();
            //bee.Create();
            ////验证用户是否是超级管理员

            //var exp = LinqExtensions.True<UserEntity>();
            //exp = exp.And(t => t.Id == bee.BaseCreatorId);
            //UserEntity user = await this.BaseRepository().FindEntity<UserEntity>(exp);
            //if (user.IsSystem != 1) //不是超级管理员
            //{
            //    whereStr += $" and BaseCreatorId={user.Id} \",{user.Id}, ";
            //}
            //else
            //{
            //    whereStr += "\",null,";

            //}

            ////表名
            //if (!string.IsNullOrEmpty(param.MainTagName) && param.MainTagName != "-1")
            //{
            //    whereStr += $" '{param.MainTagName}' ";
            //}
            //else
            //{
            //    whereStr += $" null ";
            //}

            //string sql = $" CALL  FindPrintData({whereStr});";



            ////序号
            //var list = await this.BaseRepository().FindList<PrintdataBaseEntity>(sql);
            //int i = 1;
            //foreach (var item in list)
            //{
            //    item.Index = i;
            //    i++;
            //}
            //return list.ToList();
        }

        public async Task<PrintdataBaseEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<PrintdataBaseEntity>(id);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据 将数据状态变成2 打印
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task SaveForm(PrintdataBaseSaveParam entity)
        {
            PrintdataBaseEntity user = new PrintdataBaseEntity();
            user.Create();
            //获取数据表名 
            string sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={entity.Id} limit 1;";
            var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);


            // 构建 SQL 更新语句
            sql = string.Empty;
            foreach (var item in entity.SaveData)
            {
                sql += $"UPDATE " + itemtemp.DataTableName + " as c " +
                             $"SET CustomerName = N'{item.CustomerName}', " +
                             $"IDCode = N'{item.IDCode}', " +
                             $"CollectionYear = N'{item.CollectionYear}', " +
                             $"CollectionValue = N'{item.CollectionValue}', " +
                             $"CollectionName = N'{item.CollectionName}', " +
                             $"PrintArt = N'{item.PrintArt}', " +
                             $"SerialCode = N'{item.SerialCode}', " +
                             $"Rating = N'{item.Rating}', " +
                             $"HQP = N'{item.HQP}', " +
                             $"StarTag = N'{item.StarTag}', " +
                             $"EditionPersonalization = N'{item.EditionPersonalization}', " +
                             $"EstimatedValue = N'{item.EstimatedValue}', " +
                             $"Rarity = N'{item.Rarity}', " +
                             $"IssuingUnit = N'{item.IssuingUnit}', " +
                             $"OS = N'{item.OS}', " +
                             $"Description = N'{item.Description}', " +
                             $"Edition = N'{item.Edition}', " +
                             $"NumberCode = N'{item.NumberCode}', " +
                             $"Personalization = N'{item.Personalization}', " +
                             $"Material = N'{item.Material}', " +
                             $"Weight = N'{item.Weight}', " +
                             $"Size = N'{item.Size}', " +
                             $"VirginRubber = N'{item.VirginRubber}', " +
                             $"AppraiserName = N'{item.AppraiserName}', " +
                             $"TagTypeName = N'{item.TagTypeName}', " +
                             $"PrintStyleName = N'{item.PrintStyleName}', " +
                             $"BigDataTag = N'{item.BigDataTag}', " +
                             $"DataState = 2 ," +
                             $"BaseModifyTime =now(), " +
                             $"BaseModifierId = N'{user.BaseModifierId}', " +
                             $"BaseVersion = N'{item.BaseVersion + 1}', " +
                             $"PrintStyleID = (select Id from collection_printstyle where PrintStyleName=c.PrintStyleName)," +
                             $"TagTypeID = (select Id from collection_tagtype where  TagTypeName=c.TagTypeName)  " +
                             $"WHERE Id = {item.Id}; ";
            }
            int count = await this.BaseRepository().ExecuteBySql(sql);
            if (count > 0)
            {

            }
        }

        /// <summary>
        /// 暂存数据 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task SaveTempForm(PrintdataBaseSaveParam entity)
        {
            PrintdataBaseEntity user = new PrintdataBaseEntity();
            await user.Create();
            //获取数据表名 
            string sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={entity.Id} limit 1;";
            var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);


            // 构建 SQL 更新语句
            sql = string.Empty;
            foreach (var item in entity.SaveData)
            {
                sql += $"UPDATE " + itemtemp.DataTableName + " as c " +
                             $"SET CustomerName = N'{item.CustomerName}', " +
                             $"IDCode = N'{item.IDCode}', " +
                             $"CollectionYear = N'{item.CollectionYear}', " +
                             $"CollectionValue = N'{item.CollectionValue}', " +
                             $"CollectionName = N'{item.CollectionName}', " +
                             $"PrintArt = N'{item.PrintArt}', " +
                             $"SerialCode = N'{item.SerialCode}', " +
                             $"Rating = N'{item.Rating}', " +
                             $"HQP = N'{item.HQP}', " +
                             $"StarTag = N'{item.StarTag}', " +
                             $"EditionPersonalization = N'{item.EditionPersonalization}', " +
                             $"EstimatedValue = N'{item.EstimatedValue}', " +
                             $"Rarity = N'{item.Rarity}', " +
                             $"IssuingUnit = N'{item.IssuingUnit}', " +
                             $"OS = N'{item.OS}', " +
                             $"Description = N'{item.Description}', " +
                             $"Edition = N'{item.Edition}', " +
                             $"NumberCode = N'{item.NumberCode}', " +
                             $"Personalization = N'{item.Personalization}', " +
                             $"Material = N'{item.Material}', " +
                             $"Weight = N'{item.Weight}', " +
                             $"Size = N'{item.Size}', " +
                             $"BigDataTag = N'{item.BigDataTag}', " +
                             $"AppraiserName = N'{item.AppraiserName}', " +
                             $"TagTypeName = N'{item.TagTypeName}', " +
                             $"PrintStyleName = N'{item.PrintStyleName}', " +
                             $"BaseModifyTime = now(), " +
                             $"BaseModifierId = N'{user.BaseModifierId}', " +
                             $"BaseVersion = N'{item.BaseVersion + 1}', " +
                             $"PrintStyleID = (select Id from collection_printstyle where PrintStyleName=c.PrintStyleName)," +
                             $"TagTypeID = (select Id from collection_tagtype where  TagTypeName=c.TagTypeName)  " +
                             $"WHERE Id = {item.Id}; ";
            }
            int count = await this.BaseRepository().ExecuteBySql(sql);
            if (count > 0)
            {

            }
        }

        /// <summary>
        /// 保存并打印数据 将数据状态变成0锁定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task SaveAndPrintForm(PrintdataBaseSaveParam entity)
        {
            PrintdataBaseEntity user = new PrintdataBaseEntity();
            user.Create();
            //获取数据表名 
            string sql = $"SELECT pdmt.* FROM collection_customerprofile  as cp left join collection_pringdatamaintag  as pdmt on cp.PrintDataId=pdmt.Id where cp.Id={entity.Id} limit 1;";
            var itemtemp = await this.BaseRepository().FindObject<PringdatamaintagEntity>(sql);
            if (itemtemp == null)
            {
                entity.State = false;
                entity.ParamMsg = "无法获取数据档案！请联系管理员";
                return;
            }
            //查询模版对应的打印地址
            var expression = LinqExtensions.True<TagtypeEntity>();
            expression = expression.And(t => t.TagTypeName == entity.SaveData[0].TagTypeName);
            TagtypeEntity tagtypeEntity = await this.BaseRepository().FindEntity<TagtypeEntity>(expression);
            if (tagtypeEntity == null)
            {
                entity.State = false;
                entity.ParamMsg = "样式模版错误！请联系管理员";
                return;
            }


            // 构建 SQL 更新语句
            sql = string.Empty;
            foreach (var item in entity.SaveData)
            {
                sql += $"UPDATE " + itemtemp.DataTableName + " as c " +
                             $"SET CustomerName = N'{item.CustomerName}', " +
                             $"IDCode = N'{item.IDCode}', " +
                             $"CollectionYear = N'{item.CollectionYear}', " +
                             $"CollectionValue = N'{item.CollectionValue}', " +
                             $"CollectionName = N'{item.CollectionName}', " +
                             $"PrintArt = N'{item.PrintArt}', " +
                             $"SerialCode = N'{item.SerialCode}', " +
                             $"Rating = N'{item.Rating}', " +
                             $"HQP = N'{item.HQP}', " +
                             $"StarTag = N'{item.StarTag}', " +
                             $"EditionPersonalization = N'{item.EditionPersonalization}', " +
                             $"EstimatedValue = N'{item.EstimatedValue}', " +
                             $"Rarity = N'{item.Rarity}', " +
                             $"IssuingUnit = N'{item.IssuingUnit}', " +
                             $"OS = N'{item.OS}', " +
                             $"Description = N'{item.Description}', " +
                             $"Edition = N'{item.Edition}', " +
                             $"NumberCode = N'{item.NumberCode}', " +
                             $"Personalization = N'{item.Personalization}', " +
                             $"Material = N'{item.Material}', " +
                             $"Weight = N'{item.Weight}', " +
                             $"Size = N'{item.Size}', " +
                             $"VirginRubber = N'{item.VirginRubber}', " +
                             $"AppraiserName = N'{item.AppraiserName}', " +
                             $"TagTypeName = N'{item.TagTypeName}', " +
                             $"PrintStyleName = N'{item.PrintStyleName}', " +
                             $"BigDataTag = N'{item.BigDataTag}', " +
                             $"DataState = 0 ," +
                             $"BaseModifyTime =now(), " +
                             $"BaseModifierId = N'{user.BaseModifierId}', " +
                             $"BaseVersion = N'{item.BaseVersion + 1}', " +
                             $"PrintStyleID = (select Id from collection_printstyle where PrintStyleName=c.PrintStyleName)," +
                             $"TagTypeID = (select Id from collection_tagtype where  TagTypeName=c.TagTypeName)  " +
                             $"WHERE Id = {item.Id}; ";
            }
            int count = await this.BaseRepository().ExecuteBySql(sql);
            //将数据插入打印临时表，在返回值中增加 临时表id
            if (count > 0)
            {
                //创建临时表
                string tableTempName = user.BaseModifierId + DateTime.Now.ToString("yyyyMMddHHmmss");
                string createTableSql = GetCreateTableSql(tableTempName);
                await this.BaseRepository().ExecuteBySql(createTableSql);

                //将 entity.SaveData 数据 存储到 collection_printdata_temp_tableTempName 表中]
                string insertSql = string.Empty;
                foreach (var item in entity.SaveData)
                {
                    item.TableName = itemtemp.DataTableName;
                    insertSql += GenerateInsertSql(tableTempName, item);
                }
                await this.BaseRepository().ExecuteBySql(insertSql);
                entity.State = true;
                entity.ParamMsg = $"{tagtypeEntity.TagTypeAddr}?id={tableTempName}";
            }
        }

        /// <summary>
        /// 解锁状态保存 将数据状态变成1编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UnLockSaveForm(PrintdataBaseSaveParam entity)
        {
            PrintdataBaseEntity user = new PrintdataBaseEntity();
            user.Create();

            // 构建 SQL 更新语句
            string sql = string.Empty;
            foreach (var item in entity.SaveData)
            {
                sql += $"UPDATE " + item.TableName + " as c " +
                             $"SET DataState = 1 ," +
                             $"BaseModifyTime =now(), " +
                             $"BaseModifierId = N'{user.BaseModifierId}', " +
                             $"BaseVersion = N'{item.BaseVersion + 1}' " +
                             $"WHERE Id = {item.Id}; ";
            }
            int count = await this.BaseRepository().ExecuteBySql(sql);
            if (count > 0)
            {
                //将数据插入到临时表中

                //创建临时表
                string tableTempName = user.BaseModifierId + DateTime.Now.ToString("yyyyMMddHHmmss");
                string createTableSql = GetCreateTableSql(tableTempName, "collection_printdata_temp_edit");
                await this.BaseRepository().ExecuteBySql(createTableSql);

                //将 entity.SaveData 数据 存储到 collection_printdata_temp_tableTempName 表中]
                string insertSql = string.Empty;
                foreach (var item in entity.SaveData)
                {
                    insertSql += GenerateInsertSql(tableTempName, item, "collection_printdata_temp_edit");
                }
                await this.BaseRepository().ExecuteBySql(insertSql);
                entity.State = true;
                entity.ParamMsg = tableTempName;


            }
        }
        /// <summary>
        /// 解锁状态保存 将数据状态变成0锁定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task LockSaveForm(PrintdataBaseSaveParam entity)
        {
            PrintdataBaseEntity user = new PrintdataBaseEntity();
            user.Create();

            // 构建 SQL 更新语句
            string sql = string.Empty;
            foreach (var item in entity.SaveData)
            {
                sql += $"UPDATE " + item.TableName + " as c " +
                             $"SET CustomerName = N'{item.CustomerName}', " +
                             $"IDCode = N'{item.IDCode}', " +
                             $"CollectionYear = N'{item.CollectionYear}', " +
                             $"CollectionValue = N'{item.CollectionValue}', " +
                             $"CollectionName = N'{item.CollectionName}', " +
                             $"PrintArt = N'{item.PrintArt}', " +
                             $"SerialCode = N'{item.SerialCode}', " +
                             $"Rating = N'{item.Rating}', " +
                             $"HQP = N'{item.HQP}', " +
                             $"StarTag = N'{item.StarTag}', " +
                             $"EditionPersonalization = N'{item.EditionPersonalization}', " +
                             $"EstimatedValue = N'{item.EstimatedValue}', " +
                             $"Rarity = N'{item.Rarity}', " +
                             $"IssuingUnit = N'{item.IssuingUnit}', " +
                             $"OS = N'{item.OS}', " +
                             $"Description = N'{item.Description}', " +
                             $"Edition = N'{item.Edition}', " +
                             $"NumberCode = N'{item.NumberCode}', " +
                             $"Personalization = N'{item.Personalization}', " +
                             $"Material = N'{item.Material}', " +
                             $"Weight = N'{item.Weight}', " +
                             $"BigDataTag = N'{item.BigDataTag}', " +
                             $"Size = N'{item.Size}', " +
                             $"VirginRubber = N'{item.VirginRubber}', " +
                             $"AppraiserName = N'{item.AppraiserName}', " +
                             $"TagTypeName = N'{item.TagTypeName}', " +
                             $"PrintStyleName = N'{item.PrintStyleName}', " +
                             $"DataState = 0 ," +
                             $"BaseModifyTime = now(), " +
                             $"BaseModifierId = N'{user.BaseModifierId}', " +
                             $"BaseVersion = N'{item.BaseVersion + 1}', " +
                             $"PrintStyleID = (select Id from collection_printstyle where PrintStyleName=c.PrintStyleName)," +
                             $"TagTypeID = (select Id from collection_tagtype where  TagTypeName=c.TagTypeName)  " +
                             $"WHERE Id = {item.Id}; ";
            }
            int count = await this.BaseRepository().ExecuteBySql(sql);
            if (count > 0)
            {

            }
        }



        /// <summary>
        /// 打印数据 将数据状态变成0锁定
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task LockAndPrintForm(PrintdataBaseSaveParam entity)
        {
            PrintdataBaseEntity user = new PrintdataBaseEntity();
            user.Create();
            //检查模版对应的打印地址
            var expression = LinqExtensions.True<TagtypeEntity>();
            expression = expression.And(t => t.TagTypeName == entity.SaveData[0].TagTypeName);
            TagtypeEntity tagtypeEntity = await this.BaseRepository().FindEntity<TagtypeEntity>(expression);
            if (tagtypeEntity == null)
            {
                entity.State = false;
                entity.ParamMsg = "样式模版错误！请联系管理员";
                return;
            }


            // 构建 SQL 更新语句
            string sql = string.Empty;
            foreach (var item in entity.SaveData)
            {
                sql += $"UPDATE " + item.TableName + " as c " +
                             $"SET  " +
                             $"DataState = 0 ," +
                             $"BaseModifyTime = now(), " +
                             $"BaseModifierId = N'{user.BaseModifierId}', " +
                             $"BaseVersion = N'{item.BaseVersion + 1}' " +
                              $"WHERE Id = {item.Id}; ";
            }
            int count = await this.BaseRepository().ExecuteBySql(sql);
            //将数据插入打印临时表，在返回值中增加 临时表id
            if (count > 0)
            {
                //创建临时表
                string tableTempName = user.BaseModifierId + DateTime.Now.ToString("yyyyMMddHHmmss");
                string createTableSql = GetCreateTableSql(tableTempName);
                await this.BaseRepository().ExecuteBySql(createTableSql);

                //将 entity.SaveData 数据 存储到 collection_printdata_temp_tableTempName 表中]
                string insertSql = string.Empty;
                foreach (var item in entity.SaveData)
                {
                    insertSql += GenerateInsertSql(tableTempName, item);
                }
                await this.BaseRepository().ExecuteBySql(insertSql);
                entity.State = true;
                entity.ParamMsg = $"{tagtypeEntity.TagTypeAddr}?id={tableTempName}";
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<PrintdataBaseEntity>(idArr);
        }
        #endregion

        #region 私有方法


        public string GetPaginationStr(Pagination pagination)
        {
            StringBuilder sb = new StringBuilder();
            if (pagination.PageIndex == 0)
            {
                pagination.PageIndex = 1;
            }
            int num = (pagination.PageIndex - 1) * pagination.PageSize;
            string orderBy = string.Empty;

            if (!string.IsNullOrEmpty(pagination.Sort))
            {
                if (pagination.SortType.ToUpper().IndexOf("ASC") + pagination.SortType.ToUpper().IndexOf("DESC") > 0)
                {
                    orderBy = " ORDER BY " + pagination.Sort;
                }
                else
                {
                    orderBy = " ORDER BY " + pagination.Sort + " DESC";
                }
            }
            sb.Append(orderBy);
            sb.Append(" LIMIT " + num + "," + pagination.PageSize + "");
            return sb.ToString();
        }


        /// <summary>
        /// 获取建表sql
        /// </summary>
        /// <param name="tempTableName">数据表的后缀  前缀是collection_printdata_temp_</param>
        /// <returns></returns>
        private string GetCreateTableSql(string tempTableName, string tablePrefix = "collection_printdata_temp_")
        {
            string baseSql = @"CREATE TABLE `{1}{0}` (
  `Id` bigint(20) NOT NULL,
  `IDCode` varchar(64) COLLATE utf8_bin DEFAULT NULL COMMENT '鉴定编号',
  `MainTagID` bigint(20) DEFAULT NULL COMMENT '表ID',
  `MainTagName` varchar(128) COLLATE utf8_bin DEFAULT NULL COMMENT '表名',
  `DataTableName` varchar(128) COLLATE utf8_bin DEFAULT NULL COMMENT '数据表名称',
  `BaseIsDelete` int(11) NOT NULL COMMENT '是否删除',
  `BaseCreateTime` datetime NOT NULL COMMENT '创建时间',
  `BaseModifyTime` datetime NOT NULL COMMENT '修改时间',
  `BaseCreatorId` bigint(20) NOT NULL COMMENT '创建人',
  `BaseModifierId` bigint(20) NOT NULL COMMENT '修改人',
  `BaseVersion` int(11) NOT NULL COMMENT '数据版本(每次更新+1)',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='基础打印表';
";
            string sql = string.Format(baseSql, tempTableName, tablePrefix);
            return sql;
        }

        /// <summary>
        /// 获取插入数据 sql语句
        /// </summary>
        /// <param name="tempTableName"></param>
        /// <param name="data"></param>
        /// <param name="SaveBaseTable"></param>
        /// <returns></returns>
        private string GenerateInsertSql(string tempTableName, PrintdataBaseEntity data, string tablePrefix = "collection_printdata_temp_")
        {
            var sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO `{12}{11}` (" +
                "`Id`, `IDCode`, `MainTagID`, `MainTagName`, `DataTableName`, `BaseIsDelete`, " +
                "`BaseCreateTime`, `BaseModifyTime`, `BaseCreatorId`, `BaseModifierId`, `BaseVersion`) " +
                "VALUES ({0},{1},{2},{3},{4},{5}, " +
                "'{6}', '{7}', {8}, {9}, {10});",
                data.Id,
                FormatValue(data.IDCode),
                   data.CustomerProfileId,
                FormatValue(data.MainTagName),
                FormatValue(data.TableName),
                data.BaseIsDelete == null ? "0" : data.BaseIsDelete,
                FormatDate(data.BaseCreateTime),
                FormatDate(data.BaseModifyTime),
                data.BaseCreatorId,
                data.BaseModifierId,
                data.BaseVersion,
                tempTableName, tablePrefix
            );

            return sb.ToString();
        }
        private static string FormatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd HH:mm:ss"); // 使用自定义格式
            }
            else
            {
                return ""; // 当为 null 时返回空字符串
            }
        }
        private static string FormatValue(object value)
        {
            return value == null ? "NULL" : $"'{value.ToString().Replace("'", "''")}'";
        }
        private string ListFilter(PrintdataBaseListParam param)
        {
            string whereSqlStr = "";
            if (param != null)
            {
            }
            return whereSqlStr;
        }
        #endregion
    }
}
