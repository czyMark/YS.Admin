using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Entity;
using YS.Admin.Model;
using YS.Admin.Web.Controllers;
using YS.Admin.Entity.CollectionManage;
using YS.Admin.Business.CollectionManage;
using YS.Admin.Model.Param.CollectionManage;
using System.Linq.Expressions;
using NPOI.POIFS.Crypt.Dsig;
using Newtonsoft.Json;
using TagLib.Mpeg;
using UglyToad.PdfPig.Content;
using System.Reflection;
using YS.Admin.Entity.Portal;
using YS.Admin.Business.Portal;
using YS.Admin.Model.Param.Portal;

namespace YS.Admin.Web.Areas.CollectionManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2024-07-17 16:49
    /// 描 述：基础打印数据表控制器类
    /// </summary>
    [Area("CollectionManage")]
    public class PrintdataBaseController : BaseController
    {
        private PrintdataBaseBLL printdataBaseBLL = new PrintdataBaseBLL();
        private AppraiserBLL appraiserBLL = new AppraiserBLL();
        private PrintstyleBLL printstyleBLL = new PrintstyleBLL();
        private TagtypeBLL tagtypeBLL = new TagtypeBLL();



        private BigdataCategoryBLL bigdataCategoryBLL = new BigdataCategoryBLL();
        private BigdataTagBLL bigdataTagBLL = new BigdataTagBLL();


        #region 视图功能
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">档案ID</param>
        /// <param name="datatag"></param>
        /// <returns></returns>
        [AuthorizeFilter("print:printdata:base:view")]
        public ActionResult PrintdataBaseIndex(long id, int datatag)
        {
            ViewBag.DataId = id;
            ViewBag.DataTag = datatag;

            ViewBag.DataTableColumns = GetDataTableColumns(datatag,true,false);
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">档案ID</param>
        /// <param name="datatag"></param>
        /// <returns></returns> 
        public ActionResult PrintdataBasePrint(long id, int datatag)
        {
            ViewBag.DataId = id;
            ViewBag.DataTag = datatag;

            ViewBag.DataTableColumns = GetDataTableColumns(datatag, true, false);
            return View();
        }




        /// <summary>
        /// 数据库数据-纸币
        /// </summary> 
        /// <returns></returns>
        [AuthorizeFilter("print:printdatabase:banknote:view")]
        public ActionResult PrintDataBaseBanknote()
        {
            ViewBag.DataTag = 1;
            ViewBag.DataTableColumns = GetDataTableColumns(1, true, true);
            return View();
        }


        /// <summary>
        /// 数据库数据-邮票
        /// </summary> 
        /// <returns></returns>
        [AuthorizeFilter("print:printdatabase:stamp:view")]
        public ActionResult PrintdataBaseStamp()
        {
            ViewBag.DataTag = 2;
            ViewBag.DataTableColumns = GetDataTableColumns(2, true, true);
            return View();
        }


        /// <summary>
        /// 数据库数据-硬币
        /// </summary> 
        /// <returns></returns>
        [AuthorizeFilter("print:printdatabase:coin:view")]
        public ActionResult PrintdataBaseCoin()
        {
            ViewBag.DataTag = 3;

            ViewBag.DataTableColumns = GetDataTableColumns(3, true, true);
            return View();
        }

        /// <summary>
        /// 修改 已解锁的数据
        /// </summary>
        /// <param name="id">临时表id</param>
        /// <param name="datatag"></param>
        /// <returns></returns>

        public ActionResult PrintdataBaseFrom(string id, int datatag)
        {
            ViewBag.DataTempId = id;
            ViewBag.DataTag = datatag;

            ViewBag.DataTableColumns = GetDataTableColumns(datatag, false, true);
            return View();
        }
        /// <summary>
        /// 纸币打印界面
        /// </summary> 
        /// <returns></returns>
        [AuthorizeFilter("print:printdata:banknote:view")]
        public ActionResult PrintdataBanknote()
        {
            ViewBag.DataTag = 1;

            ViewBag.DataTableColumns = GetDataTableColumns(1, true, true);
            return View();
        }


        /// <summary>
        /// 邮票打印界面
        /// </summary> 
        /// <returns></returns>
        [AuthorizeFilter("print:printdata:stamp:view")]
        public ActionResult PrintdataStamp()
        {
            ViewBag.DataTag = 2;

            ViewBag.DataTableColumns = GetDataTableColumns(2, true, true);
            return View();
        }


        /// <summary>
        /// 硬币打印界面
        /// </summary> 
        /// <returns></returns>
        [AuthorizeFilter("print:printdata:coin:view")]
        public ActionResult PrintdataCoin()
        {
            ViewBag.DataTag = 3;

            ViewBag.DataTableColumns = GetDataTableColumns(3, true, true);
            return View();
        }



        public ActionResult PrintdataBaseForm()
        {
            return View();
        }
        #endregion

        #region 获取数据 

        [HttpGet]
        [AuthorizeFilter("print:printdatabase:search")]
        public async Task<ActionResult> GetPageListJson(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("print:printdatabase:search")]
        public async Task<ActionResult> GetPageListCountJson(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPageListCount(param, pagination);
            return Json(obj);
        }


        [HttpGet]
        [AuthorizeFilter("print:printdatabaseprint:view")]
        public async Task<ActionResult> GetGetDataTableColumnsJson(PrintdataBaseListParam param)
        {
            return Json(GetDataTableColumns(param.DataTag, true, true));
        }


        [HttpGet]
        [AuthorizeFilter("print:printdatabaseunlocktemp:search")]
        public async Task<ActionResult> GetUnLockTempPageListJson(PrintdataTempListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetUnLockTempPageList(param, pagination);
            return Json(obj);
        }




        [HttpGet]
        [AuthorizeFilter("print:printdatabaseprint:search")]
        public async Task<ActionResult> GetPrintPageListJson(PrintdataBaseListParam param, Pagination pagination)
        {
            pagination.Sort = " IDCode ";
            pagination.SortType = "desc";
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPrintPageList(param, pagination);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("print:printdatabaseprint:search")]
        public async Task<ActionResult> GetPrintPageListCountJson(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPrintPageListCount(param, pagination);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("print:printtype:data:search")]
        public async Task<ActionResult> GetPrintTypeDataListJson(PrintdataBaseListParam param, Pagination pagination)
        {
            pagination.Sort = " IDCode ";
            pagination.SortType = "desc";
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPrintTypeDataList(param, pagination);
            return Json(obj);
        }
        [HttpGet] 
        public async Task<ActionResult> GetPrintTypeDataListCountJson(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetPrintTypeDataListCount(param, pagination);
            return Json(obj);
        }
        [HttpGet]
        public async Task<ActionResult> GetDatabaseTypeDataListCountJson(PrintdataBaseListParam param, Pagination pagination)
        {
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetDatabaseTypeDataListCount(param, pagination);
            return Json(obj);
        }
        [HttpGet]
        [AuthorizeFilter("print:database:type:search")]
        public async Task<ActionResult> GetDatabaseTypeDataListJson(PrintdataBaseListParam param, Pagination pagination)
        {
            pagination.Sort = " IDCode ";
            pagination.SortType = "desc";
            TData<List<PrintdataBaseEntity>> obj = await printdataBaseBLL.GetDatabaseTypeDataList(param, pagination);
            return Json(obj);
        }
        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<PrintdataBaseEntity> obj = await printdataBaseBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据

        [HttpPost]
        [AuthorizeFilter("print:printdatabase:savetemp")]
        public async Task<ActionResult> SaveTempFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.SaveTempForm(entity);
            return Json(obj);
        }
        [HttpPost]
        [AuthorizeFilter("print:printdatabase:save")]
        public async Task<ActionResult> SavePrintFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("print:printdatabase:saveprint")]
        public async Task<ActionResult> SaveAndPrintFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.SaveAndPrintForm(entity);
            return Json(obj);
        }


        [HttpPost]
        [AuthorizeFilter("print:printdatabase:locksave")]
        public async Task<ActionResult> LockSaveFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.LockSaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("print:printdatabase:banknote:unlocksave;print:printdatabase:stamp:unlocksave;print:printdatabase:coin:unlocksave")]
        public async Task<ActionResult> UnLockSaveFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.UnLockSaveForm(entity);
            return Json(obj);
        }



        /// <summary>
        ///锁定打印
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilter("print:printdatabase:bandnote:lockprint;print:printdatabase:stamp:lockprint;print:printdatabase:coin:lockprint")]
        public async Task<ActionResult> LockPrintFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.LockAndPrintForm(entity);
            return Json(obj);
        }
        /// <summary>
        /// 正常打印
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFilter("print:printdatabase:banknote:print;print:printdatabase:stamp:print;print:printdatabase:coin:print")]
        public async Task<ActionResult> PrintFormJson(PrintdataBaseSaveParam entity)
        {
            entity.SaveData = JsonConvert.DeserializeObject<List<PrintdataBaseEntity>>(entity.SaveDataString);
            TData<string> obj = await printdataBaseBLL.LockAndPrintForm(entity);
            return Json(obj);
        }

        #endregion

        #region 私有方法

        private string GetDataTableColumns(int datatag, bool isCheckbox, bool showMinTag)
        {
            string coloumnStr = string.Empty;
            if (isCheckbox)
            {
                coloumnStr += GetCheckboxColumnsStr("全选");
            }
            if (showMinTag)
            {
                coloumnStr += GetColumnsStr("MainTagName", "分表", true);
            }
            if (datatag == 1)
            {
                //组织纸币打印的表头
                coloumnStr += GetColumnsStr("Index", "序号", true);
                coloumnStr += GetColumnsStr("CustomerName", "姓名");
                coloumnStr += GetColumnsStr("IDCode", "鉴定编号", true);
                coloumnStr += GetColumnsStr("CollectionYear", "年份");
                coloumnStr += GetColumnsStr("CollectionValue", "面值");
                coloumnStr += GetColumnsStr("CollectionName", "藏品名称");
                coloumnStr += GetColumnsStr("PrintArt", "印刷工艺");
                coloumnStr += GetColumnsStr("SerialCode", "冠字编号");
                coloumnStr += GetColumnsStr("Rating", "评分");

                coloumnStr += GetColumnsStr("HQP", "HQP", false, true, "['是','否']");
                //coloumnStr += GetColumnsStr("StarTag", "三星", false, true, "['是','否']");

                coloumnStr += GetColumnsStr("StarTag", "三星");

                coloumnStr += GetColumnsStr("EditionPersonalization", "版别/个性化");
                coloumnStr += GetColumnsStr("EstimatedValue", "估值保价");
                coloumnStr += GetColumnsStr("Rarity", "珍稀度");
                coloumnStr += GetColumnsStr("IssuingUnit", "发行单位");


                #region 鉴定师
                {
                    Task<TData<List<AppraiserEntity>>> obj = appraiserBLL.GetList(new AppraiserListParam());

                    coloumnStr += GetComboxColumnsStr("AppraiserName", "鉴定师", obj.Result.Data, "AppraiserName");
                }
                #endregion

                #region 大数据模版
                {
                    //纸币查询的是栏目
                    Task<TData<List<BigdataCategoryEntity>>> obj = bigdataCategoryBLL.GetPageList(new BigdataCategoryListParam() { DataTag = "1" }, new Pagination() { SortType="asc", PageSize=int.MaxValue, PageIndex=1, Sort= "SortId" });
                    
                    coloumnStr += GetComboxColumnsStr("BigDataTag", "大数据标志", obj.Result.Data, "CategoryName");
                }
                #endregion

                #region 自定义模板
                {
                    Task<TData<List<PrintstyleEntity>>> obj = printstyleBLL.GetList(new PrintstyleListParam());
                    coloumnStr += GetComboxColumnsStr("PrintStyleName", "自定义模板", obj.Result.Data, "PrintStyleName"); 
                }
                #endregion

                #region 标签类型
                {
                    Task<TData<List<TagtypeEntity>>> obj = tagtypeBLL.GetList(new TagtypeListParam());

                    coloumnStr += GetComboxColumnsStr("TagTypeName", "标签类型", obj.Result.Data, "TagTypeName"); 
                }
                #endregion


            }
            else if (datatag == 2)
            {
                //组织邮票打印的表头 
                coloumnStr += GetColumnsStr("Index", "序号", true);
                coloumnStr += GetColumnsStr("CustomerName", "姓名", true);
                coloumnStr += GetColumnsStr("IDCode", "鉴定编号", true);
                coloumnStr += GetColumnsStr("CollectionName", "藏品名称");
                coloumnStr += GetColumnsStr("CollectionYear", "年份");
                coloumnStr += GetColumnsStr("CollectionValue", "面值");
                coloumnStr += GetColumnsStr("Rating", "评分");
                coloumnStr += GetColumnsStr("OS", "OS");
                coloumnStr += GetColumnsStr("VirginRubber", "原胶", false, true, "['是','否']");
                coloumnStr += GetColumnsStr("Description", "描述");
                coloumnStr += GetColumnsStr("Edition", "版别");
                coloumnStr += GetColumnsStr("NumberCode", "志号");
                coloumnStr += GetColumnsStr("Personalization", "个性化");
                coloumnStr += GetColumnsStr("EstimatedValue", "估值");
                coloumnStr += GetColumnsStr("Rarity", "珍稀度");
                coloumnStr += GetColumnsStr("IssuingUnit", "发行单位");
                 


                #region 鉴定师
                {
                    Task<TData<List<AppraiserEntity>>> obj = appraiserBLL.GetList(new AppraiserListParam());

                    coloumnStr += GetComboxColumnsStr("AppraiserName", "鉴定师", obj.Result.Data, "AppraiserName");
                }
                #endregion

                #region 大数据模版
                {
                    //邮票直接查询统计项
                    Task<TData<List<BigdataTagEntity>>> obj = bigdataTagBLL.GetPageList(new BigdataTagListParam() { DataTag = "2" }, new Pagination() { SortType = "asc", PageSize = int.MaxValue, PageIndex = 1, Sort = "SortId" });

                    coloumnStr += GetComboxColumnsStr("BigDataTag", "大数据标志", obj.Result.Data, "TagName1");
                }
                #endregion

                #region 自定义模板
                {
                    Task<TData<List<PrintstyleEntity>>> obj = printstyleBLL.GetList(new PrintstyleListParam());
                    coloumnStr += GetComboxColumnsStr("PrintStyleName", "自定义模板", obj.Result.Data, "PrintStyleName");
                }
                #endregion

                #region 标签类型
                {
                    Task<TData<List<TagtypeEntity>>> obj = tagtypeBLL.GetList(new TagtypeListParam());

                    coloumnStr += GetComboxColumnsStr("TagTypeName", "标签类型", obj.Result.Data, "TagTypeName");
                }
                #endregion
            }
            else if (datatag == 3)
            {
                //组织硬币打印的表头 
                coloumnStr += GetColumnsStr("Index", "序号", true);
                coloumnStr += GetColumnsStr("CustomerName", "姓名");
                coloumnStr += GetColumnsStr("IDCode", "鉴定编号", true);
                coloumnStr += GetColumnsStr("CollectionName", "藏品名称");
                coloumnStr += GetColumnsStr("CollectionYear", "年份");
                coloumnStr += GetColumnsStr("CollectionValue", "面值");
                coloumnStr += GetColumnsStr("Rating", "评分");
                coloumnStr += GetColumnsStr("OS", "OS");
                //coloumnStr += GetColumnsStr("OS", "OS", false, true, "['是','否']");
                coloumnStr += GetColumnsStr("Description", "描述");
                coloumnStr += GetColumnsStr("Edition", "版别");
                coloumnStr += GetColumnsStr("Material", "材质");
                coloumnStr += GetColumnsStr("Weight", "重量");
                coloumnStr += GetColumnsStr("Size", "尺寸");
                coloumnStr += GetColumnsStr("EstimatedValue", "估值保价");
                coloumnStr += GetColumnsStr("Rarity", "珍稀度");
                coloumnStr += GetColumnsStr("IssuingUnit", "发行单位");
                 
                 
                #region 鉴定师
                {
                    Task<TData<List<AppraiserEntity>>> obj = appraiserBLL.GetList(new AppraiserListParam());

                    coloumnStr += GetComboxColumnsStr("AppraiserName", "鉴定师", obj.Result.Data, "AppraiserName");
                }
                #endregion

                #region 大数据模版
                {
                    //硬币直接查询统计项
                    Task<TData<List<BigdataTagEntity>>> obj = bigdataTagBLL.GetPageList(new BigdataTagListParam() { DataTag = "3" }, new Pagination() { SortType = "asc", PageSize = int.MaxValue, PageIndex = 1, Sort = "SortId" });

                    coloumnStr += GetComboxColumnsStr("BigDataTag", "大数据标志", obj.Result.Data, "TagName1");
                }
                #endregion

                #region 自定义模板
                {
                    Task<TData<List<PrintstyleEntity>>> obj = printstyleBLL.GetList(new PrintstyleListParam());
                    coloumnStr += GetComboxColumnsStr("PrintStyleName", "自定义模板", obj.Result.Data, "PrintStyleName");
                }
                #endregion

                #region 标签类型
                {
                    Task<TData<List<TagtypeEntity>>> obj = tagtypeBLL.GetList(new TagtypeListParam());

                    coloumnStr += GetComboxColumnsStr("TagTypeName", "标签类型", obj.Result.Data, "TagTypeName");
                }
                #endregion
            }
            else { }


            coloumnStr = coloumnStr.Substring(0, coloumnStr.Length - 1);
            return $"[{coloumnStr}]";
        }


        private string GetComboxColumnsStr<T>(string data, string title, IEnumerable<T> dataList,string ComboxValueName)
        {
            string nameArr = string.Empty;
            foreach (var item in dataList)
            {
                // 假设你想要获取的属性名称是 AppraiserName，你需要根据实际类型调整
                // 这里使用反射来获取属性值
                var nameProperty = item.GetType().GetProperty(ComboxValueName);
                if (nameProperty != null)
                {
                    var nameValue = nameProperty.GetValue(item)?.ToString();
                    if (!string.IsNullOrEmpty(nameValue))
                    {
                        nameArr += $"'{nameValue}',";
                    }
                }
            }
            if (!string.IsNullOrEmpty(nameArr))
            {
                nameArr = nameArr.TrimEnd(',');
            }
            return GetColumnsStr(data, title, false, true, $"[{nameArr}]");
        }
         

        /// <summary>
        /// 组织表格所需行元素
        /// </summary>
        /// <param name="data"></param>
        /// <param name="title"></param>
        /// <param name="readOnly"></param>
        /// <param name="dropdown"></param>
        /// <param name="dropdownSource"></param>
        /// <returns></returns>
        public string GetColumnsStr(string data, string title, bool readOnly = false, bool dropdown = false, string dropdownSource = "[]")
        {
            //string visiblestr = visible ? "True" : "False";
            if (readOnly)
            {
                string readOnlyStr = "readOnly: true";
                return $"{{ data: '{data}' ,title:'{title}',{readOnlyStr} }},";
            }
            else
            {
                if (dropdown)
                {
                    return $"{{ data: '{data}' ,title:'{title}',type:'dropdown',source:{dropdownSource} }},";
                }
                else
                {
                    return $"{{ data: '{data}' ,title:'{title}' }},";
                }

            }
        }

        /// <summary>
        /// 复选框
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetCheckboxColumnsStr(string title)
        {
            return $"{{ data: 0 ,title:'', type: 'checkbox' }},";
        }

        /// <summary>
        /// 高版本按钮处理方式-禁用
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetBtnColumnsStr(string title)
        {
            return "{" +
                $"data: null,title:'{title}',renderer: function (instance, td, row, col, prop, value, cellProperties) " +
                "{" +
                "Handsontable.dom.empty(td);" +
                "var button = document.createElement('button');" +
                "button.textContent = '修改';" +
                "button.addEventListener('click', function () {alert('Button in row ' + row + ' clicked!');});" +
                "td.appendChild(button);" +
                "return td;}},";
        }
        #endregion
    }
}
