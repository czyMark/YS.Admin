using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using YS.Admin.Web.Controllers;
using YS.Admin.Business.SystemManage;
using YS.Admin.CodeGenerator.Model;
using YS.Admin.CodeGenerator.Template;
using YS.Admin.Entity;
using YS.Admin.Model.Result;
using YS.Admin.Model.Result.SystemManage;
using YS.Admin.Util;
using YS.Admin.Util.Model;
using YS.Admin.Web.Code;
using YS.Admin.CodeGenerator;

namespace YS.Admin.Web.Areas.ToolManage.Controllers
{
    [Area("ToolManage")]
    public class CodeGeneratorController : BaseController
    {
        private DatabaseTableBLL databaseTableBLL = new DatabaseTableBLL();

        #region 视图功能
        [AuthorizeFilter("tool:codegenerator:view")]
        public IActionResult CodeGeneratorIndex()
        {
            return View();
        }

        public IActionResult CodeGeneratorForm(string outputModule)
        {
            ViewBag.OutputModule = outputModule;
            return View();
        }
        public IActionResult CodeEntityGeneratorForm(string outputModule)
        {
            ViewBag.OutputModule = outputModule;
            return View();
        }
        public IActionResult CodeEntityAndServiceGeneratorForm(string outputModule)
        {
            ViewBag.OutputModule = outputModule;
            return View();
        }

        public IActionResult CodeGeneratorEditSearch()
        {
            return View();
        }

        public IActionResult CodeGeneratorEditToolbar()
        {
            return View();
        }

        public IActionResult CodeGeneratorEditList()
        {
            return View();
        }

        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("tool:codegenerator:search")]
        public async Task<IActionResult> GetTableFieldTreeListJson(string tableName)
        {
            TData<List<ZtreeInfo>> obj = await databaseTableBLL.GetTableFieldZtreeList(tableName);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("tool:codegenerator:search")]
        public async Task<IActionResult> GetTableFieldTreePartListJson(string tableName, int upper = 0)
        {
            TData<List<ZtreeInfo>> obj = await databaseTableBLL.GetTableFieldZtreeList(tableName);
            if (obj.Data != null)
            {
                // 基础字段不显示出来
                obj.Data.RemoveAll(p => BaseField.BaseFieldList.Contains(p.name));                
            }
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("tool:codegenerator:search")]
        public async Task<IActionResult> GetBaseConfigJson(string tableName)
        {
            TData<BaseConfigModel> obj = new TData<BaseConfigModel>();

            string tableDescription = string.Empty;
            TData<List<TableFieldInfo>> tDataTableField = await databaseTableBLL.GetTableFieldList(tableName);
            List<TableFieldInfo> columnList = tDataTableField.Data.Where(p => !BaseField.BaseFieldList.Contains(p.TableColumn)).ToList();

            OperatorInfo operatorInfo = await Operator.Instance.Current();
            string serverPath = GlobalContext.HostingEnvironment.ContentRootPath;
            obj.Data = new SingleTableTemplate().GetBaseConfig(serverPath, operatorInfo.UserName, tableName, tableDescription, columnList);
            obj.Tag = 1;
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("tool:codegenerator:add")]
        public async Task<IActionResult> CodePreviewJson(BaseConfigModel baseConfig)
        {
            TData<object> obj = new TData<object>();
            if (string.IsNullOrEmpty(baseConfig.OutputConfig.OutputModule))
            {
                obj.Message = "请选择输出到的模块";
            }
            else
            {
                SingleTableTemplate template = new SingleTableTemplate();
                TData<List<TableFieldInfo>> objTable = await databaseTableBLL.GetTableFieldList(baseConfig.TableName);
                DataTable dt = DataTableHelper.ListToDataTable(objTable.Data);  // 用DataTable类型，避免依赖
                if (baseConfig.Type == "1")
                {
                    string codeEntity = template.BuildEntity(baseConfig, dt);
                    var json = new
                    {
                        CodeEntity = HttpUtility.HtmlEncode(codeEntity)
                    };
                    obj.Data = json;
                    obj.Tag = 1;
                }
                else if (baseConfig.Type == "2")
                {
                    string codeEntity = template.BuildEntity(baseConfig, dt);
                    string codeEntityParam = template.BuildEntityParam(baseConfig, dt);
                    string codeService = template.BuildService(baseConfig, dt);
                    string codeBusiness = template.BuildBusiness(baseConfig);
                    string codeController = template.BuildController(baseConfig);
                    var json = new
                    {
                        CodeEntity = HttpUtility.HtmlEncode(codeEntity),
                        CodeEntityParam = HttpUtility.HtmlEncode(codeEntityParam),
                        CodeService = HttpUtility.HtmlEncode(codeService),
                        CodeBusiness = HttpUtility.HtmlEncode(codeBusiness),
                        CodeController = HttpUtility.HtmlEncode(codeController),
                    };
                    obj.Data = json;
                    obj.Tag = 1;
                }
                else
                {


                    string codeEntity = template.BuildEntity(baseConfig, dt);
                    string codeEntityParam = template.BuildEntityParam(baseConfig, dt);
                    string codeService = template.BuildService(baseConfig, dt);
                    string codeBusiness = template.BuildBusiness(baseConfig);
                    string codeController = template.BuildController(baseConfig);
                    string codeIndex = template.BuildIndex(baseConfig);
                    string codeForm = template.BuildForm(baseConfig);
                    string codeMenu = template.BuildMenu(baseConfig);

                    var json = new
                    {
                        CodeEntity = HttpUtility.HtmlEncode(codeEntity),
                        CodeEntityParam = HttpUtility.HtmlEncode(codeEntityParam),
                        CodeService = HttpUtility.HtmlEncode(codeService),
                        CodeBusiness = HttpUtility.HtmlEncode(codeBusiness),
                        CodeController = HttpUtility.HtmlEncode(codeController),
                        CodeIndex = HttpUtility.HtmlEncode(codeIndex),
                        CodeForm = HttpUtility.HtmlEncode(codeForm),
                        CodeMenu = HttpUtility.HtmlEncode(codeMenu)
                    };
                    obj.Data = json;
                    obj.Tag = 1;

                }
                
            }
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("tool:codegenerator:add")]
        public async Task<IActionResult> CodeGenerateJson(BaseConfigModel baseConfig, string Code)
        {
            TData<List<KeyValue>> obj = new TData<List<KeyValue>>();
            if (!GlobalContext.SystemConfig.Debug)
            {
                obj.Message = "请在本地开发模式时使用代码生成";
            }
            else
            {
                SingleTableTemplate template = new SingleTableTemplate();
                List<KeyValue> result = await template.CreateCode(baseConfig, HttpUtility.UrlDecode(Code));
                obj.Data = result;
                obj.Tag = 1;
            }
            return Json(obj);
        }
        #endregion
    }
}